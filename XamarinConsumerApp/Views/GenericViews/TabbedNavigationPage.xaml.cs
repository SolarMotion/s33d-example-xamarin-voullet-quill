using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamarinConsumerApp.Views;

namespace XamarinConsumerApp.Views.GenericViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedNavigationPage : TabbedPage
    {
        public TabbedNavigationPage()
        {
            InitializeComponent();
        }

        private void OnClicked_HighlightIcon(object sender, EventArgs e)
        {
            //App.SetDetailPage(new HighlightViews.HighlightSummaryListingPage());

            // Another way
            Navigation.PushAsync(new HighlightViews.HighlightSummaryListingPage());
        }
    }
}