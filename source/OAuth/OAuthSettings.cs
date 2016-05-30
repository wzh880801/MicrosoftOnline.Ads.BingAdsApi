using System;
using System.Text;

namespace MicrosoftOnline.Ads.BingAdsApi
{
    /// <summary>
    /// This class contains the data necessary to make calls to the BingAds service using OAuth authentication. To create a new OAuth client, see
    /// https://account.live.com/developers/applications
    /// </summary>
    internal class OAuthSettings
    {
        /// <summary>
        /// The OAuth client Id.
        /// </summary>
        public String ClientId { get; set; }

        /// <summary>
        /// The OAuth client secret
        /// </summary>
        public String ClientSecret { get; set; }

        /// <summary>
        /// The OAuth access token. This is used in the AuthorizationToken of BingAds service operation headers.
        /// </summary>
        public String AccessToken { get; set; }

        /// <summary>
        /// The OAuth refresh token returned in the authorization grant flow mode.
        /// </summary>
        public String RefreshToken { get; set; }

        /// <summary>
        /// The OAuth expiration time returned in the authorization grant flow mode.
        /// UTC
        /// </summary>
        public DateTime ExpireTime { get; set; }

        /// <summary>
        /// The scope used to request authorization to manage a user's Bing Ads data.
        /// </summary>
        public String AuthorizationScope { get; set; }

        /// <summary>
        /// The OAuth redirect URI.
        /// </summary>
        public String RedirectUri
        {
            get
            {
                if (_redirectUri != null)
                    return _redirectUri;

                return String.Format("https://{0}{1}",
                    AuthorizationServiceHost,
                    DesktopRedirectPath);
            }

            set { _redirectUri = value; }
        }

        /// <summary>
        /// BingAds.NET can automatically use the provided OAuth refresh token, if provided, to update the access token.
        /// </summary>
        public bool AutoRefresh
        {
            get
            {
                return _autoRefresh && !String.IsNullOrEmpty(RefreshToken);
            }

            set { _autoRefresh = value; }
        }

        /// <summary>
        /// The fixed URI path used for mobile and desktop applications.
        /// </summary>
        public const String DesktopRedirectPath = "/oauth20_desktop.srf";

        /// <summary>
        /// The URI path used to obtain user authorization during authorization grant flow.
        /// </summary>
        public const String AuthorizePath = "/oauth20_authorize.srf";

        /// <summary>
        /// The URI path used to obtain an OAuth acccess token.
        /// </summary>
        public const String AccessTokenPath = "/oauth20_token.srf";

        /// <summary>
        /// The standard authorization authority for BingAds OAuth clients
        /// </summary>
        public String AuthorizationServiceHost = "login.live.com";

        protected const String AuthorizeUriFormatter = "https://{0}{1}?client_id={2}&response_type=code";
        protected const String ImplicitUriFormatter = "https://{0}{1}?client_id={2}&response_type=token";
        protected const String AccessUriFormatter = "https://{0}{1}?client_id={2}&code={3}&grant_type=authorization_code&redirect_uri={4}";
        protected const String RefreshUriFormatter = "https://{0}{1}?client_id={2}&grant_type=refresh_token&redirect_uri={3}&refresh_token={4}";

        private bool _autoRefresh;
        private String _redirectUri;

        /// <summary>
        /// Constructor for OAuth credentials and settings. Use this constructor to request that BingAds.NET use the refresh token and client ID to request a new access token if an operation fails due to invalid credentials.
        /// </summary>
        /// <param name="accessToken">The current access token.</param>
        /// <param name="clientId">The ID for your application.</param>
        /// <param name="refreshToken">The current refresh token.</param>
        /// <param name="clientSecret">The client secret, used for auto refresh in OAuth web applications</param>
        public OAuthSettings(
            String accessToken,
            String clientId,
            String refreshToken,
            String clientSecret = null)
        {
            //if (String.IsNullOrEmpty(accessToken))
            //{
            //    throw new ArgumentNullException("accessToken", "OAuthSettings requires a non-null access token.");
            //}

            AccessToken = accessToken;
            ClientId = String.IsNullOrEmpty(clientId) ? null : clientId;
            ClientSecret = String.IsNullOrEmpty(clientSecret) ? null : clientSecret;
            RefreshToken = String.IsNullOrEmpty(refreshToken) ? null : refreshToken;

            AuthorizationScope = "bingads.manage";

            AutoRefresh = (ClientId != null && RefreshToken != null);
        }

        private String GetScopeAndRedirect()
        {
            var str = new StringBuilder();

            if (!String.IsNullOrEmpty(AuthorizationScope))
            {
                str.Append("&scope=" + AuthorizationScope);
            }

            if (!String.IsNullOrEmpty(RedirectUri))
            {
                str.Append("&redirect_uri=" + RedirectUri);
            }

            return str.ToString();
        }

        /// <summary>
        /// Returns the URI used to request Authorization for a client to manage a user's Bing Ads account.
        /// </summary>
        /// <returns></returns>
        public String RequestAuthorizationUri(String clientId)
        {
            return String.Format(AuthorizeUriFormatter, AuthorizationServiceHost, AuthorizePath, clientId) + GetScopeAndRedirect();
        }

        /// <summary>
        /// Returns the URI used to request an access token using the implicit grant flow.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public String RequestImplicitAccessTokenUri(String clientId)
        {
            return String.Format(ImplicitUriFormatter, AuthorizationServiceHost, AuthorizePath, clientId) + GetScopeAndRedirect();
        }

        /// <summary>
        /// Returns the URI used to request an access token for a given authorization code.
        /// </summary>
        /// <param name="authorizationCode"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <returns></returns>
        public string RequestAccessTokenUri(
            String authorizationCode,
            String clientId,
            String clientSecret = null)
        {
            var baseUri = String.Format(AccessUriFormatter, AuthorizationServiceHost, AccessTokenPath, clientId, authorizationCode, RedirectUri);
            if (clientSecret != null)
            {
                baseUri += ("&client_secret=" + clientSecret);
            }

            return baseUri;
        }

        /// <summary>
        /// Returns the URI used to refresh an access token
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <returns></returns>
        public String RefreshAccessTokenUri(
            String clientId,
            String clientSecret = null)
        {
            var baseUri = String.Format(RefreshUriFormatter, AuthorizationServiceHost, AccessTokenPath, clientId, RedirectUri, RefreshToken);
            if (clientSecret != null)
            {
                baseUri += ("&client_secret=" + clientSecret);
            }

            return baseUri;
        }
    }
}
