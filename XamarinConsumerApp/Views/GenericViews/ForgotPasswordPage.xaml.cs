using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using static XamarinConsumerApp.Helpers.AlertProvider;
using Acr.UserDialogs;
using XamarinConsumerApp.Resources;
using XamarinConsumerApp.ApiServices;
using static XamarinConsumerApp.Helpers.CommonExtension;

namespace XamarinConsumerApp.Views.GenericViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : BaseViews.BaseContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        async void OnClicked_ForgotPasswordButton(object sender, EventArgs e)
        {
            var email = txtEmail.Text;

            if (email.IsEmpty())
            {
                await ErrorAlert(this, $"Email is required.");
                return;
            }

            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                var forgotPasswordResponse = await AuthenticationService.ForgotPasswordApi(new AuthenticationService.ForgotPasswordRequest()
                {
                    Username = email,
                });

                if (forgotPasswordResponse.Success)
                {
                    txtEmail.Text = "";
                    await SuccessAlert(this, $"Password reset success. Please check your email.");
                }
                else
                {
                    await ErrorAlert(this, forgotPasswordResponse.Error);
                }           
            }
        }
    }
}