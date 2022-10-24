using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinConsumerApp.ApiServices;
using XamarinConsumerApp.Helpers;
using XamarinConsumerApp.Models;
using XamarinConsumerApp.Resources;
using static XamarinConsumerApp.Helpers.AlertProvider;
using XamarinConsumerApp.Enums;

namespace XamarinConsumerApp.Views.VoucherViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VoucherDetailsPage : BaseViews.BaseContentPage
    {
        VoucherDetailsViewModel _viewModel = new VoucherDetailsViewModel();
        int _voucherDesignID = 0;
        int _voucherID = 0;

        public class VoucherDetailsViewModel
        {
            public VoucherModel Voucher { get; set; } = new VoucherModel();
        }

        protected async override void OnAppearing()
        {
            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                var voucherDetailsRequest = new MiscellaneousService.CouponDetailsRequest()
                {
                    ID = _voucherDesignID,
                    VoucherID = _voucherID,
                };
                var voucherDetailsResponse = await MiscellaneousService.CouponDetailsApi(voucherDetailsRequest);
                if (!voucherDetailsResponse.Error.IsEmpty())                    
                {
                    await ErrorAlert(this, voucherDetailsResponse.Error);
                }
                else
                {
                    _viewModel.Voucher = new VoucherModel()
                    {
                        Name = voucherDetailsResponse.Name,
                        Description = voucherDetailsResponse.Description,
                        ValidUntilDate = $"Valid Until: {voucherDetailsResponse.ValidTill}",
                        TnCList = voucherDetailsResponse.TnCList,
                        RedeemMethod = voucherDetailsResponse.RedeemMethod,
                        RedemptionPoints = voucherDetailsResponse.Redeem,
                    };
                    //_viewModel.Voucher = new VoucherModel()
                    //{
                    //    Name = "Test Voucher",
                    //    ValidUntilDate = "Valid Until: 09/09/2020",
                    //    Description = "A simple description",
                    //    TnCList = new List<string> { "- Text 1", "- Text 2" },
                    //    RedeemMethod = RedeemMethodEnum.SWIPE.ToString(),
                    //    RedemptionPoints = new List<RedemptionPoint>()
                    //    {
                    //        new RedemptionPoint()
                    //        {
                    //            address = "Very long address",
                    //            information = "A0001",
                    //            contactNo = "0123456789",
                    //            state = "new state",
                    //            town="new town",
                    //        },
                    //        new RedemptionPoint()
                    //        {
                    //            address = "Very long address",
                    //            information = "A0002",
                    //            contactNo = "0123456789",
                    //            state = "new state",
                    //            town="new town",
                    //        }
                    //    },
                    //};
                }
            }

            this.BindingContext = _viewModel;

            collectionViewTnCList.HeightRequest = _viewModel.Voucher.TnCList.Count * 20;
            collectionViewRedemptionPoints.HeightRequest = _viewModel.Voucher.RedemptionPoints.Count * 20;
        }

        public VoucherDetailsPage(int voucherDesignID, int voucherID)
        {
            InitializeComponent();

            _voucherDesignID = voucherDesignID;
            _voucherID = voucherID;
        }

        private async void OnClicked_Share(object sender, EventArgs e)
        {
            var voucherDetails = ((ImageButton)sender).CommandParameter as VoucherModel;

            await SuccessAlert(this, $"Share with Name: {voucherDetails.Name}");
        }

        private async void OnClicked_Redeem(object sender, EventArgs e)
        {
            var voucherDetails = ((Button)sender).CommandParameter as VoucherModel;

            if (_viewModel.Voucher.RedeemMethod == RedeemMethodEnum.INPUT.ToString())
            {
                await Navigation.PushAsync(new VoucherViews.VoucherRedeemByMerchantNamePage(voucherDetails));
            }
            else if (_viewModel.Voucher.RedeemMethod == RedeemMethodEnum.SCAN.ToString())
            {
                await Navigation.PushAsync(new VoucherViews.VoucherRedeemByQRCodePage(voucherDetails));
            }
            else
            {
                await Navigation.PushAsync(new VoucherViews.VoucherRedeemBySwipePage(voucherDetails));
            }
        }

        private async void OnTapped_RedemptionPoints(object sender, EventArgs e)
        {
            var args = (TappedEventArgs)e;
            var voucherDetails = args.Parameter as VoucherModel;

            await Navigation.PushModalAsync(new NavigationPage(new VoucherViews.VoucherRedemptionPointPage(voucherDetails)));
        }
    }
}