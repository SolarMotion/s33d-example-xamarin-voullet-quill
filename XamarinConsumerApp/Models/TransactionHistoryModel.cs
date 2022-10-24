using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinConsumerApp.Models
{
    //CHIEN: delete
    //public class TransactionHistoryModel
    //{
    //    public int ID { get; set; }
    //    public string VoucherName { get; set; }
    //    public string TransactionDate { get; set; }
    //    public string TransactionTime { get; set; }
    //    public string Status { get; set; }
    //}

    public class TransactionHistoryModel
    {
        public string Image { get; set; }

        public string ImageType { get; set; }

        public byte[] ImageByte { get; set; }

        public string Status { get; set; }

        public string StatusCode { get; set; }

        public int StatusID { get; set; }

        public int ID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string RedeemAt { get; set; }

        public string RedeemOn { get; set; }

        public DateTime? RedeemOnDate { get; set; }

        public string ExpiredOn { get; set; }

        public DateTime? ExpiredOnDate { get; set; }

        public string RedeemTime { get; set; }
    }
}
