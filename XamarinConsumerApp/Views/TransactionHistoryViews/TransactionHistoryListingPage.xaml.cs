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

namespace XamarinConsumerApp.Views.TransactionHistoryViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionHistoryListingPage : BaseViews.BaseContentPage
    {
        TransactionHistoryViewModel _viewModel;

        public class TransactionHistoryViewModel
        {
            public List<TransactionHistoryModel> Transactions { get; set; } = new List<TransactionHistoryModel>();
        }

        protected async override void OnAppearing()
        {
            _viewModel = new TransactionHistoryViewModel();

            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                var transactionHistoryResponse = await GetTransactionHistory();

                if (!transactionHistoryResponse.Error.IsEmpty())
                {
                    await ErrorAlert(this, transactionHistoryResponse.Error);
                }
                else
                {
                    _viewModel.Transactions = transactionHistoryResponse.Redeemed;
                    collectionViewTransaction.ItemsSource = _viewModel.Transactions;
                }
            }

            this.BindingContext = _viewModel;
        }

        public TransactionHistoryListingPage()
        {
            InitializeComponent();
        }

        private async void OnClicked_Close(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void OnRefreshing_VoucherCollectionView(object sender, EventArgs e)
        {
            var transactionHistoryResponse = await GetTransactionHistory();

            if (!transactionHistoryResponse.Error.IsEmpty())
            {
                await ErrorAlert(this, transactionHistoryResponse.Error);
            }
            else
            {
                _viewModel.Transactions = transactionHistoryResponse.Redeemed;
                collectionViewTransaction.ItemsSource = _viewModel.Transactions;
            }

            refreshViewTransaction.IsRefreshing = false;
        }

        private Task<MiscellaneousService.TransactionHistoryResponse> GetTransactionHistory()
        {
            var request = new MiscellaneousService.TransactionHistoryRequest()
            {
                ID = DB.GetValue(StorageEnum.UserID).ToInt(),
            };

            return MiscellaneousService.TransactionHistoryApi(request);
        }
    }
}