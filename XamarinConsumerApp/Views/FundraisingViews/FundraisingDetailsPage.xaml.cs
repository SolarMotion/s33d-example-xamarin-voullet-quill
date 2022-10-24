using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinConsumerApp.Helpers;
using XamarinConsumerApp.Models;
using Api = XamarinConsumerApp.ApiServices.MiscellaneousService;

namespace XamarinConsumerApp.Views.FundraisingViews
{
    // TODO: Refactor, Exception Handle, MVVM
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FundraisingDetailsPage : BaseViews.BaseContentPage
    {
        int _campaignID;
        FundraisingDetail _viewModel;

        #region Constructor

        public FundraisingDetailsPage(int campaignID)
        {
            InitializeComponent();

            _campaignID = campaignID;
        }

        #endregion

        #region Private Methods

        private async Task ConstructViewModel()
        {
            _viewModel = await GetDetails();

            this.BindingContext = _viewModel;
        }
        private async Task<FundraisingDetail> GetDetails()
        {
            var response = await Api.GetFundraisingDetail(_campaignID);

            if (!string.IsNullOrEmpty(response.Error))
            {
                await AlertProvider.ErrorAlert(this, response.Error);
                return new FundraisingDetail();
            }

            return response;
        }

        #endregion

        #region Override Methods

        protected override async void OnAppearing()
        {
            if (_viewModel == null)
                await ConstructViewModel();
        }

        #endregion

        #region Event Handler

        private async void OnClicked_Share(object sender, EventArgs e)
        {
            var details = ((ImageButton)sender).CommandParameter as FundraisingDetail;
            await ShareUri(details.Url, details.Name);
        }

        // TODO: Proper Voucher Detail linkage
        private async void OnTapped_Voucher(object sender, EventArgs e)
        {
            var args = (TappedEventArgs)e;
            var item = args.Parameter as FundraisingDetailItem;
            var voucherDetails = new VoucherModel
            {
                Description = item.Description,
                ID = item.ID,
                Image = item.Image,
                Name = item.Name,
                ValidUntilDate = item.ValidTill
            };

            await Navigation.PushAsync(new VoucherViews.VoucherDetailsPage(item.ID, 0));
        }

        #endregion


    }
}