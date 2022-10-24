using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using XamarinConsumerApp.Models;
using XamarinConsumerApp.Views;
using XamarinConsumerApp.Views.GenericViews;
using XamarinConsumerApp.Views.AccountViews;

namespace XamarinConsumerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            masterPage.listView.ItemSelected += OnItemSelected;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as SideMenuModel;
            if (item != null)
            {
                masterPage.listView.SelectedItem = null;
                IsPresented = false;

                if (item.TargetType == typeof(LogoutPage))
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    Preferences.Clear();
                }
                else if (item.TargetType == typeof(AccountIndexPage))
                {
                    //Detail =  new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                    Navigation.PushAsync((Page)Activator.CreateInstance(item.TargetType));
                }
                else
                {
                    //Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                    Navigation.PushModalAsync(new NavigationPage((Page)Activator.CreateInstance(item.TargetType)));
                }
            }
        }
    }
}
