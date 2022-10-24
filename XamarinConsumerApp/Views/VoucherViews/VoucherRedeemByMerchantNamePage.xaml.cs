using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinConsumerApp.ApiServices;
using XamarinConsumerApp.Models;
using XamarinConsumerApp.Resources;
using static XamarinConsumerApp.Helpers.AlertProvider;
using XamarinConsumerApp.Helpers;
using XamarinConsumerApp.Enums;

namespace XamarinConsumerApp.Views.VoucherViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VoucherRedeemByMerchantNamePage : BaseViews.BaseContentPage
    {
        VoucherRedeemByMerchantNameViewModel _viewModel = new VoucherRedeemByMerchantNameViewModel();
        VoucherModel _voucherDetails;

        protected async override void OnAppearing()
        {
            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                var voucherRedeemDetailsRequest = new MiscellaneousService.CouponRedeemDetailsRequest()
                {
                    UserId = DB.GetValue(StorageEnum.UserID).ToInt(),
                    VoucherDesignID = _voucherDetails.VoucherDesignID,
                    VoucherID = _voucherDetails.VoucherID,
                };
                var voucherRedeemDetailsResponse = await MiscellaneousService.CouponRedeemDetailsApi(voucherRedeemDetailsRequest);
                if (!voucherRedeemDetailsResponse.Error.IsEmpty())
                {
                    await ErrorAlert(this, voucherRedeemDetailsResponse.Error);
                }
                else
                {
                    _viewModel.Voucher = voucherRedeemDetailsResponse.RedeemInfo;
                    //_viewModel.Voucher = new VoucherModel()
                    //{
                    //    OrgName = "HEHEHEHE",
                    //    VoucherName = "HE HE HE HE",
                    //};
                }
            }

            this.BindingContext = _viewModel;
        }

        public class VoucherRedeemByMerchantNameViewModel
        {
            public VoucherModel Voucher { get; set; } = new VoucherModel();
        }

        public VoucherRedeemByMerchantNamePage(VoucherModel voucherDetails)
        {
            InitializeComponent();

            _voucherDetails = voucherDetails;
        }

        private async void OnClicked_Redeem(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                var voucherRedeemRequest = new MiscellaneousService.CouponRedeemRequest()
                {
                    RedeemMethod = _voucherDetails.RedeemMethod,
                    MerchantID = txtMerchantCode.Text,
                    UserId = DB.GetValue(StorageEnum.UserID).ToInt(),
                    VoucherDesignID = _viewModel.Voucher.VoucherDesignID,
                    VoucherID = _viewModel.Voucher.VoucherID,
                    VoucherCode = _viewModel.Voucher.VoucherCode,
                };
                var voucherRedeemResponse = await MiscellaneousService.CouponRedeemApi(voucherRedeemRequest);
                if (!voucherRedeemResponse.Error.IsEmpty())
                {
                    //await ErrorAlert(this, voucherRedeemResponse.Error);
                    await ErrorAlert(this, "Backend API un-sync");
                }
                else
                {
                    await SuccessAlert(this, "Redeemed.");
                }
            }
        }
    }
}