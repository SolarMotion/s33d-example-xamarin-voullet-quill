using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamarinConsumerApp.Helpers;
using XamarinConsumerApp.Models;
using static XamarinConsumerApp.Helpers.AlertProvider;
using static XamarinConsumerApp.Helpers.CommonExtension;
using Acr.UserDialogs;
using XamarinConsumerApp.Resources;
using XamarinConsumerApp.ApiServices;
using XamarinConsumerApp.Enums;

namespace XamarinConsumerApp.Views.AccountViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountChangePasswordPage : BaseViews.BaseContentPage
    {
        public AccountChangePasswordPage(AccountDetailsModel accountDetails)
        {
            InitializeComponent();
        }

        private async void OnClicked_Update(object sender, EventArgs e)
        {
            if (txtNewPassword.Text.IsEmpty() || !txtNewPassword.Text.Equals(txtConfirmNewPassword.Text))
            {
                await ErrorAlert(this, "New password not matched.");
                return;
            }

            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                var changePasswordResponse = await AuthenticationService.ChangePasswordApi(new AuthenticationService.ChangePasswordRequest()
                {
                    UserDetail = new AuthenticationService.ChangePasswordItem()
                    {
                        LoginID = DB.GetValue(StorageEnum.Email),
                        Password = txtCurrentPassword.Text,
                        NewPassword = txtNewPassword.Text,
                    }
                });

                if (changePasswordResponse.Error.IsEmpty())
                {
                    txtCurrentPassword.Text = "";
                    txtNewPassword.Text = "";
                    txtConfirmNewPassword.Text = "";

                    await SuccessAlert(this, $"Password updated successfully.");
                }
                else
                {
                    await ErrorAlert(this, changePasswordResponse.Error);
                }
            }
        }
    }
}