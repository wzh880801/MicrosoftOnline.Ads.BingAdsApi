using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrosoftOnline.Ads.BingAdsApi;

namespace BingAdsApiDemo
{
    class Common
    {
        //CustomerId for demo
        public const long CustomerId = 42164768;

        private static ApiAuthentication OAuthInfo = null;

        public static ApiAuthentication GetApiAuth()
        {
            return OAuthInfo == null ? 
                new OAuthAuthentication(
                    //accessToken
                    "EwB4AnhlBAAUxT83/QvqiAZEx5SuwyhZqHzk21oAATo1L+ZidOwC+A7XuwE5iM2aMgpH+jsm4QPw/z8qHHdp/uwX2PE6mM2vuKFo4jvzAg/O9iHDPSePHL7tCliKXo7ZAFN/N8fG1xcEaiiUpwkavJSjo5LBEqqarur0FvNFdF6Ie2ZJ7yG6ayMbwOlppdnjzgnebyDMDUCFUmNBOBUdhd7q3ucVVxknHMUPHjC9XmP4+ZP8rtpAZil/P6Oq45hnhzICqBLJ2V5kxQKNrfJvtCfYTCNyDJA79NmDwUfYqsnZWnJM+7OVwRVsxjWCV1xx59vzLlu6l6p9jWy2afPrm9v0jcwvJzxgVNwQPh4Oxn0MrRkKC2sQQzZytzJu8TIDZgAACCIoUitsnpRwSAGeVH5noWGzfbhML3sOwgBlq9SVIjGtr4mSGfFXN3SCGGYRQLSrNKoziuPsNBI0CioNQBcihZq3GchYmQ+znDBkjmgH9rn9+GJ1rCiy2PKt96GMGw7HF3WAtAldj7SzLEno9MKCyd9GHil432UKMwEkH9Al5cqfKJFgrMtlbRDt/jcL46lpDdUnq5A/2fYoce7X71WKybmez5vKTMOWy+Vw4ejhvgZ98Vbq/GFMtEGy71wM6q850egvcuFACZzVaTwztrJcWyVUpbEUGdrfNVwwCfdNo+Xy8gagyf+fVgC/e8qO4qJh2BERLzakEr7RWvzW+P/g3QMDXiibph8syohcPocHaYZnecbfmDzg3lDO6sXyS8JlA9QvanMIu+fo1s+ZBUb8K07q8T6O7fc7JW/BiYjnBTznWZmfnGdPXSIGduf8ISU6b4GFdwE=",
                    //refreshToken
                    "MCUdoFKVCaEWbNBEciYzkgNqrjr3rjZDdFtYxw0waplqvZ1Ujlx3v3oDOxyawzE6*HsFbx4JWs9wcgtwcW4!VWDfLEJ4TgmSc*tL5!SJvkCNALpCSWFbmbw*F1Bd18BtUwIBNx!KzDK*g0cNeNVtNV6VafOUBmMvF0zcE7c4iTtpD*DfSwwTvWQHAJCxsLsW2vKHEskennvS*jIOQEEEJ2qjfr*wC5sO8sRNyKQwM2A0H3hhnTkpTeo0eM3EC281uFCJ6rQEBQLpO9oRURuUl4RGCSYYih*XQgB!rQ6dRn3FptV06u!1Jpg4lYcbyMjy4RYAnKzJQ6LycMaLcQjr1pdA$",
                    //developerToken
                    "0417MW27U4928703",
                    DateTime.Parse("2016-06-03 11:23:13.449").ToUniversalTime().Ticks,
                    //clientId
                    "000000004418E25F",
                    null,
                    //save the token when a new token was generated
                    TokenRefreshEventHandler) :
                OAuthInfo;
        }

        //Event raised when a new accesstoken was generated
        public static void TokenRefreshEventHandler(object sender, TokenRefreshedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            //you can store new tokens
            //or you can replace the old one
            //for demo, we just print them
            Console.WriteLine("Access token refreshed\t------------------------Start------------------------");

            //too long, only show first 10 and last 10 characters
            Console.WriteLine("\tNew AccessToken:\t\t{0}···{1}", e.AuthenticationToken.Substring(0, 10), e.AuthenticationToken.Substring(e.AuthenticationToken.Length - 10, 10));
            Console.WriteLine("\tNew RefreshToken:\t\t{0}···{1}", e.RefreshToken.Substring(0, 10), e.RefreshToken.Substring(e.RefreshToken.Length - 10, 10));
            Console.WriteLine("\tExpireTime(BeijingTime):\t{0:yyyy-MM-dd HH:mm:ss.fff}", new DateTime(e.ExpiresTime.Value).AddHours(8));

            OAuthInfo = new OAuthAuthentication(
                e.AuthenticationToken, 
                e.RefreshToken, 
                "0417MW27U4928703", 
                e.ExpiresTime,
                e.ClientId,
                e.ClientSecret,
                TokenRefreshEventHandler);

            Console.WriteLine("Access token refreshed\t------------------------End--------------------------");
        }
    }
}
