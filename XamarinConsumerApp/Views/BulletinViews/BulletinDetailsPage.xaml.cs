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
using XamarinConsumerApp.Helpers;
using XamarinConsumerApp.Resources;
using static XamarinConsumerApp.Helpers.AlertProvider;
using static XamarinConsumerApp.Helpers.CommonExtension;

namespace XamarinConsumerApp.Views.BulletinViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BulletinDetailsPage : BaseViews.BaseContentPage
    {
        BulletinDetailsViewModel _viewModel = new BulletinDetailsViewModel();
        int _bulletinID = 0;
        string _imageStr = "";

        public class BulletinDetailsViewModel
        {
            public BulletinModel Bulletin { get; set; } = new BulletinModel();
        }

        protected async override void OnAppearing()
        {
            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                var bulletinDetailsRequest = new MiscellaneousService.BulletinDetailsRequest()
                {
                    BulletinID = _bulletinID,
                    UserId = DB.GetValue(StorageEnum.UserID).ToInt(),
                };
                var bulletinDetailsResponse = await MiscellaneousService.BulletinDetailsApi(bulletinDetailsRequest);
                if (!bulletinDetailsResponse.Error.IsEmpty())
                {
                    await ErrorAlert(this, bulletinDetailsResponse.Error);
                }
                else
                {
                    _viewModel.Bulletin.ImageStr = _imageStr;
                    _viewModel.Bulletin.Name = bulletinDetailsResponse.Subject;
                    _viewModel.Bulletin.Description = bulletinDetailsResponse.Body;
                }
            }

            this.BindingContext = _viewModel;
        }

        public BulletinDetailsPage(int bulletinID, string imageStr)
        {
            InitializeComponent();

            _bulletinID = bulletinID;
            _imageStr = imageStr;
        }
    }
}