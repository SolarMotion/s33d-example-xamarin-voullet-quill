using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using XamarinConsumerApp.Helpers;
using XamarinConsumerApp.Models;

namespace XamarinConsumerApp.ApiServices
{
    public static class MiscellaneousService
    {
        #region Gender

        public static GenderListingResponse GenderListingApi()
        {
            return new GenderListingResponse
            {
                Genders = new List<GenderListingItem>()
                {
                    new GenderListingItem()
                    {
                        ID = 1,
                        Name = "Male",
                    },
                    new GenderListingItem()
                    {
                        ID= 2,
                        Name = "Female",
                    },
                },
            };
        }

        public class GenderListingRequest : BaseRequest
        {

        }

        public class GenderListingResponse : BaseResponse
        {
            public List<GenderListingItem> Genders { get; set; } = new List<GenderListingItem>();
        }

        public class GenderListingItem
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        #endregion

        #region Country

        public static CountryListingResponse CountryListingApi()
        {
            return new CountryListingResponse
            {
                Countries = new List<CountryListingItem>()
                {
                    new CountryListingItem()
                    {
                        ID = 1,
                        Name = "Malaysia",
                    },
                },
            };
        }

        public class CountryListingRequest : BaseRequest
        {

        }

        public class CountryListingResponse : BaseResponse
        {
            public List<CountryListingItem> Countries { get; set; } = new List<CountryListingItem>();
        }

        public class CountryListingItem
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        #endregion

        #region State

        public static async Task<StateListingResponse> StateListingApi()
        {
            return await CustomHttpClient.Post<StateListingResponse>(null, "/GetState");
        }

        public class StateListingRequest : BaseRequest
        {
        }

        public class StateListingResponse : BaseResponse
        {
            public List<StateListingItem> Items { get; set; } = new List<StateListingItem>();
        }

        public class StateListingItem
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        #endregion

        #region Supported Merchant

        public static async Task<SupportedMerchantResponse> SupportedMerchantApi()
        {
            return await CustomHttpClient.Post<SupportedMerchantResponse>(null, "/ConsumerGetWeSupport");
        }

        public class SupportedMerchantRequest : BaseRequest
        {
        }

        public class SupportedMerchantResponse : BaseResponse
        {
            public List<SupportedMerchantItem> ImageList { get; set; } = new List<SupportedMerchantItem>();
        }

        public class SupportedMerchantItem
        {
            public string Image { get; set; }

            public string ImageType { get; set; }
        }

        #endregion

        #region Highlight

        public static async Task<HighlightSummaryResponse> HighlightSummaryApi()
        {
            return await CustomHttpClient.Post<HighlightSummaryResponse>(null, "/getHighlight");
        }

        public class HighlightSummaryRequest : BaseRequest
        {
        }

        public class HighlightSummaryResponse : BaseResponse
        {
            public List<VoucherModel> popularItemList { get; set; } = new List<VoucherModel>();

            public List<VoucherModel> newItemList { get; set; } = new List<VoucherModel>();
        }

        public static async Task<HighlightAllResponse> HighlightAllApi(HighlightAllRequest request)
        {
            return await CustomHttpClient.Post<HighlightAllResponse>(null, "/getHighlightAllListing");
        }

        public class HighlightAllRequest : BaseRequest
        {
            public bool IsPopular { get; set; } = true;
        }

        public class HighlightAllResponse : BaseResponse
        {
            public List<VoucherModel> allItemList { get; set; } = new List<VoucherModel>();
        }

        #endregion

        #region Bulletin

        public static async Task<BulletinIndexResponse> BulletinIndexApi()
        {
            return await CustomHttpClient.Post<BulletinIndexResponse>(null, "/GetBeanBagImagesSlide");
        }

        public class BulletinIndexRequest : BaseRequest
        {
        }

        public class BulletinIndexResponse : BaseResponse
        {
            public List<BulletinModel> BeanBagImagesSlideList { get; set; }
        }

        public static async Task<BulletinDetailsResponse> BulletinDetailsApi(BulletinDetailsRequest request)
        {
            return await CustomHttpClient.Post<BulletinDetailsResponse>(request, "/GetBulletinInfo");
        }

        public class BulletinDetailsRequest : BaseRequest
        {
            public int BulletinID { get; set; }

            public int UserId { get; set; }
        }

        public class BulletinDetailsResponse : BaseResponse
        {
            public string Subject { get; set; }

            public string Body { get; set; }
        }

        #endregion

        #region Coupon

        public static async Task<CouponConsumerListingResponse> CouponConsumerListingApi(CouponConsumerListingRequest request)
        {
            return await CustomHttpClient.Post<CouponConsumerListingResponse>(request, "/GetMyCouponList");
        }

        public class CouponConsumerListingRequest : BaseRequest
        {
            public string SearchName { get; set; }

            public string SortBy { get; set; }

            public int UserId { get; set; }
        }

        public class CouponConsumerListingResponse : BaseResponse
        {
            public List<VoucherModel> MyCouponList { get; set; }
        }

        public static async Task<CouponDeleteResponse> CouponDeleteApi(CouponDeleteRequest request)
        {
            return await CustomHttpClient.Post<CouponDeleteResponse>(request, "/DeleteBeanBagVoucherList");
        }

        public class CouponDeleteRequest
        {
            public int ID { get; set; }

            public int UserId { get; set; }
        }

        public class CouponDeleteResponse
        {
            public string Error { get; set; }
        }

        public static async Task<CouponDetailsResponse> CouponDetailsApi(CouponDetailsRequest request)
        {
            return await CustomHttpClient.Post<CouponDetailsResponse>(request, "/GetVoucherDetail");
        }

        public class CouponDetailsRequest : BaseRequest
        {
            public int ID { get; set; }
            public int VoucherID { get; set; }
        }

        public class CouponDetailsResponse : BaseResponse
        {
            public string url { get; set; }
            public string encodedURL { get; set; }
            public string Image { get; set; }
            public string ImageType { get; set; }
            public byte ImageByte { get; set; }
            public int ID { get; set; }
            public int VoucherID { get; set; }
            public string Name { get; set; }
            public string RedeemMethod { get; set; }
            public string Description { get; set; }
            public string ValidTill { get; set; }
            public DateTime? ValidTillDate { get; set; }
            public string Tnc { get; set; }
            public string VoucherTnc { get; set; }
            public int Count { get; set; }
            public string designType { get; set; }

            public List<RedemptionPoint> Redeem { get; set; }
            public List<string> TnCList { get; set; }
        }

        public static async Task<CouponRedeemDetailsResponse> CouponRedeemDetailsApi(CouponRedeemDetailsRequest request)
        {
            return await CustomHttpClient.Post<CouponRedeemDetailsResponse>(request, "/GetRedeemInfo");
        }

        public class CouponRedeemDetailsRequest:BaseRequest
        {
            public int VoucherID { get; set; }
            public int VoucherDesignID { get; set; }
            public int UserId { get; set; }
        }

        public class CouponRedeemDetailsResponse : BaseResponse
        {
            public VoucherModel RedeemInfo { get; set; } = new VoucherModel();
        }

        public static async Task<CouponRedeemResponse> CouponRedeemApi(CouponRedeemRequest request)
        {
            return await CustomHttpClient.Post<CouponRedeemResponse>(request, "/GetVoucherDetail");
        }

        public class CouponRedeemRequest : BaseRequest
        {
            public int VoucherID { get; set; }

            public int VoucherDesignID { get; set; }

            public string RedeemMethod { get; set; }

            public string VoucherCode { get; set; }

            public string MerchantID { get; set; }

            public int UserId { get; set; }
        }

        public class CouponRedeemResponse : BaseResponse
        {
            public bool isMaxUsage { get; set; } = false;
        }

        #endregion

        #region Download

        public static async Task<DownloadResponse> DownloadInputCodeApi(DownloadRequest request)
        {
            return await CustomHttpClient.Post<DownloadResponse>(request, "/grabCouponInput");
        }

        public static async Task<DownloadResponse> DownloadScanQRCodeApi(DownloadRequest request)
        {
            return await CustomHttpClient.Post<DownloadResponse>(request, "/grabCouponQRScan");
        }

        public class DownloadRequest : BaseRequest
        {
            public int ID { get; set; }

            public string Code { get; set; }

            public int UserID { get; set; }

            public string GrabMethod { get; set; }
        }

        public class DownloadResponse : BaseResponse
        {
            public bool isSuccess { get; set; }
        }

        #endregion

        #region Transaction History

        public static async Task<TransactionHistoryResponse> TransactionHistoryApi(TransactionHistoryRequest request)
        {
            return await CustomHttpClient.Post<TransactionHistoryResponse>(request, "/GetHistoryList");
        }

        public class TransactionHistoryRequest : BaseRequest
        {
            public int ID { get; set; }
        }

        public class TransactionHistoryResponse : BaseResponse
        {
            public List<TransactionHistoryModel> Expired { get; set; }

            public List<TransactionHistoryModel> Redeemed { get; set; }

            public int Count { get; set; }
        }

        #endregion

        #region Fundraising

        public static async Task<GetFundraisingListResponse> GetFundraisingList(GetFundraisingListRequest request)
        {
            return await CustomHttpClient.Post<GetFundraisingListResponse>(request, "/GetFundraisingList");
        }

        public static async Task<FundraisingDetail> GetFundraisingDetail(int id)
        {
            var request = new GetFundraisingDetailRequest { ID = id };
            return await CustomHttpClient.Post<FundraisingDetail>(request, "/GetFundraisingDetail");
        }

        public class GetFundraisingListRequest
        {
            public string SearchName { get; set; }
        }

        public class GetFundraisingListResponse
        {
            public List<FundraisingListItem> Items { get; set; }

            public int Count { get; set; }

            public string Error { get; set; }
        }

        public class GetFundraisingDetailRequest
        {
            public int ID { get; set; }
        }

        #endregion
    }
}
