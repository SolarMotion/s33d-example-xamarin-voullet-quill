using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using XamarinConsumerApp.Models;
using static XamarinConsumerApp.ApiServices.MiscellaneousService;

namespace XamarinConsumerApp.Helpers
{
    public static class PickerProvider
    {
        public static List<PickerModel> GenderPicker()
        {
            return GenderListingApi().Genders.Select(a => new PickerModel()
            {
                Value = a.ID,
                StringValue = a.Name,
                Text = a.Name,
            }).ToList();
        }

        public static async Task<List<PickerModel>> StatePicker()
        {
            var stateResponse = await StateListingApi();
            return stateResponse.Items.Select(a => new PickerModel()
            {
                Value = a.ID,
                StringValue = a.Name,
                Text = a.Name,
            }).ToList();
        }

        public static List<PickerModel> CountryPicker()
        {
            return CountryListingApi().Countries.Select(a => new PickerModel()
            {
                Value = a.ID,
                StringValue = a.Name,
                Text = a.Name,
            }).ToList();
        }
    }
}
