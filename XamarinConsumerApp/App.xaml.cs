using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using static XamarinConsumerApp.Helpers.CommonExtension;
using XamarinConsumerApp.Views.GenericViews;
using XamarinConsumerApp.Helpers;
using XamarinConsumerApp.Enums;

namespace XamarinConsumerApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Page page;

            if (DB.GetValue(StorageEnum.UserID).ToInt() == 0)
                page = new NavigationPage(new LoginPage());
            else
                //page = new MainPage();
                page = new NavigationPage(new MainPage());

            MainPage = page;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static void SetDetailPage(Page page)
        {
            if (App.Current.MainPage is MasterDetailPage)
            {
                var masterPage = (MasterDetailPage)App.Current.MainPage;
                masterPage.Detail = new NavigationPage(page);
            }
        }
    }
}
