using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamarinConsumerApp.Models;

namespace XamarinConsumerApp.Views.VoucherViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VoucherRedemptionPointPage : BaseViews.BaseContentPage
    {
        VoucherDetailsViewModel _viewModel;
        VoucherModel _voucherDetails;

        public class VoucherDetailsViewModel
        {
            public VoucherModel Voucher { get; set; } = new VoucherModel();
        }

        protected override void OnAppearing()
        {
            _viewModel = new VoucherDetailsViewModel() { Voucher = _voucherDetails };
            this.BindingContext = _viewModel;

            collectionViewRedemptionPoints.HeightRequest = _viewModel.Voucher.RedemptionPoints.Count * 120;
        }

        public VoucherRedemptionPointPage(VoucherModel voucherDetails)
        {
            InitializeComponent();

            _voucherDetails = voucherDetails;
        }

        private async void OnClicked_Close(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}