using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinConsumerApp.Helpers;

namespace XamarinConsumerApp.Models
{
    public class FundraisingModel
    {
        public string Url { get; set; }

        public string Image { get; set; }

        public string ImageType { get; set; }

        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public string ValidTill { get; set; }

        public DateTime? ValidTillDate { get; set; }

        // TODO: Change to better method
        public ImageSource ImageSource
        {
            get
            {
                return CommonExtension.GetImageFromBase64(Image);
            }
        }
    }

    public class FundraisingListItem
    {
        public string Url { get; set; }

        public string Image { get; set; }

        public string ImageType { get; set; }

        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public string ValidTill { get; set; }

        public DateTime? ValidTillDate { get; set; }
    }

    public class FundraisingDetailItem
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int ID { get; set; }

        public string Image { get; set; }

        public string ImageType { get; set; }

        public string ValidTill { get; set; }

        public DateTime? ValidTillDate { get; set; }

        // TODO: Change to better method
        public ImageSource ImageSource
        {
            get
            {
                return CommonExtension.GetImageFromBase64(Image);
            }
        }
    }

    public class FundraisingDetail
    {
        public string Url { get; set; }
        
        public List<FundraisingDetailItem> Items { get; set; }
        
        public int ID { get; set; }
        
        public string Image { get; set; }
        
        public string ImageType { get; set; }
        
        public string Name { get; set; }
        
        public string Address { get; set; }
        
        public string ContectNumber { get; set; }
        
        public string Purpose { get; set; }
        
        public string Amount { get; set; }
        
        public string End { get; set; }

        public int Count { get; set; }

        public string Error { get; set; }

        // TODO: Change to better method
        public ImageSource ImageSource
        {
            get
            {
                return CommonExtension.GetImageFromBase64(Image);
            }
        }
    }
}
