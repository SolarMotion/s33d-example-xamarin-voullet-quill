using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

using XamarinConsumerApp.Enums;

namespace XamarinConsumerApp.Helpers
{
    public static class DB
    {
        public static void SetValue(StorageEnum storageEnum, string value)
        {
            Preferences.Set(storageEnum.ToString(), value);
        }

        public static string GetValue(StorageEnum storageEnum)
        {
            return Preferences.Get(storageEnum.ToString(), "EMPTY");
        }
    }
}
