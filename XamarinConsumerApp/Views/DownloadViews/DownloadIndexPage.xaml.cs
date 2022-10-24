using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using static XamarinConsumerApp.Helpers.AlertProvider;

namespace XamarinConsumerApp.Views.DownloadViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DownloadIndexPage : BaseViews.BaseContentPage
    {
        protected override void OnAppearing()
        {
        }

        public DownloadIndexPage()
        {
            InitializeComponent();
        }

        private async void OnClicked_InputCode(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DownloadInputCodePage());
        }

        private async void OnTapped_Program(object sender, EventArgs e)
        {
            await SuccessAlert(this, "Program scanned.");
        }

        private async void OnTapped_Campaign(object sender, EventArgs e)
        {
            await SuccessAlert(this, "Campaign scanned.");
        }
    }
}