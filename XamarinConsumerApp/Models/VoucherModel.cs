using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinConsumerApp.Helpers;

namespace XamarinConsumerApp.Models
{
    public class VoucherModel
    {
        public string OrgName { get; set; }
        public string VoucherLogoStr { get; set; }
        public int VoucherID { get; set; }
        public string VoucherDescription { get; set; }
        public string RedeemMethod { get; set; }
        public string VoucherName { get; set; }
        public string VoucherCode { get; set; }
        public string VoucherCodeBase64 { get; set; }

        public ImageSource ImageSource
        {
            get
            {
                return VoucherLogoStr.IsEmpty() ? null : CommonExtension.GetImageFromBase64(VoucherLogoStr);
            }
        }

        public ImageSource LogoSource
        {
            get
            {
                return LogoStr.IsEmpty() ? null : CommonExtension.GetImageFromBase64(LogoStr);
            }
        }

        public ImageSource VoucherCodeSource
        {
            get
            {
                return VoucherCodeBase64.IsEmpty() ? null : CommonExtension.GetImageFromBase64(VoucherCodeBase64);
            }
        }

        public List<string> TnCList { get; set; }
        public string Url { get; set; }
        public int VoucherDesignID { get; set; }
        //public Byte[] Logo { get; set; }
        public string LogoStr { get; set; }
        public string ImageType { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public DateTime? ValidUntil { get; set; }
        public string ValidUntilStr { get; set; }
        public string SatusCode { get; set; }
        public List<RedemptionPoint> RedemptionPoints { get; set; } = new List<RedemptionPoint>();
        //public int VoucherRedeemQty { get; set; }
        //public int VoucherDesignRedeemQty { get; set; }


        //CHIEN: delete below if not using
        public int ID { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ValidUntilDate { get; set; }
    }

    public class RedemptionPoint
    {
        public string information { get; set; }
        public string contactNo { get; set; }
        public string address { get; set; }
        public string town { get; set; }
        public string state { get; set; }

        public string FullAddress
        {
            get
            {
                return $"{address} {town} {state}";
            }
        }

        //CHIEN: delete below if not using
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactNo { get; set; }
    }
}
