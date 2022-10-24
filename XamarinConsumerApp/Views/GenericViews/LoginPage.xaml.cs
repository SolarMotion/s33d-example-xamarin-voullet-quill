using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading;

using Acr.UserDialogs;
using XamarinConsumerApp.Resources;
using XamarinConsumerApp.Helpers;
using XamarinConsumerApp.Enums;
using XamarinConsumerApp.ApiServices;
using Xamarin.Essentials;
using static XamarinConsumerApp.Helpers.AlertProvider;

namespace XamarinConsumerApp.Views.GenericViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
                ErrorAlert(this, "Connection to internet is not available.");

            InitializeComponent();
        }

        async void OnClicked_LoginButton(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                var email = txtEmail.Text;
                var password = txtPassword.Text;

                var loginResponse = await ValidateAuth(email, password);

                if (loginResponse.IsFirstLogin)
                {
                    var verificationCode = await DisplayPromptAsync(
                        "Enter Verification Code", 
                        "Verification code is required for first time login", 
                        "OK", "Cancel", null, -1, null, "");

                    loginResponse = await ValidateAuth(email, password, verificationCode);
                }

                if (!loginResponse.Valid)
                {
                    await AlertProvider.ErrorAlert(this, loginResponse.Error);
                }
                else
                {
                    DB.SetValue(StorageEnum.Email, email);
                    DB.SetValue(StorageEnum.UserID, loginResponse.UserId.ToString());

                    Application.Current.MainPage = new NavigationPage(new MainPage());
                }
            }
        }

        private Task<AuthenticationService.ValidateUserResponse> ValidateAuth(string email, string password, string verificationCode = "")
        {
            return AuthenticationService.ValidateUserApi(new AuthenticationService.ValidateUserRequest()
            {
                Username = email,
                Password = password,
                VerificationCode = verificationCode,
            });
        }

        private async void OnTapped_ForgotPassword(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPasswordPage());
        }

        async void OnTapped_Register(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }
    }
}