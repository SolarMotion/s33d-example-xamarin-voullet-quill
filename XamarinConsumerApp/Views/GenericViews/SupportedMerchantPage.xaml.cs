using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Acr.UserDialogs;
using XamarinConsumerApp.ApiServices;
using XamarinConsumerApp.Helpers;
using XamarinConsumerApp.Models;
using XamarinConsumerApp.Resources;
using static XamarinConsumerApp.Helpers.AlertProvider;

namespace XamarinConsumerApp.Views.GenericViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SupportedMerchantPage : BaseViews.BaseContentPage
    {
        SupportedMerchantViewModel _viewModel;

        public class SupportedMerchantViewModel
        {
            public List<SupportedMerchantModel> Merchants { get; set; } = new List<SupportedMerchantModel>();
            //public List<SupportedMerchantModel> Merchants { get; set; } = new List<SupportedMerchantModel>()
            //{
            //    new SupportedMerchantModel()
            //{
            //    ID = 1,
            //        Image = "home_slider1.jpg",
            //    },
            //    new SupportedMerchantModel()
            //{
            //    ID = 1,
            //        Image = "home_slider1.jpg",
            //    },
            //    new SupportedMerchantModel()
            //{
            //    ID = 1,
            //        Image = "home_slider1.jpg",
            //    },
            //    new SupportedMerchantModel()
            //{
            //    ID = 1,
            //        Image = "home_slider1.jpg",
            //    },
            //    new SupportedMerchantModel()
            //{
            //    ID = 1,
            //        Image = "home_slider1.jpg",
            //    },
            //    new SupportedMerchantModel()
            //{
            //    ID = 1,
            //        Image = "home_slider1.jpg",
            //    },
            //    new SupportedMerchantModel()
            //{
            //    ID = 1,
            //        Image = "home_slider1.jpg",
            //    },
            //    new SupportedMerchantModel()
            //{
            //    ID = 1,
            //        Image = "home_slider1.jpg",
            //    },
            //};
        }

        protected async override void OnAppearing()
        {
            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                var supportedMerchantResponse = await MiscellaneousService.SupportedMerchantApi();

                if (supportedMerchantResponse.Error.IsEmpty())
                {
                    _viewModel = new SupportedMerchantViewModel();

                    _viewModel.Merchants = supportedMerchantResponse.ImageList.Select(a => new SupportedMerchantModel()
                    {
                        Image = a.Image,
                        ImageType = a.ImageType,
                    }).ToList();
                }
                else
                {
                    _viewModel = new SupportedMerchantViewModel();
                    await ErrorAlert(this, supportedMerchantResponse.Error);
                }

                //await Task.Delay(1000);

                this.BindingContext = _viewModel;
            }
        }

        public SupportedMerchantPage()
        {
            InitializeComponent();
        }

        private async void OnRefreshing_MerchantCollectionView(object sender, EventArgs e)
        {
            await Task.Delay(1000);

            var result = _viewModel;
            collectionViewMerchant.ItemsSource = result.Merchants;

            refreshViewMerchant.IsRefreshing = false;
        }

        private async void OnClicked_Close(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}