using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinConsumerApp.ApiServices;
using XamarinConsumerApp.Resources;
using static XamarinConsumerApp.Helpers.AlertProvider;
using static XamarinConsumerApp.Helpers.CommonExtension;
using XamarinConsumerApp.Helpers;
using XamarinConsumerApp.Enums;

namespace XamarinConsumerApp.Views.DownloadViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DownloadInputCodePage : BaseViews.BaseContentPage
    {
        protected override void OnAppearing()
        {
        }

        public DownloadInputCodePage()
        {
            InitializeComponent();
        }

        private async void OnClick_InputCode(object sender, EventArgs e)
        {
            var code = txtCode.Text;
            if (code.IsEmpty())
            {
                await ErrorAlert(this, "Code is required.");
                return;
            }

            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                var downloadResponse = await MiscellaneousService.DownloadInputCodeApi(new MiscellaneousService.DownloadRequest()
                {
                    ID = 0,
                    Code = code,
                    UserID = DB.GetValue(StorageEnum.UserID).ToInt(),
                    GrabMethod = "INPUT_VOUCHER",
                }); ;

                if (downloadResponse.Error.IsEmpty())
                {
                    await SuccessAlert(this, "Code grab success.");
                }
                else
                {
                    txtCode.Text = "";
                    await ErrorAlert(this, downloadResponse.Error);
                }
            }
        }

        private async void OnClick_InputCampaign(object sender, EventArgs e)
        {
            var campaign = txtCampaign.Text;
            if (campaign.IsEmpty())
            {
                await ErrorAlert(this, "Campaign code is required.");
                return;
            }

            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                var downloadResponse = await MiscellaneousService.DownloadInputCodeApi(new MiscellaneousService.DownloadRequest()
                {
                    ID = 0,
                    Code = campaign,
                    UserID = DB.GetValue(StorageEnum.UserID).ToInt(),
                    GrabMethod = "INPUT_CAMPAIGN",
                }); ;

                if (downloadResponse.Error.IsEmpty())
                {
                    await SuccessAlert(this, "Campaign grab success.");
                }
                else
                {
                    txtCampaign.Text = "";
                    await ErrorAlert(this, downloadResponse.Error);
                }
            }
        }
    }
}