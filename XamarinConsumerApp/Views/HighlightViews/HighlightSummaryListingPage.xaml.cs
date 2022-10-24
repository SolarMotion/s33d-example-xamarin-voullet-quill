using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinConsumerApp.ApiServices;
using XamarinConsumerApp.Enums;
using XamarinConsumerApp.Helpers;
using XamarinConsumerApp.Models;
using XamarinConsumerApp.Resources;
using static XamarinConsumerApp.Helpers.AlertProvider;

namespace XamarinConsumerApp.Views.HighlightViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HighlightSummaryListingPage : BaseViews.BaseContentPage
    {
        HighlightViewModel _viewModel;

        public class HighlightViewModel
        {
            public List<VoucherModel> NewListing { get; set; } = new List<VoucherModel>();
            //{
            //        new VoucherModel()
            //        {
            //            ID  =3,
            //            Image = "home_slider1.jpg",
            //            Name = "Voucher 3",
            //            Description = "Description",
            //            ValidUntilDate = "Valid until 8/4/2019",
            //            TnC = "A very long terms & conditions description.",

            //        },
            //        new VoucherModel()
            //        {
            //            ID  =4,
            //            Image = "home_slider1.jpg",
            //            Name = "Voucher 4",
            //            Description = "Description",
            //            ValidUntilDate = "Valid until 8/4/2019",
            //            TnC = "A very long terms & conditions description.",
            //        },
            //};

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
            _viewModel = new HighlightViewModel();

            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                var highlightSummaryResponse = await GetHighlightSummary();

                if (!highlightSummaryResponse.Error.IsEmpty())
                {
                    await ErrorAlert(this, highlightSummaryResponse.Error);
                }
                else
                {
                    _viewModel.NewListing = highlightSummaryResponse.newItemList;
                    _viewModel.PopularListing = highlightSummaryResponse.popularItemList;
                }
            }

            this.BindingContext = _viewModel;

            collectionViewPopularListing.HeightRequest = _viewModel.PopularListing.Count * 120;
            collectionViewNewListing.HeightRequest = _viewModel.NewListing.Count * 120;
        }

        public HighlightSummaryListingPage()
        {
            InitializeComponent();
        }

        private async void OnRefreshing_Highlight(object sender, EventArgs e)
        {
            var highlightSummaryResponse = await GetHighlightSummary();

            if (!highlightSummaryResponse.Error.IsEmpty())
            {
                await ErrorAlert(this, highlightSummaryResponse.Error);
            }
            else
            {
                _viewModel.NewListing = highlightSummaryResponse.newItemList;
                _viewModel.PopularListing = highlightSummaryResponse.popularItemList;

                collectionViewNewListing.ItemsSource = highlightSummaryResponse.newItemList;
                collectionViewPopularListing.ItemsSource = highlightSummaryResponse.popularItemList;
            }

            refreshViewHighlight.IsRefreshing = false;
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

            await AlertProvider.SuccessAlert(this, $"Share with Name: {voucherDetails.VoucherDescription}");
        }

        private async void OnClicked_Download(object sender, EventArgs e)
        {
            var voucherDetails = ((ImageButton)sender).CommandParameter as VoucherModel;

            await AlertProvider.SuccessAlert(this, $"Download with Name: {voucherDetails.VoucherDescription}");
        }

        private async void OnTapped_PopularLabel(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HighlightPopularListingPage());
        }

        private async void OnTapped_NewLabel(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HighlightNewListingPage());
        }

        private Task<MiscellaneousService.HighlightSummaryResponse> GetHighlightSummary()
        {
            return MiscellaneousService.HighlightSummaryApi();
        }
    }
}