using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinConsumerApp.ApiServices;
using XamarinConsumerApp.Models;
using static XamarinConsumerApp.Helpers.AlertProvider;
using XamarinConsumerApp.Enums;
using XamarinConsumerApp.Helpers;
using Acr.UserDialogs;
using XamarinConsumerApp.Resources;

namespace XamarinConsumerApp.Views.CouponViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CouponIndexPage : BaseViews.BaseContentPage
    {
        CouponIndexViewModel _viewModel;

        public class CouponIndexViewModel
        {
            public List<VoucherModel> VoucherListing { get; set; } = new List<VoucherModel>();
            //{
            //        new VoucherModel()
            //        {
            //            ID  =1,
            //            Image = "home_slider1.jpg",
            //            Name = "Voucher 1",
            //            Description = "Description",
            //            ValidUntilStr = "Valid until 8/4/2019",
            //            TnC = "A very long terms & conditions description.",
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
            //        {
            //            ID  =2,
            //            Image = "home_slider1.jpg",
            //            Name = "Voucher 2",
            //            Description = "Description",
            //            ValidUntilStr = "Valid until 8/4/2019",
            //            TnC = "A very long terms & conditions description.",
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
            //        {
            //            ID  =3,
            //            Image = "home_slider1.jpg",
            //            Name = "Voucher 3",
            //            Description = "Description",
            //            ValidUntilStr = "Valid until 8/4/2019",
            //            TnC = "A very long terms & conditions description.",
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
            //        {
            //            ID  =4,
            //            Image = "home_slider1.jpg",
            //            Name = "Voucher 4",
            //            Description = "Description",
            //            ValidUntilStr = "Valid until 8/4/2019",
            //            TnC = "A very long terms & conditions description.",
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
            _viewModel = new CouponIndexViewModel();

            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
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

        public CouponIndexPage()
        {
            InitializeComponent();
        }

        private void OnClick_SearchBar(object sender, EventArgs e)
        {
            // Do nothing
        }

        private void TextChanged_SearchBar(object sender, TextChangedEventArgs e)
        {
            var searchBar = (SearchBar)sender;
            var searchBarText = searchBar.Text;

            var result = _viewModel;
            collectionViewVoucherListing.ItemsSource = result.VoucherListing.Where(a => a.Name.Contains(searchBarText) || a.Description.Contains(searchBarText));
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

        private async void OnTapped_Voucher(object sender, EventArgs e)
        {
            var args = (TappedEventArgs)e;
            var voucherDetails = args.Parameter as VoucherModel;

            await Navigation.PushAsync(new VoucherViews.VoucherDetailsPage(voucherDetails.VoucherDesignID, voucherDetails.VoucherID));
        }

        private async void OnToggle_SwitchSortBy(object sender, ToggledEventArgs e)
        {
            if (switchSortBy.IsToggled)
            {
                lblSortBy.Text = "Nationwide";
                switchSortBy.IsToggled = false;

                await ErrorAlert(this, "Backend API un-sync.");
                //lblSortBy.Text = "Local";
            }
            else
                lblSortBy.Text = "Nationwide";
        }

        private async void OnClicked_Share(object sender, EventArgs e)
        {
            var voucherDetails = ((ImageButton)sender).CommandParameter as VoucherModel;

            await SuccessAlert(this, $"Share with Name: {voucherDetails.Name}");
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
                    UserId = DB.GetValue(StorageEnum.UserID).ToInt(),
                    ID = voucherDetails.ID,
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