using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinConsumerApp.Helpers;

namespace XamarinConsumerApp.Models
{
    public class SupportedMerchantModel
    {
        public int ID { get; set; }
        public string Image { get; set; }
        public string ImageType { get; set; }

        public ImageSource ImageSource
        {
            get
            {
                return CommonExtension.GetImageFromBase64(Image);
            }
        }
    }
}
