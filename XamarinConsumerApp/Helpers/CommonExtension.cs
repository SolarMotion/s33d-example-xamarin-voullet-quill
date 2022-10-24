using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

using XamarinConsumerApp.Models;

namespace XamarinConsumerApp.Helpers
{
    public static class CommonExtension
    {
        public static PickerModel GetPickerValue(Picker picker)
        {
            return (PickerModel)picker?.SelectedItem ?? new PickerModel();
        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static int ToInt(this string value)
        {
            int.TryParse(value, out int number);

            return number;
        }

        public static DateTime ToDateTime(this string value)
        {
            var isDate = DateTime.TryParse(value, out DateTime result);

            return isDate ? result : new DateTime(1970, 1, 1);
        }

        public static ImageSource GetImageFromBase64(string imageBase64)
        {
            var byteArray = Convert.FromBase64String(imageBase64);

            var stream = new MemoryStream(byteArray);
            return ImageSource.FromStream(() => stream);
        }
    }
}
