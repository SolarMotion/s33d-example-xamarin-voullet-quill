using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamarinConsumerApp.Models;
using static XamarinConsumerApp.Helpers.AlertProvider;

namespace XamarinConsumerApp.Views.AccountViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountProfilePicturePage : BaseViews.BaseContentPage
    {
        AccountDetailsModel _accountDetails;
        AccountProfilePictureViewModel _viewModel;

        public class AccountProfilePictureViewModel
        {
            public AccountDetailsModel Account { get; set; } = new AccountDetailsModel();
        }

        protected override void OnAppearing()
        {
            _viewModel = new AccountProfilePictureViewModel() { Account = _accountDetails };
            this.BindingContext = _viewModel;
        }

        public AccountProfilePicturePage(AccountDetailsModel accountDetails)
        {
            InitializeComponent();

            _accountDetails = accountDetails;
        }

        private async void OnClicked_Upload(object sender, EventArgs e)
        {
            await SuccessAlert(this, "Upload");
        }

        private async void OnClicked_TakePhoto(object sender, EventArgs e)
        {
            await SuccessAlert(this, "Take Photo");
        }

        private async void OnClicked_Update(object sender, EventArgs e)
        {
            var source = imageProfilePicture.Source; //as FileImageSource;

            if (source != null)
            {
                //var fileName = source.File;
                await SuccessAlert(this, $"Picture updated.");
            }
            else
            {
                await ErrorAlert(this, "Picture error");
            }
        }
    }
}