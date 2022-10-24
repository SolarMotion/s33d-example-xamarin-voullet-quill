using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamarinConsumerApp.Models;
using static XamarinConsumerApp.Helpers.PickerProvider;
using static XamarinConsumerApp.Helpers.CommonExtension;
using Acr.UserDialogs;
using XamarinConsumerApp.Resources;
using XamarinConsumerApp.ApiServices;
using static XamarinConsumerApp.Helpers.AlertProvider;

namespace XamarinConsumerApp.Views.GenericViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        RegistrationViewModel _viewModel;

        public class RegistrationViewModel
        {
            public List<PickerModel> Genders { get; set; } = GenderPicker();
            public List<PickerModel> States { get; set; } = new List<PickerModel>();
            public List<PickerModel> Countries { get; set; } = CountryPicker();
        }

        protected async override void OnAppearing()
        {
            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                _viewModel = new RegistrationViewModel()
                {
                    States = await StatePicker(),
                };
                this.BindingContext = _viewModel;
            }
        }

        public RegistrationPage()
        {
            InitializeComponent();
        }

        async void OnClicked_RegisterButton(object sender, EventArgs e)
        {
            if (!txtConfirmPassword.Text.Equals(txtPassword.Text))
            {
                await ErrorAlert(this, "Passwor and confirm password not matched.");
                return;
            }

            var dobDate = datePickerDOB.Date.ToString("yyyy-MM-dd");

            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                var createAccountResponse = await AuthenticationService.CreateAccountApi(new AuthenticationService.CreateAccountRequest()
                {
                    Address1 = txtAddress1.Text,
                    Address2 = txtAddress2.Text,
                    Address3 = txtAddress3.Text,
                    StateID = GetPickerValue(pickerState).Value,
                    Email = txtEmail.Text,
                    IDNo = txtIDNo.Text,
                    Gender = GetPickerValue(pickerGender).StringValue,
                    Birthday = dobDate,
                    Mobile = txtMobileNo.Text,
                    Name = txtName.Text,
                    Password = txtPassword.Text,
                    Postcode = txtPostcode.Text,
                    TownCity = txtCity.Text,
                    Username = txtEmail.Text,
                });

                if (createAccountResponse.Error.IsEmpty())
                {
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                    txtAddress3.Text = "";
                    pickerState.SelectedIndex = -1;
                    txtEmail.Text = "";
                    txtIDNo.Text = "";
                    pickerGender.SelectedIndex = -1;
                    datePickerDOB.Date = DateTime.Now;
                    txtMobileNo.Text = "";
                    txtName.Text = "";
                    txtPassword.Text = "";
                    txtConfirmPassword.Text = "";
                    txtPostcode.Text = "";
                    txtCity.Text = "";
                    txtEmail.Text = "";

                    await SuccessAlert(this, $"Verification Code has sent to your email. Kindly login for the first time and enter the Verification Code when prompt.");
                }
                else
                {
                    await ErrorAlert(this, createAccountResponse.Error);
                }
            }
        }
    }
}