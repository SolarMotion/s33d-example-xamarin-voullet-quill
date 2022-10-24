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

namespace XamarinConsumerApp.Views.AccountViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountIndexPage : BaseViews.BaseContentPage
    {
        AccountIndexViewModel _viewModel;

        public class AccountIndexViewModel
        {
            public AccountDetailsModel Account { get; set; } = new AccountDetailsModel();
            //{
            //    ID = 1,
            //    Email = "abc@abc.com",
            //    Image = "home_slider1.jpg",
            //    Name = "User Name 1",
            //    DOB = new DateTime(1997, 1, 1),
            //    IDNo = "19970101148888",
            //    Gender = "Male",
            //    MobileNo = "0123456789",

            //    FullAddress = "Address 1 Address 2 Address 3",
            //    Address1 = "Address 1",
            //    Address2 = "Address 2",
            //    Address3 = "Address 3",
            //    City = "Bukit Bintang",
            //    Postcode = "55100",
            //    State = "Kuala Lumpur",
            //    Country = "Malaysia",
            //};
        }

        protected async override void OnAppearing()
        {
            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                _viewModel = new AccountIndexViewModel();
                var userDetailsResponse = await AuthenticationService.UserDetailsApi(new AuthenticationService.UserDetailsRequest()
                {
                    userID = DB.GetValue(StorageEnum.UserID).ToInt(),
                });

                if (userDetailsResponse.Error.IsEmpty())
                {
                    _viewModel.Account = userDetailsResponse;
                }
                else
                {
                    await ErrorAlert(this, userDetailsResponse.Error);
                }

                this.BindingContext = _viewModel;
            }

            //_viewModel = new AccountIndexViewModel();
            //this.BindingContext = _viewModel;
        }

        public AccountIndexPage()
        {
            InitializeComponent();
        }

        private async void OnTapped_ProfilePicture(object sender, EventArgs e)
        {
            var args = (TappedEventArgs)e;
            var accountDetails = args.Parameter as AccountDetailsModel;

            await Navigation.PushAsync(new AccountProfilePicturePage(accountDetails));
        }

        private async void OnClicked_UpdateProfile(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountUpdatePage(_viewModel.Account));
        }

        private async void OnClicked_ChangePassword(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountChangePasswordPage(_viewModel.Account));
        }
    }
}