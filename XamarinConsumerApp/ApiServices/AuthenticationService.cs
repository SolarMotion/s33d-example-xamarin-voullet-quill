using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using XamarinConsumerApp.Helpers;
using XamarinConsumerApp.Models;

namespace XamarinConsumerApp.ApiServices
{
    public static class AuthenticationService
    {
        #region Validate User

        public static async Task<ValidateUserResponse> ValidateUserApi(ValidateUserRequest request)
        {
            return await CustomHttpClient.Post<ValidateUserResponse>(request, "/ValidateUser");
        }

        public class ValidateUserRequest : BaseRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string VerificationCode { get; set; }
        }

        public class ValidateUserResponse : BaseResponse
        {
            public int UserId { get; set; }
            public string Name { get; set; }
            public bool Valid { get; set; }
            public bool IsFirstLogin { get; set; }
        }

        #endregion

        #region Create Account

        public static async Task<CreateAccountResponse> CreateAccountApi(CreateAccountRequest request)
        {
            return await CustomHttpClient.Post<CreateAccountResponse>(request, "/CreateAccount");
        }

        public class CreateAccountRequest : BaseRequest
        {
            public string Password { get; set; }
            public string Username { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
            public string Birthday { get; set; }
            public string Gender { get; set; }
            public string IDNo { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Address3 { get; set; }
            public string Postcode { get; set; }
            public string TownCity { get; set; }
            public int StateID { get; set; }
        }

        public class CreateAccountResponse : BaseResponse
        {
            public int UserId { get; set; }
        }

        #endregion

        #region Forgot Password

        public static async Task<ForgotPasswordResponse> ForgotPasswordApi(ForgotPasswordRequest request)
        {
            return await CustomHttpClient.Post<ForgotPasswordResponse>(request, "/ForgotPassword");
        }

        public class ForgotPasswordRequest : BaseRequest
        {
            public string Username { get; set; }
        }

        public class ForgotPasswordResponse : BaseResponse
        {
            public bool Success { get; set; }
        }

        #endregion

        #region Change Password

        public static async Task<ChangePasswordResponse> ChangePasswordApi(ChangePasswordRequest request)
        {
            return await CustomHttpClient.Post<ChangePasswordResponse>(request, "/UpdateLoginInformation");
        }

        public class ChangePasswordRequest : BaseRequest
        {
            public ChangePasswordItem UserDetail { get; set; } = new ChangePasswordItem();
        }

        public class ChangePasswordResponse : BaseResponse
        {
        }

        public class ChangePasswordItem
        {
            public string LoginID { get; set; }
            public string Password { get; set; }
            public string NewPassword { get; set; }
        }

        #endregion

        #region User Details

        public static async Task<UserDetailsResponse> UserDetailsApi(UserDetailsRequest request)
        {
            return await CustomHttpClient.Post<UserDetailsResponse>(request, "/GetUserDetail");
        }

        public class UserDetailsRequest : BaseRequest
        {
            public int userID { get; set; }
        }

        public class UserDetailsResponse : UserDetails
        {
        }

        public class UserDetails : AccountDetailsModel
        {
        }

        #endregion

        #region User Update

        public static async Task<UserUpdateResponse> UserUpdateApi(UserUpdateRequest request)
        {
            return await CustomHttpClient.Post<UserUpdateResponse>(request, "/UpdateUserDetail");
        }
        
        public class UserUpdateRequest : BaseRequest
        {
            public int userID { get; set; }

            public UserDetails UserDetails { get; set; } = new UserDetails();
        }

        public class UserUpdateResponse : UserDetails
        {
        }

        #endregion
    }
}
