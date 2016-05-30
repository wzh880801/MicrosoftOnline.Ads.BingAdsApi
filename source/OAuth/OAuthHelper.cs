using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Linq;

namespace MicrosoftOnline.Ads.BingAdsApi
{
    internal class OAuthHelpers
    {
        /// <summary>
        /// ExpireTime is UTC
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static bool RefreshAccessToken(OAuthSettings settings)
        {
            // The access token is valid for a limited time per the 'expires_in' response field.
            // If you call a Bing Ads service operation after the token expires, the API will return a
            // fault with an InvalidCredentials (105) error.

            // Use the refresh token to get a new access token without further user consent.
            // For example GET /oauth20_token.srf?
            //                 client_id=<ClientId>
            //                 &grant_type=refresh_token
            //                 &redirect_uri=https://login.live.com/oauth20_desktop.srf
            //                 &refresh_token=<tokens.RefreshToken>

            if (String.IsNullOrEmpty(settings.RefreshToken))
            {
                return false;
            }

            var now = DateTime.UtcNow;

            var uri = settings.RefreshAccessTokenUri(settings.ClientId, settings.ClientSecret);
            var tokens = GetAccessTokens(uri);
            if (tokens == null)
            {
                return false;
            }

            settings.ExpireTime = now.AddSeconds(tokens.ExpiresIn);
            settings.AccessToken = tokens.AccessToken;
            settings.RefreshToken = tokens.RefreshToken;

            return true;
        }

        // Gets an access token. Returns the access token, access token 
        // expiration, and refresh token.

        public static AccessTokens GetAccessTokens(String uri)
        {
            var responseSerializer = new DataContractJsonSerializer(typeof(AccessTokens));
            AccessTokens tokenResponse = null;

            try
            {
                var realUri = new Uri(uri, UriKind.Absolute);

                var addy = realUri.AbsoluteUri.Substring(0, realUri.AbsoluteUri.Length - realUri.Query.Length);
                var request = (HttpWebRequest)WebRequest.Create(addy);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                using (var writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(realUri.Query.Substring(1));
                }

                var response = (HttpWebResponse)request.GetResponse();

                // The response is JSON, for example: {
                //                                     "token_type":"bearer",
                //                                     "expires_in":3600,
                //                                     "scope":"bingads.manage",
                //                                     "access_token":"<AccessToken>",
                //                                     "refresh_token":"<RefreshToken>"
                //                                    }

                // Use the JSON serializer to serialize the response into the AccessTokens object.

                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        tokenResponse = (AccessTokens)responseSerializer.ReadObject(responseStream);
                }
            }
            catch (WebException e)
            {
                var response = (HttpWebResponse)e.Response;

                Debug.WriteLine("HTTP status code: " + response.StatusCode);
            }

            return tokenResponse;
        }
    }

    internal class SetAuthHelper
    {
        //public static void SetAuth<T>(ApiAuthentication auth, T request)
        //    where T : class
        //{
        //    var fs = request.GetType().GetFields();

        //    if (fs.Length > 0 && fs.Count(p => p.Name == "DeveloperToken") > 0)
        //    {
        //        SetAuthUsingField<T>(auth, request);
        //    }
        //    else
        //    {
        //        var ps = request.GetType().GetProperties();
        //        if (ps.Length > 0 && ps.Count(p => p.Name == "DeveloperToken") > 0)
        //            SetAuthUsingPropertity<T>(auth, request);
        //    }
        //}

        public static void SetAuth(ApiAuthentication auth, dynamic request)
        {
            if (auth.GetType() == typeof(PasswordAuthentication))
            {
                var _auth = auth as PasswordAuthentication;
                request.DeveloperToken = _auth.DeveloperToken;
                request.UserName = _auth.UserName;
                request.Password = _auth.Password;
            }
            else
            {
                var _auth = auth as OAuthAuthentication;
                request.DeveloperToken = _auth.DeveloperToken;
                request.AuthenticationToken = _auth.AuthenticationToken;
            }
        }

        //private static void SetAuthUsingPropertity<T>(ApiAuthentication auth, T request)
        //{
        //    var ps = request.GetType().GetProperties();

        //    if (auth.GetType() == typeof(PasswordAuthentication))
        //    {
        //        var _auth = (PasswordAuthentication)auth;

        //        foreach (var p in ps)
        //        {
        //            if (!p.CanWrite)
        //                continue;
        //            if (p.Name == "DeveloperToken")
        //                p.SetValue(request, _auth.DeveloperToken);
        //            else if (p.Name == "Password")
        //                p.SetValue(request, _auth.Password);
        //            else if (p.Name == "UserName")
        //                p.SetValue(request, _auth.UserName);
        //        }
        //    }
        //    else if (auth.GetType() == typeof(OAuthAuthentication))
        //    {
        //        var _auth = (OAuthAuthentication)auth;

        //        foreach (var p in ps)
        //        {
        //            if (!p.CanWrite)
        //                continue;
        //            if (p.Name == "DeveloperToken")
        //                p.SetValue(request, _auth.DeveloperToken);
        //            else if (p.Name == "AuthenticationToken")
        //                p.SetValue(request, _auth.AuthenticationToken);
        //        }
        //    }
        //}

        //private static void SetAuthUsingField<T>(ApiAuthentication auth, T request)
        //{
        //    var fs = request.GetType().GetFields();

        //    if (auth.GetType() == typeof(PasswordAuthentication))
        //    {
        //        var _auth = (PasswordAuthentication)auth;

        //        foreach (var p in fs)
        //        {
        //            if (!p.IsPublic)
        //                continue;
        //            if (p.Name == "DeveloperToken")
        //                p.SetValue(request, _auth.DeveloperToken);
        //            else if (p.Name == "Password")
        //                p.SetValue(request, _auth.Password);
        //            else if (p.Name == "UserName")
        //                p.SetValue(request, _auth.UserName);
        //        }
        //    }
        //    else if (auth.GetType() == typeof(OAuthAuthentication))
        //    {
        //        var _auth = (OAuthAuthentication)auth;

        //        foreach (var p in fs)
        //        {
        //            if (!p.IsPublic)
        //                continue;
        //            if (p.Name == "DeveloperToken")
        //                p.SetValue(request, _auth.DeveloperToken);
        //            else if (p.Name == "AuthenticationToken")
        //                p.SetValue(request, _auth.AuthenticationToken);
        //        }
        //    }
        //}
    }

    [DataContract]
    class AccessTokens
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "expires_in")]
        public int ExpiresIn { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }
    }
}
