using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinConsumerApp.Models
{
    public class PickerBaseModel
    {
        public string Text { get; set; }
        public int Value { get; set; }
        public string StringValue { get; set; }
    }

    public class PickerModel : PickerBaseModel
    {
    }
}
