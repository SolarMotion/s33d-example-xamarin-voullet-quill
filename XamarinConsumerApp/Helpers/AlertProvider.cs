using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinConsumerApp.Helpers
{
    public static class AlertProvider
    {
        public static Task SuccessAlert(Page page, string message = "Default message")
        {
            return page.DisplayAlert("Success", message, "OK");
        }

        public static Task ErrorAlert(Page page, string message = "Default message")
        {
            return page.DisplayAlert("Error", message, "OK");
        }
    }
}
