using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinConsumerApp.ApiServices;
using XamarinConsumerApp.Models;
using XamarinConsumerApp.Resources;
using XamarinConsumerApp.Helpers;
using XamarinConsumerApp.Enums;
using static XamarinConsumerApp.Helpers.AlertProvider;

namespace XamarinConsumerApp.Views.VoucherViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VoucherRedeemByQRCodePage : BaseViews.BaseContentPage
    {
        VoucherRedeemByQRCodeViewModel _viewModel = new VoucherRedeemByQRCodeViewModel();
        VoucherModel _voucherDetails;

        protected async override void OnAppearing()
        {
            using (UserDialogs.Instance.Loading(InfoText.PopupLoading_Text))
            {
                var voucherRedeemDetailsRequest = new MiscellaneousService.CouponRedeemDetailsRequest()
                {
                    UserId = DB.GetValue(StorageEnum.UserID).ToInt(),
                    VoucherDesignID = _voucherDetails.VoucherDesignID,
                    VoucherID = _voucherDetails.VoucherID,
                };
                var voucherRedeemDetailsResponse = await MiscellaneousService.CouponRedeemDetailsApi(voucherRedeemDetailsRequest);
                if (!voucherRedeemDetailsResponse.Error.IsEmpty())
                {
                    await ErrorAlert(this, voucherRedeemDetailsResponse.Error);
                }
                else
                {
                    _viewModel.Voucher = voucherRedeemDetailsResponse.RedeemInfo;
                    //_viewModel.Voucher = new VoucherModel()
                    //{
                    //    OrgName = "HEHEHEHE",
                    //    VoucherName = "HE HE HE HE",
                    //    VoucherCodeBase64 = "iVBORw0KGgoAAAANSUhEUgAAAMgAAADICAQAAAAHUWYVAAAAAmJLR0QA/WmBrZMAAAAJcEhZcwAAAEgAAABIAEbJaz4AAAAJdnBBZwAAAMgAAADIAOtKfJYAABgqSURBVHja7V1tjFXXdV3vY2YEEQPmwxgKirCDLQIGJMuuLIUSFBQVY2JDLdUyaVW15lcQOLaDEsmpIjU/KodgwP5RyU4btcFypFjGBNuq68qlpIoKiQCX2IoJoMTEATPDx6AAM+/Nu/2Rxz7rWXuz77nceXOT3DV/Dofz9j0f956zz8c6q5IkKFEgVMc7AyU6UTZIwVA2SMFQNkjBUA/B0ziYq+mZuEeJbWE/hm7Y3lGcbIeqWIZ+JfUJ/EzCd+MWJQWXdyFujcjJAZzJtaY68pcIXk2Q69+aRMPVZHEO9jZJbG9yWH3Odvrlq2oKLu/2JAZrcq4pzh91WZVcW91GHs+pKKHYJ3arvHH5K8eQgqFskILh97RBRiXUgL7UUIsqenG6r7oevRbLM5m7hKdxKdMvl2OthF/AUSf1Q/hUO1TDHIn9ENvQbIcPu/mbiO0SP4DHpEIex2zFHucvYBK2YFKm8u7DK/p/hPF9D437z0V5HQHnk5mul7VE9TQepzQPRHhtjEOGF7NHUpxOpkjsatVrQ3JItfcl1cuamZzPWFPPqfnr8LIYTWTDcMbfcScEtDLaqKDPTRNS6F1WX4cHF1IbXUnmEls1/Hs6hvzhomyQgqHuJxmBt2XSE9WuPWrHkiIjhGZHFxdieyU8qnYKFfTK02vU3YTOqbejywr2/Py10HBSsD0L7nNG8EW876TZEeGT9WIXrijxM1JbAIBteFGJnYu3pUAv4mklxVS8IQ11CH8q8evxv1Jp8yV2Pn4kL6Ofv/3Y7KS4Hd9zm8RtkATvuU7oxfQ1iQpuj0ht4RSOKLEtLJEC/bdR3IUS/oBs/C2WKKknYHFEni6qeWKMun1NqjHEn2J1f1pVNWJDR+Z7aokRzgq/FvyaLAf1wqGQDeKvzupv9Ch06IWsGOHxRZxzkxkf4qwUfT4mKCkG8GsJ62MS99F9ap9/u1Gxx9XefYBsDLv9/wxZUBlbdKlBtuG5dqgXP1KHypfwpIR193E/eUX/KF4Rw3Irn1S/kVVk46tkW8dGbO1KTXWpQZrk81vdjbcI0VLnDWmgN/Ao2Ujcp2ddTIpFIceQP2aUDVIwdKlBwgc/YvSScX1nHl4RF92fIXSry+rSGPI5eVAFr+MtJcU+18atWCPhi9jRDtXwMKZnytNxsQH8VEKWvWwbdvHoUoOslf22FhbTiakY3Ek7fJvx9+1QFZ/J2CBHZZews0K+Rssr3UfXx5BG5neAF0NCl9WT+6SuW52TjnJQLxjKBikYUjSIvxIas1aa/6pRUdah8qknt0PvwU73cPTd7mNewJ52qCXHpIF52Iqe6/7uAjbhwnVTNLBJDltb9p6SY9ps7x48JSmexw9TVNb1cY+U0UK/U1ogRYNUc3H4jqgFnoy1zvt9Hl9xLLfIYf60sQtyL+5T7M0iN/rtHMp4C9nLji6NIfrEK8GI8zvv/zthvX9hWZ7ttYzw+KIc1AsGo0GyDpRxv0vc9JVcNlfHEnnXlDGGDGXkCA24H38FN0nXMhkfSXginZG9IIvh5zBNsj6B8lTDzHYowbkxm8o1cU5eiInqGd4WzmZ8ZSxHyWiQp2VDKQ4tDDopevADfLodfh/3yl7FBvyDpNmEN9uhqXgZU9vhfbTDt0V2+EawKuNSjI+fY5WMOpy/gEGsyNjrXzbijQa5lPEMu48KpsnbfQYfSYH5jbkg30KCabi5Hda/kJExXI5r4oyav4CWbE3nhXEY1BMKedQ0fwwZ2zEmjjqXB0ovq2CgBsnbF7cO5YTZQp12u/lN198Sa7U3DOne2VqAZyJ+/vx5TT7gklEHPCuXmWbAPWpsC2/heDs8iC9I4T5Bc/nfqL/k/P1WUo/ibuGYz3M/+D7cT0snXv5OmjP/NGcQ02MW/yMjASgSm1Vm0+JkWFJsV1PMTE479nqTI2qK7QZDKSZ//Le5OxWVjOsYkse0r/hTxziUg3rBUDZIwdClBtH9En/Jw/Kb4rLtD8F5+03ZYUxzX1GP5czG4868mHngi/CoxAdeOWO6Ye1RLGqHeAXpKF6QsH9oiPFPshTD0PPXxDZ86NjTefRsL9Qf895TQR/rv6R6GguSq46PwDzwVVHeBXtFr6spdqt56ktxG5D+Z+VvqetlrXLtcf0dysPL0t9cn7KIFMxuH3pXlnfvqudvOIXX5p+9DOG+yEWXclAvGMoGKRjqVnToenSeOvPAA0/d4oH78D2uKuWp6fpFNUrdUJdAWmr+LFY+81eyrvpZZwiY5280yOP4KynMepWn/hK+JeHAU7d44D78fYVlxHj6FnY5qR/GMglvVr2y/Wr+EhxT7b2E/RI+iWw4hvWqI888f6NBZour1lQZgcBZYuVdJHM6DzwP9NOOoU/jn05HsCerKYai8jeAgRsuwRUcVnsC5lS6Y4jVNfgc1rFcY4qzXZzVLn2KWt65WGBkbhB+67p/62cYVtMsenRn+9Ua6sNXUU2R28yztxnUow+4PPBfSw/MPPUrOOZ0J038XHrdfsyT+Dliu4fGOMsej3F3OEVOcEwux5mA+RGNeZth7z1phg+wREpz1lqg8abyfCXfElo6aSZX5e/BpE/5e5BSbKSljoMSezDpc67kO5PMFXvrKFcNst2S2CPJJDUnVbF8c/Ir+qX2N0QXPS9OhpzU/DdC+eMNr17Jx11kb6uxgZb5C6nRp6hzzJkHzoNZuC/LPwueYERss8NYV99zTq2D50l2mpjUPsLco0n2rIovB/WCoWyQgiFFg4wooU40I2J5zdhaPWZ/JXRB/uJKPZK+oOeOOfVZoc83+ApCdhaMY0CW6b8Rf2C28aB16i1xi9S0o/guXVQc3MCltHwQ/JWJ2EgbXrq9l8SDG8Fm8bIOR21i7ZOLlxPch5VKedneGjl4dBm71FO6Ou+9FzukIYboQsAO/yxq9yQz/GM2X85o+WqyUGwsTEYl/tmoY0XBC6wmR9UU28jKbok9nUx1ytVH9o6Sv7fJKE9hxpDs5ybrFAqeWNwuefgSelJsj3HY9xSbFAqprflNYRqkxO9QNkjB0KUG6c7Kr09v8Fem43KaP12CvKwD+GZedQSgkwe+QTyXBp7MuMHD+dsgB6+ZRz+Iv5SxQ3/GBfy1uJ56/qq0YnYST8qotIhY6G/i+XZoAp419oug2GP8ECck/BQf/A7j+27XE4r7u0/1Ilopjtno4PxtU1OwF5M1f4xDSUX1ilZL7NRkMMIjPGys3e2mNFVuy3yhz1n8m+Ttd83LazOFz+Plj1ExJnLh6T2pOCk3XrIS4wZqkO5sdFqDbexgGmM777xmzXUa0KDeJ8zWNLis8nSrmCZtPIXiL8kCwwhNlOqYKoXW1Dp/R7O+NmW8QPE6j37QqKApsujN9nQkOC/dENu7TE+cIDU13ehiQnkrmBq1C0hpPxt1CuN5fF2JnYa3ZeWGdxKeFr8kwTmJvQNvSC89UX3KIFbIStVwhz2NR98wevSd+Hw7NIAVzoGjBh7Cu4q9XeRl7cTOdqiKaaqVUN5evBF1ZWDmL0R/o6uYIbxyhv5G1zHTOS/cwll8pMTH8einSMkq7qCZYFDN62VaRJzg1lQob2/kPRO5HHLw47OPHPmOC2n6/O5LwzJKL6tgyL1B0lAWrsHXZMrHz8+76HG/1Hn5FqL01PuxzMnMMPaKdzXTYIIHXMQrkt15tAEV9MqvYKWqWBWQXZ/9N8SM1/XUrfKeVO/Hs/TjdV7+SUtIKkzafT31xXQMaLubmpU5/Q0qXpoISpo3J2ec5YhhQzlUP1Z0mpRI+S/oqXceexpWn7kmY3mZl79DzV+knvpYngAs5uUz+Vrn0liWy0G9YCgbpGCIapAKeQx5c7/HkinOq7M6eDmlqYQse3Fge9adqFFHSU9hszTEYYp/VD2ic6tqw+JtL3Wfzjx1Xe/d4tH/D/6jHeIVOOaVM4Ppa7JYwzx61j//v4g64/KyvRWk9XAn/yCM73tcb8X603nliep19CXvuqlZrzwc2+ENqm+rXtFSw95qNdcPuPlgfDujV5mmvIxcxpC41Zqs5wF9RRxL2su/EM1H9huy4spbDuoFQ9kgBQONgRYPPCiM+3e1J2jIlKdq+DSNFIqFwZ7OD4lDYKRw/iyEXX9ea/N57z64/mr+BR06D7wX35Oj1O/ji06TnMdDsgG1nES3uLjrnYMzfITnHFZJFrOtWAHAVlGsOkf508H68ax/7vPefTBP/RFsUdNQg+g88AoWiFNbcxdPGnhH7raeo6ZI1GsILDRzubU6nIwadM+lsH4865/7vHcfzFP/MyONMYbwZz2qhGyEFi7i8JTGH6wpoU5kX+FiRq6OItbaHzXcLyTNV8FvTHgHx1IwJWQ7bn7gL0zUrvOva9A77jRlDLVpfWWUwyEaTPtUHjhjOv5EwqFP7cFiGTSZFzRH1T+3eOXz1CMUnL9TckKmYWxgsT57wDnDVwt672zvCg6rY47Oe9d56owJWCrP76MzPh3lDZP2vcTsfkblgfPZ1I2UIvCWWsmwyttuqMxui6f+spr6ZUpRp7yG87fMo39WZaz3Umpe6nhCtVdRbVi8d52nzlcQtqg0z5C9vfRL+kLi9Mpragprl1znlVseT49qm1M33eHZ12dnNNXUvsp6HJOda7Vi3L9VDuoFQ9kgBYMhV6H7VswDz2O32eKV57E6WxzEHIzq8LJuI+b0XRKyeOD96sIIg/XP93VsaEGxx3gHv2iHJmK9nPrl/AVw/ooI5uVPx8O+m+5tmFg88B3Odk0nQ2mjmoLt6RtKUw1eecAoMbKWjOExJf3Pl9PoLK+eP+MYkAWdB+53WTUjzJb1WYG/Bx7gr94WB2n2y8tBvWAoG6RgGNcGyaOzqRj/6pbMXcxz0vDoDZ76howCYVOwUw5b/xJfkPh7Va3xNPriOnR99pNYJ29Y1suO48C8d0bYxenBVtmP8Xn0HQ3Cp8FXZMxgH+7HTe3wa2RvZc4KcLo++xBey/UpPobx706KKlaK1OzP8HfubMrgqWfvycJUjx+dtyeUr2zd2CJ4kml49OWgXjDk3CC+QnosujM4j91cJrnOvzTkIrUceOBTidt9wcjgeXU66OuV93Uw36+B9dkZfIrX56n3q7zaBs47Vci8fB29HRPrmdKlXzZ4xLk0SOCBn8NfyI6hvpfAPHCGr1f+efyrkoL12RnMo/d56luwUYl9F3/uHHtiXr6OiujBA3fgJ9LAOs8/ty8k8MAHVV55gMUD9/XKLxgFnqa+3bwJ7PPUJ6nf5xm3w7R4+TrqlLbfSDMOY0je95QkNxwbZ/lGLPq/K72sgsHYoOJwQwl1gu/0bLgp9N3wOD31kNq6f8u6qNjXUw9Is44Qt/3kw9BTD7qcVayUAy6WXvmPpXDMKz+DA5LiiKp/ztD11PvwWSnyJynFJ+iKv3AIaQj7pfJ/S6WZRfZ0PfWj6lLGSXdeHcfLT4XU1J6PQd8AYl65zshKo3/OG0C6ve2qjcNJr6TYHFWaTZnZY/qGV1z9RW5QxcDnlcfqn+dxsdnY/S5/lIN6wVA2SMGQe4PEMNkZ+vDZMFLk3cXksZaVD9OevCzmgetIo6f+VTm2cwIxYN52AK9w3UkpBvCYFMDXK3/BunlHMF1ss566Vd5gbxK2SA5vRS4I4/tu149YEHXMRv+z9M/jwF7RIfKywuFt9rJWubnaQanDsSKL9/4AeYHnM5YghZfl9155T4KyI3RZafTK/QW70GWxnrp12FpXAMoH5aBeMJQNUjAYPHUrMWvyBfSo7doiH6kuPldvii7G54GPdqS+1m2w6BZ3UzpPnXn0oXOy9dQ1sp6vnJiuxAEGT13HB1hB0qEBW9W7efbR4ejH8Ug7VMF8N1NPujzwQFdj3vtcvC0FYoFvnae+jI6L7xJtdVtPPejH6zx6HT3YpUqmWTB46jqqOKKu1d6m/vIUhee4thnHI+7YZt57C0vU6tF56pMpT//sPvGsmsLn0dedSzw/jqgxpGVM9lpubBy3I+vA5qsx8+vk610jKoWO2ANL5aBeMEQ1SBo9cN10XLtnfRtb7vtYN3/p2cv7zeUy8rdq8NR1+HrgrH9+nOIDrzyNnrrOA9fzx/bm4ogzxFo8dX2MY3uncOPg8g7TEzvuTgmT9r0GKzv8+XrgrH/eQykCr3wSbVDF6Z+/bCzFBH32nxj2fJ66zqNne/Ucloq4vI+pPH+Tp67D1wO3/HLmlSdu6gDmgfus9nqKWYEOnUef3Z4OLm9izPrKQb1gKBukYDCOAengY5Xsb2Vd/fSvrhihxRDr3fH12XV0h/c+bCzFWP6gwVPXwfrid6l64Kx/ztB56jfjCRlbdL3yGv5FnnmZnhjsWfrsOpj3PpGWTpa7FzkvVZeHGCfomFKwVzeOmv6Unr6GN7dy2C1KgS+7G1Ssf/6axA6SXvlq1V7c30xS5txL8bpyKG94+Xrvr5G9Z117/GcofY4l/O5hVA03yLeqRtmz0FBt+NWQtQRpUCp9FhhlgxQMXddTT6LoCEVkm2e3l+bUZtf11EfxjNzsy+B1qm+KTuaIQdPxsQYbyN4BJ/XzeKsdYl65hWCPefm6PcY0fF98xj3GoSuDp54HeGBbJJcxt/CUu6njVV8afIpOv3/HTX1UTlpVDKoZ48d4vR26ifYRdXuMhfiOdEm/NGx3XU+9kQ+LzkXW7bE0O+A1Sh0Dvv3I8sPKQb1gGIcGGc/7rfLm0cdSK6Iun2FMMuS0Pfh65cyatXjgU9Sl6QnE3q2JjQTnZPmF9dlrqv458+ivEHv3EmlB+5ji6qnr9TcZH8lE19J6MBpkC/koMfD1ynvwA+lJLR544JUz9tEe2xbZgRzBKnESWJ99F6UO+ufMo19O50i+Ln5dGuyURVFLT12vv/dxr5TdegGMBumP0lYP8PXKmUhv8cCnqE/Xv5CRj92U0CsptC+EefT8hcT1B1PcFHr9ncFHriROCrmKGIwlXS2NDV+odTzvTknz9NLLKhjGtUGy3idn8d75Pip+F/mWU30mwOvLdUodU1H+kThLwytK6TONXvnduMXNjGbP54Fb0Hnv07BHqoV56kH/nHn0zCpfIqlrOCjcL87fL9x1DH99YTLWqseQZvE/wtaIzgO/mix2t31eldSsV67zttPY26P+0ue9HzF46nH67H7+/L/tSVak6LLyPvea77DaqVHu2U6jzz6+nPVyUC8YygYpGArZIH9YchVx6M5auAFf/5x54MxTfyWj1mY+0PXjdZ6/xXtnfXa2N64NcjOeUOPvlwuRp2AjEfPDuaxfjWuDrMMqJfYNtUFG8V281w4vwGapcD6XtYIaZJzvfvf0zy0aaj7XWGRFMyLWOlnJ08jyGFCBUTZIwdClMSTos6fhgdeIE9KgvQdPn5156rpOIDM02B7nr06sdl3/vEr2Au+dF0WaKu89MXj+jC41yDa8KJnyeeCP4Rvt0BAekZUvX5+deepvCvPc4pWzvZC/Or6Bue0w68c/jK9I6nBMiPXjecVvG/5NKe8xfEbWAaxtvC41yKkoHvh02e0bwHuiwebrszNPfZ/LK2d7IX91fBJ3tsOsHz9DZSGyfjzjQ5WBeQXvuDXVpTHEf4zOSh2N4sFWU6gx63li9enQwbEvl0LduUs1VaKrSNEgvs+fdSvWegPD11JXY+0n1tTUPvSvIo3+uSeSnAYGT11HBQvcvbDJnpEOe4FXfrvRRw9Ij8688os0KgS99wTHZMuJ9c+ZB37S3WKbo+rH+/rnrB/PPPrZHZffeDB46tZFxcMqh/uqyrO2NqhYXzzwyocpRZPsPajyyquq3vsQbSix/jnzwFerm0gWTz3ox/v656wfzzz6rW6d6fXXwVO3kPfFfrpaeo2+Q10L3dJ7D18ZL8UwD9zvl3Weuq9/zvrxPR320qusd6Ic1AuGskEKBqNBsnrX1ocafJHhFD6Z77nEeX55bG3px4pgxGZfjTZq/j8zunPWoeXPyYPqKSSC1smleJexSyyyPvtd6u9Yr5z13heL/Abb8zGAl6Rqh2g3Jui9My6revQWdF4+LC8rj784+QYdg+S1PaCmuJoskRRLDb33vao9P3+H6F6iTRSve22rXXuMbePLU88K6+53HYmh995S7fmouCch4cZaqKYIlygAygYpGKhBxpNqZsPPVeKmTYxwzNPHsnY61vES+ddpHMz1MXmIZA3jv2SLSLfHh7f7sUzesBO08xEOg/v2GCwyNo9OhhxQpTHjyqvnr6NBShQB5RhSMJQNUjCUDVIwlA1SMPw/FXxJYrmQSyAAAAAldEVYdGRhdGU6Y3JlYXRlADIwMTItMTAtMDhUMTA6MTI6MTYrMDE6MDAN744MAAAAJXRFWHRkYXRlOm1vZGlmeQAyMDEyLTEwLTA4VDEwOjEyOjE2KzAxOjAwfLI2sAAAAABJRU5ErkJggg==",
                    //};
                }
            }

            this.BindingContext = _viewModel;
        }

        public class VoucherRedeemByQRCodeViewModel
        {
            public VoucherModel Voucher { get; set; } = new VoucherModel();
        }

        public VoucherRedeemByQRCodePage(VoucherModel voucherDetails)
        {
            InitializeComponent();

            _voucherDetails = voucherDetails;
        }
    }
}