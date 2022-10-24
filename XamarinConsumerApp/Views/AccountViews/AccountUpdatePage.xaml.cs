using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamarinConsumerApp.ApiServices;
using XamarinConsumerApp.Models;
using XamarinConsumerApp.Enums;
using XamarinConsumerApp.Resources;
using static XamarinConsumerApp.Helpers.AlertProvider;
using static XamarinConsumerApp.Helpers.PickerProvider;
using static XamarinConsumerApp.Helpers.CommonExtension;
using XamarinConsumerApp.Helpers;

namespace XamarinConsumerApp.Views.AccountViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountUpdatePage : BaseViews.BaseContentPage
    {
        AccountUpdateViewModel _viewModel;
        AccountDetailsModel _accountDetails;

        public class AccountUpdateViewModel
        {
            public List<PickerModel> Genders { get; set; } = GenderPicker();
            public List<PickerModel> States { get; set; } = new List<PickerModel>();
            public List<PickerModel> Countries { get; set; } = CountryPicker();

            public AccountDetailsModel Account { get; set; } = new AccountDetailsModel();
        }

        protected async override void OnAppearing()
        {
            _viewModel = new AccountUpdateViewModel()
            {
                Account = _accountDetails,
                States = await StatePicker(),
            };
            this.BindingContext = _viewModel;

            pickerGender.SelectedItem = _viewModel.Genders.FirstOrDefault(a => a.Text == _accountDetails.Gender);
            pickerState.SelectedItem = _viewModel.States.FirstOrDefault(a => a.Text == _accountDetails.StateName);
            //pickerCountry.SelectedItem = _viewModel.Countries.FirstOrDefault(a => a.Text == _accountDetails.Contry);
        }

        public AccountUpdatePage(AccountDetailsModel accountDetails)
        {
            InitializeComponent();

            _accountDetails = accountDetails;
        }

        private async void OnClicked_Update(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                var request = new AuthenticationService.UserUpdateRequest()
                {
                    userID = DB.GetValue(StorageEnum.UserID).ToInt(),
                    UserDetails = new AuthenticationService.UserDetails()
                    {
                        Name = txtName.Text,
                        NRIC = txtIDNo.Text,
                        DOB = datePickerDOB.Date.ToString("yyyy-MM-dd"),
                        Gender = GetPickerValue(pickerGender).StringValue,
                        Mobile = txtMobileNo.Text,
                        Address1 = txtAddress1.Text,
                        Address2 = txtAddress2.Text,
                        Address3 = txtAddress3.Text,
                        Postcode = txtPostcode.Text,
                        TownCity = txtCity.Text,
                        StateID = GetPickerValue(pickerState).Value,
                    },
                };
                var createAccountResponse = await AuthenticationService.UserUpdateApi(request);

                if (createAccountResponse.Error.IsEmpty())
                {
                    await SuccessAlert(this, $"User details updated.");
                }
                else
                {
                    await ErrorAlert(this, "Backend API un-sync.");
                    //await ErrorAlert(this, createAccountResponse.Error);
                }
            }
        }
    }
}