using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinConsumerApp.Helpers;

namespace XamarinConsumerApp.Models
{
    public class BulletinModel
    {
        public int LnkBulletinID { get; set; }
        public int BulletinID { get; set; }
        public Byte[] Image { get; set; }
        public string ImageStr { get; set; }
        public string ImageType { get; set; }
        public string Error { get; set; }

        public ImageSource ImageSource
        {
            get
            {
                return ImageStr.IsEmpty() ? null : CommonExtension.GetImageFromBase64(ImageStr);
            }
        }

        //CHIEN: delete below if not using
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
