using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinConsumerApp.Views.BaseViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            InitializeComponent();
        }

        public async Task ShareUri(string uri, string title)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = title
            });
        }
    }
}