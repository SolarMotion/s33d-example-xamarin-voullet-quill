using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using XamarinConsumerApp.Helpers;
using XamarinConsumerApp.Enums;
using System.Security.Cryptography;

namespace XamarinConsumerApp.Helpers
{
    public static class CustomHttpClient
    {
        private static readonly HttpClient _client = new HttpClient();
        private static readonly string _baseUrl = "http://charitymobileservice.azurewebsites.net/consumerservice.svc";
        private static readonly string _secretKey = "Gk8OlaAj14";

        public static Task<HttpResponseMessage> CustomHttpPost(Object request, string url)
        {
            var unixTimeStamp = GetUnixTimestamp().ToString();
            var userID = DB.GetValue(StorageEnum.UserID);
            var hashedToken = CreateHashedToken(userID + unixTimeStamp, _secretKey);

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("DeviceId", userID);
            _client.DefaultRequestHeaders.Add("TimeStamp", unixTimeStamp);
            _client.DefaultRequestHeaders.Add("HashInput", hashedToken);

            var requestObject = new RequestObject() { request  = request };
            var requestBody = JsonConvert.SerializeObject(requestObject);
            var requestContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

            return _client.PostAsync($"{_baseUrl}{url}", requestContent);
        }

        public static async Task<T> Post<T>(Object request, string url)
        {
            var response = await CustomHttpPost(request, url);

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        class RequestObject
        {
            public Object request { get; set; }
        }

        static long GetUnixTimestamp()
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((DateTime.UtcNow - epoch).TotalMilliseconds);
        }

        static string CreateHashedToken(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
    }
}
