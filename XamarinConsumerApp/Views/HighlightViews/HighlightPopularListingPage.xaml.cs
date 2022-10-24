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
    public partial class HighlightPopularListingPage : BaseViews.BaseContentPage
    {
        HighlightPopularViewModel _viewModel;

        public class HighlightPopularViewModel
        {
            public List<VoucherModel> PopularListing { get; set; } = new List<VoucherModel>();
            //{
            //        new VoucherModel()
            //        {
            //            ID  =1,
            //            Image = "home_slider1.jpg",
            //            Name = "Voucher 1",
            //            Description = "Description",
            //            ValidUntilDate = "Valid until 8/4/2019",
            //            TnC = "A very long terms & conditions description.",
            //        },
            //        new VoucherModel()
            //        {
            //            ID  =2,
            //            Image = "home_slider1.jpg",
            //            Name = "Voucher 2",
            //            Description = "Description",
            //            ValidUntilDate = "Valid until 8/4/2019",
            //            TnC = "A very long terms & conditions description.",
            //        },
            //};
        }

        protected async override void OnAppearing()
        {
            _viewModel = new HighlightPopularViewModel();

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
                    _viewModel.PopularListing = highlightAllResponse.allItemList;
                }
            }

            this.BindingContext = _viewModel;

            collectionViewPopularListing.HeightRequest = _viewModel.PopularListing.Count * 120;
        }

        public HighlightPopularListingPage()
        {
            InitializeComponent();
        }

        private async void OnRefreshing_Highlight(object sender, EventArgs e)
        {
            var highlightPopularResponse = await GetHighlightAll();

            if (!highlightPopularResponse.Error.IsEmpty())
            {
                await ErrorAlert(this, "Backend API un-sync.");
                //await ErrorAlert(this, highlightAllResponse.Error);
            }
            else
            {
                _viewModel.PopularListing = highlightPopularResponse.allItemList;

                collectionViewPopularListing.ItemsSource = highlightPopularResponse.allItemList;
            }

            refreshViewPopular.IsRefreshing = false;
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
            var request = new MiscellaneousService.HighlightAllRequest() { IsPopular = true };

            return MiscellaneousService.HighlightAllApi(request);
        }
    }
}