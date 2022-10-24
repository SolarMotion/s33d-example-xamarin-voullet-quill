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
    public partial class FundraisingIndexPage : BaseViews.BaseContentPage
    {
        FundraisingViewModel _viewModel;

        #region Constructor

        public FundraisingIndexPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        private async Task ConstructViewModel(string search = "")
        {
            _viewModel = new FundraisingViewModel
            {
                Items = await GetListing(search)
            };

            this.BindingContext = _viewModel;
        }
        private async Task<List<FundraisingModel>> GetListing(string search = "")
        {
            var response = await Api.GetFundraisingList(new Api.GetFundraisingListRequest { SearchName = search });

            if (!string.IsNullOrEmpty(response.Error))
            {
                await AlertProvider.ErrorAlert(this, response.Error);
                return new List<FundraisingModel>();
            }

            return response.Items.Select(a =>
                new FundraisingModel
                {
                    ID = a.ID,
                    Image = a.Image,
                    Description = a.Description,
                    Name = a.Name,
                    ValidTill = $"Valid Until {a.ValidTill}",
                    Url = a.Url
                }).ToList();
        }

        #endregion

        #region Override Methods

        protected override void OnAppearing()
        {
            if (_viewModel == null)
                refreshCollectionView.IsRefreshing = true;
        }

        #endregion

        #region View Action - SearchBar

        private void TextChanged_SearchBar(object sender, TextChangedEventArgs e)
        {
            var searchBar = (SearchBar)sender;
            var searchBarText = searchBar.Text;

            collectionViewListing.ItemsSource = _viewModel.Items.Where(a => a.Name.Contains(searchBarText) || a.Description.Contains(searchBarText));
        }

        private void OnClick_SearchBar(object sender, EventArgs e)
        {
            refreshCollectionView.IsRefreshing = true;
        }

        #endregion

        #region View Action - List

        private async void OnRefreshing_CollectionView(object sender, EventArgs e)
        {
            if (_viewModel == null)
                await ConstructViewModel();
            else
                _viewModel.Items = await GetListing(searchBarFundraisingListing.Text);

            collectionViewListing.ItemsSource = _viewModel.Items;
            refreshCollectionView.IsRefreshing = false;
        }

        private async void OnTapped_List(object sender, EventArgs e)
        {
            var args = e as TappedEventArgs;
            var model = args.Parameter as FundraisingModel;

            await Navigation.PushAsync(new FundraisingViews.FundraisingDetailsPage(model.ID));
        }

        private async void OnClicked_Share(object sender, EventArgs e)
        {
            var details = ((ImageButton)sender).CommandParameter as FundraisingModel;
            await ShareUri(details.Url, details.Name);
        }

        #endregion
    }

    #region View Model

    public class FundraisingViewModel
    {
        public List<FundraisingModel> Items { get; set; }
    }

    #endregion
}