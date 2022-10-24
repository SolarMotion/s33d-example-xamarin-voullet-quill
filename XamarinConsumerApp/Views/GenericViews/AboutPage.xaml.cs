using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinConsumerApp.Views.GenericViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : BaseViews.BaseContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private async void OnClicked_Close(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}