using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading;

using XamarinConsumerApp.Views;
using XamarinConsumerApp.Helpers;
using XamarinConsumerApp.Enums;
using XamarinConsumerApp.ApiServices;
using static XamarinConsumerApp.Helpers.CommonExtension;
using XamarinConsumerApp.Models;
using Xamarin.Essentials;
using static XamarinConsumerApp.Helpers.AlertProvider;
using Acr.UserDialogs;
using XamarinConsumerApp.Resources;

namespace XamarinConsumerApp.Views.GenericViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : BaseViews.BaseContentPage
    {
        HomeViewModel _viewModel = new HomeViewModel();

        public class HomeViewModel
        {
            public List<BulletinModel> BulletinListing { get; set; } = new List<BulletinModel>();
            //{
            //    new BulletinModel()
            //        {
            //            ID = 1,
            //            Name = "Slider 1",
            //            Description = "Description 1",
            //            Image = "home_slider1.jpg",
            //        },
            //        new BulletinModel()
            //        {
            //            ID = 2,
            //            Name = "Slider 2",
            //            Description = "Description 2",
            //            Image = "home_slider2.jpg",
            //        },
            //        new BulletinModel()
            //        {
            //            ID = 3,
            //            Name = "Slider 3",
            //            Description = "Description 3",
            //            Image = "home_slider3.jpg",
            //        }
            //};

            public List<VoucherModel> VoucherListing { get; set; } = new List<VoucherModel>();
            //{
            //        new VoucherModel()
            //{
            //    ID = 1,
            //    ValidUntil = null,
            //            OrgName = "Voucher",
            //            Image = "home_slider1.jpg",
            //            Name = "Voucher 1",
            //            Description = "Description",
            //            ValidUntilStr = "Valid until 8/4/2019",
            //            RedemptionPoints = new List<RedemptionPoint>()
            //            {
            //                new RedemptionPoint()
            //                {
            //                    ID = 1,
            //                    Name = "MM1",
            //                    Description = "A very long description.",
            //                    ContactNo = "0123456789",
            //                },
            //                new RedemptionPoint()
            //                {
            //                    ID = 2,
            //                    Name = "MM2",
            //                    Description = "A very long description.",
            //                    ContactNo = "0123456789",
            //                },
            //            },
            //        },
            //        new VoucherModel()
            //{
            //    ID = 2,
            //                    ValidUntil = new DateTime(2020, 3, 23),
            //            OrgName = "Voucher",
            //            Image = "home_slider1.jpg",
            //            Name = "Voucher 2",
            //            Description = "Description",
            //            ValidUntilStr = "Valid until 8/4/2019",
            //            RedemptionPoints = new List<RedemptionPoint>()
            //            {
            //                new RedemptionPoint()
            //                {
            //                    ID = 1,
            //                    Name = "MM1",
            //                    Description = "A very long description.",
            //                    ContactNo = "0123456789",
            //                },
            //                new RedemptionPoint()
            //                {
            //                    ID = 2,
            //                    Name = "MM2",
            //                    Description = "A very long description.",
            //                    ContactNo = "0123456789",
            //                },
            //            },
            //        },
            //        new VoucherModel()
            //{
            //    ID = 3,
            //                                    ValidUntil = new DateTime(2020, 9, 23),
            //            OrgName = "Voucher",
            //            Image = "home_slider1.jpg",
            //            Name = "Voucher 3",
            //            Description = "Description",
            //            ValidUntilStr = "Valid until 8/4/2019",
            //            RedemptionPoints = new List<RedemptionPoint>()
            //            {
            //                new RedemptionPoint()
            //                {
            //                    ID = 1,
            //                    Name = "MM1",
            //                    Description = "A very long description.",
            //                    ContactNo = "0123456789",
            //                },
            //                new RedemptionPoint()
            //                {
            //                    ID = 2,
            //                    Name = "MM2",
            //                    Description = "A very long description.",
            //                    ContactNo = "0123456789",
            //                },
            //            },
            //        },
            //        new VoucherModel()
            //{
            //    ID = 4,
            //                                    ValidUntil = new DateTime(2020, 12, 23),
            //            OrgName = "Voucher",
            //            Image = "home_slider1.jpg",
            //            Name = "Voucher 4",
            //            Description = "Description",
            //            ValidUntilStr = "Valid until 8/4/2019",
            //            RedemptionPoints = new List<RedemptionPoint>()
            //            {
            //                new RedemptionPoint()
            //                {
            //                    ID = 1,
            //                    Name = "MM6",
            //                    Description = "A very long description.",
            //                    ContactNo = "0123456789",
            //                },
            //                new RedemptionPoint()
            //                {
            //                    ID = 2,
            //                    Name = "MM7",
            //                    Description = "A very long description.",
            //                    ContactNo = "0123456789",
            //                },
            //            },
            //        },
            //};
        }

        protected async override void OnAppearing()
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
                await ErrorAlert(this, "Connection to internet is not available.");

            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                var bulletinIndexResponse = await MiscellaneousService.BulletinIndexApi();
                if (!bulletinIndexResponse.Error.IsEmpty())
                {
                    await ErrorAlert(this, bulletinIndexResponse.Error);
                }
                else
                {
                    _viewModel.BulletinListing = bulletinIndexResponse.BeanBagImagesSlideList;
                }


                var couponCustomerListingResponse = await GetCouponConsumerListing();
                if (!couponCustomerListingResponse.Error.IsEmpty())
                {
                    await ErrorAlert(this, couponCustomerListingResponse.Error);
                }
                else
                {
                    _viewModel.VoucherListing = couponCustomerListingResponse.MyCouponList;
                }
            }

            this.BindingContext = _viewModel;
        }

        public HomePage()
        {
            InitializeComponent();
        }

        private void OnToggle_SwitchType(object sender, ToggledEventArgs e)
        {
            if (switchType.IsToggled)
            {
                lblType.Text = "Expiring";

                collectionViewVoucherListing.ItemsSource = null;

                var result = _viewModel.VoucherListing.Where(a => a.ValidUntil != null).OrderBy(a => a.ValidUntil).ToList();
                result.AddRange(_viewModel.VoucherListing.Where(a => a.ValidUntil == null).ToList());

                collectionViewVoucherListing.ItemsSource = result;
            }
            else
            {
                lblType.Text = "All";

                collectionViewVoucherListing.ItemsSource = null;
                collectionViewVoucherListing.ItemsSource = _viewModel.VoucherListing;
            }
        }

        private async void OnRefreshing_VoucherCollectionView(object sender, EventArgs e)
        {
            var couponCustomerListingResponse = await GetCouponConsumerListing();

            if (!couponCustomerListingResponse.Error.IsEmpty())
            {
                await ErrorAlert(this, couponCustomerListingResponse.Error);
            }
            else
            {
                _viewModel.VoucherListing = couponCustomerListingResponse.MyCouponList;

                collectionViewVoucherListing.ItemsSource = couponCustomerListingResponse.MyCouponList;
            }

            searchBarVoucherListing.Text = "";
            refreshVoucherCollectionView.IsRefreshing = false;
        }

        private void OnClick_SearchBar(object sender, EventArgs e)
        {
            // Do nothing
        }

        private async void OnTapped_Voucher(object sender, EventArgs e)
        {
            var args = (TappedEventArgs)e;
            var voucherDetails = args.Parameter as VoucherModel;

            await Navigation.PushAsync(new VoucherViews.VoucherDetailsPage(voucherDetails.VoucherDesignID, voucherDetails.VoucherID));
        }

        private async void OnTapped_Bulletin(object sender, EventArgs e)
        {
            var args = (TappedEventArgs)e;
            var bulletinDetails = args.Parameter as BulletinModel;

            await Navigation.PushAsync(new BulletinViews.BulletinDetailsPage(bulletinDetails.BulletinID, bulletinDetails.ImageStr));
        }

        private async void OnClicked_Share(object sender, EventArgs e)
        {
            var voucherDetails = ((ImageButton)sender).CommandParameter as VoucherModel;

            await AlertProvider.SuccessAlert(this, $"Share with Name: {voucherDetails.Name}");
        }

        private async void OnClicked_Delete(object sender, EventArgs e)
        {
            var voucherDetails = ((ImageButton)sender).CommandParameter as VoucherModel;

            var isDelete = await DisplayAlert($"Delete {voucherDetails.Name}", $"Are you sure you want delete this {voucherDetails.Name} coupon?", "Yes", "No");

            if (!isDelete)
                return;

            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                var couponDeleteRequest = new MiscellaneousService.CouponDeleteRequest()
                {
                    ID = voucherDetails.ID,
                    UserId = DB.GetValue(StorageEnum.UserID).ToInt(),
                };

                var couponDeleteResponse = await MiscellaneousService.CouponDeleteApi(couponDeleteRequest);

                if (couponDeleteResponse.Error.IsEmpty())
                {
                    var result = _viewModel;

                    var deletedCoupon = result.VoucherListing.First(a => a.ID == voucherDetails.ID);
                    result.VoucherListing.Remove(deletedCoupon);

                    collectionViewVoucherListing.ItemsSource = null;
                    collectionViewVoucherListing.ItemsSource = result.VoucherListing;

                    await SuccessAlert(this, "Coupon deleted.");
                }
                else
                {
                    await ErrorAlert(this, couponDeleteResponse.Error);
                }
            }
        }

        private void TextChanged_SearchBar(object sender, TextChangedEventArgs e)
        {
            var searchBar = (SearchBar)sender;
            var searchBarText = searchBar.Text.ToUpper();

            var result = _viewModel;
            collectionViewVoucherListing.ItemsSource = result.VoucherListing.Where(a => a.Name.ToUpper().Contains(searchBarText) || a.Description.ToUpper().Contains(searchBarText));
        }

        private Task<MiscellaneousService.CouponConsumerListingResponse> GetCouponConsumerListing()
        {
            var request = new MiscellaneousService.CouponConsumerListingRequest()
            {
                UserId = DB.GetValue(StorageEnum.UserID).ToInt(),
            };

            return MiscellaneousService.CouponConsumerListingApi(request);
        }
    }
}