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

namespace XamarinConsumerApp.Views.HighlightViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HighlightNewListingPage : BaseViews.BaseContentPage
    {
        HighlightNewViewModel _viewModel;

        public class HighlightNewViewModel
        {
            public List<VoucherModel> NewListing { get; set; } = new List<VoucherModel>();
            //{
            //    new VoucherModel()
            //    {
            //        ID  =3,
            //        Image = "home_slider1.jpg",
            //        Name = "Voucher 3",
            //        Description = "Description",
            //        ValidUntilDate = "Valid until 8/4/2019",
            //        TnC = "A very long terms & conditions description.",

            //    },
            //    new VoucherModel()
            //    {
            //        ID  =4,
            //        Image = "home_slider1.jpg",
            //        Name = "Voucher 4",
            //        Description = "Description",
            //        ValidUntilDate = "Valid until 8/4/2019",
            //        TnC = "A very long terms & conditions description.",
            //    },
            //};
        }

        protected async override void OnAppearing()
        {
            _viewModel = new HighlightNewViewModel();

            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                var highlightAllResponse = await GetHighlightAll();

                if (!highlightAllResponse.Error.IsEmpty())
                {
                    await ErrorAlert(this, "Backend API un-sync.");
                    //await ErrorAlert(this, highlightAllResponse.Error);
                }
                else
                {
                    _viewModel.NewListing = highlightAllResponse.allItemList;
                }
            }

            this.BindingContext = _viewModel;

            collectionViewNewListing.HeightRequest = _viewModel.NewListing.Count * 120;
        }

        public HighlightNewListingPage()
        {
            InitializeComponent();
        }

        private async void OnRefreshing_Highlight(object sender, EventArgs e)
        {
            var highlightAllResponse = await GetHighlightAll();

            if (!highlightAllResponse.Error.IsEmpty())
            {
                await ErrorAlert(this, "Backend API un-sync.");
                //await ErrorAlert(this, highlightAllResponse.Error);
            }
            else
            {
                _viewModel.NewListing = highlightAllResponse.allItemList;

                collectionViewNewListing.ItemsSource = highlightAllResponse.allItemList;
            }

            refreshViewNew.IsRefreshing = false;
        }

        private async void OnTapped_Voucher(object sender, EventArgs e)
        {
            var args = (TappedEventArgs)e;
            var voucherDetails = args.Parameter as VoucherModel;

            await Navigation.PushAsync(new VoucherViews.VoucherDetailsPage(voucherDetails.VoucherDesignID, voucherDetails.VoucherID));
        }

        private async void OnClicked_Share(object sender, EventArgs e)
        {
            var voucherDetails = ((ImageButton)sender).CommandParameter as VoucherModel;

            await AlertProvider.SuccessAlert(this, $"Share with Name: {voucherDetails.Name}");
        }

        private async void OnClicked_Download(object sender, EventArgs e)
        {
            var voucherDetails = ((ImageButton)sender).CommandParameter as VoucherModel;

            await AlertProvider.SuccessAlert(this, $"Download with Name: {voucherDetails.Name}");
        }

        private Task<MiscellaneousService.HighlightAllResponse> GetHighlightAll()
        {
            var request = new MiscellaneousService.HighlightAllRequest() { IsPopular = false };

            return MiscellaneousService.HighlightAllApi(request);
        }
    }
}