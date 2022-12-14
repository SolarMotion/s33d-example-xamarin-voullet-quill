using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinConsumerApp.Helpers;

namespace XamarinConsumerApp.Models
{
    public class AccountDetailsModel
    {
        public int userID { get; set; }
        public string LoginID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string NRIC { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Postcode { get; set; }
        public string TownCity { get; set; }
        public int? StateID { get; set; }
        public string StateName { get; set; }
        public string Contry { get; set; }
        public string Image { get; set; }
        public byte[] ImageByte { get; set; }
        public string Imagetype { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public int Count { get; set; }
        public string Error { get; set; } = "";
        public bool WrongPassword { get; set; }

        public string FullAddress
        {
            get
            {
                return $"{Address1} {Address2} {Address3}";
            }
        }

        [JsonIgnore]
        public ImageSource ImageSource
        {
            get
            {
                return Image == null ? 
                    CommonExtension.GetImageFromBase64("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") :
                    CommonExtension.GetImageFromBase64(Image);
            }
        }

        public DateTime DateOfBirth
        {
            get
            {
                return DOB.ToDateTime();
            }
        }
    }
}
