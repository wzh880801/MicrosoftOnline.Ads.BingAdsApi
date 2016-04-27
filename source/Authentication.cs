using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftOnline.Ads.BingAdsApi
{
    /// <summary>
    /// OAuthAuthentication
    /// </summary>
    public class OAuthAuthentication : ApiAuthentication
    {
        private string _authenticationToken = null;
        private long _expiresTime = DateTime.UtcNow.Ticks;
        private string _refreshToken=null;
        private string _clientId = null;
        private string _clientSecret = null;

        public string RefreshToken
        {
            get
            {
                return _refreshToken;
            }
        }

        public long ExpiresTime
        {
            get { return _expiresTime; }
        }

        public string ExpiresUtcTimeString
        {
            get
            {
                return string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", new DateTime(this.ExpiresTime, DateTimeKind.Utc));
            }
        }

        public DateTime ExpiresUtcTime
        {
            get
            {
                return new DateTime(this.ExpiresTime, DateTimeKind.Utc);
            }
        }

        private string _developerToken = null;

        public override string DeveloperToken
        {
            get
            {
                return _developerToken;
            }
        }

        /// <summary>
        /// AuthenticationToken
        /// 过期前15分钟自动刷新Token以免过期
        /// </summary>
        public string AuthenticationToken
        {
            get
            {
                if (string.IsNullOrEmpty(_authenticationToken) 
                    || _authenticationToken.Length == 36 
                    || _authenticationToken.Length == 32 
                    || DateTime.UtcNow.Subtract(this.ExpiresUtcTime).TotalMinutes >= -15)
                {
                    OAuthSettings _setting =
                       new OAuthSettings(_authenticationToken ?? Guid.NewGuid().ToString(), _clientId, _refreshToken, _clientSecret);
                    var refreshResult = OAuthHelpers.RefreshAccessToken(_setting);
                    if (refreshResult)
                    {
                        _authenticationToken = _setting.AccessToken;
                        _refreshToken = _setting.RefreshToken;
                        _expiresTime = _setting.ExpireTime.Ticks;

                        if (this._handler != null)
                            this._handler(this, 
                                new TokenRefreshedEventArgs(
                                    this._authenticationToken, 
                                    this._refreshToken, 
                                    this._expiresTime,
                                    this._clientId, 
                                    this._clientSecret));
                    }
                }

                return _authenticationToken;
            }
        }

        private EventHandler<TokenRefreshedEventArgs> _handler = null;

        public OAuthAuthentication(
            string accessToken, 
            string refreshToken, 
            string developerToken, 
            long? expiresTime, 
            string clientId, 
            string clientSecret = null,
            EventHandler<TokenRefreshedEventArgs> tokenRefreshedHandler = null)
        {
            this._authenticationToken = accessToken ?? null;
            this._refreshToken = refreshToken;
            this._developerToken = developerToken;
            this._clientId = clientId;
            this._clientSecret = clientSecret;
            if (expiresTime.HasValue)
                this._expiresTime = expiresTime.Value;
            this._handler = tokenRefreshedHandler;

            if (string.IsNullOrEmpty(refreshToken) || string.IsNullOrEmpty(clientId))
                throw new ArgumentNullException("refreshToken or clientId can not be null");
        }
    }

    /// <summary>
    /// PasswordAuthentication
    /// </summary>
    public class PasswordAuthentication : ApiAuthentication
    {
        private string _password = null;
        public string Password { get { return _password; } }

        private string _userName = null;
        public string UserName { get { return _userName; } }

        private string _developerToken = null;

        public override string DeveloperToken
        {
            get
            {
                return _developerToken;
            }
        }

        public PasswordAuthentication(string userName, string password, string developerToken)
        {
            this._userName = userName;
            this._password = password;
            this._developerToken = developerToken;
        }
    }

    /// <summary>
    /// abstract class, do not use this directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.
    /// </summary>
    public abstract class ApiAuthentication
    {
        public virtual string DeveloperToken { get; set; }
    }

    public class TokenRefreshedEventArgs : EventArgs
    {
        public string AuthenticationToken { get; private set; }
        public long? ExpiresTime { get; private set; }
        public string RefreshToken { get; private set; }
        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }
        public string ExpiresUtcTimeString
        {
            get
            {
                return this.ExpiresTime.HasValue ? string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", new DateTime(this.ExpiresTime.Value, DateTimeKind.Utc)) : "";
            }
        }

        public TokenRefreshedEventArgs(
            string _authenticationToken,
            string _refreshToken,
            long? _expiresTime,
            string _clientId,
            string _clientSecret)
        {
            this.AuthenticationToken = _authenticationToken;
            this.RefreshToken = _refreshToken;
            this.ClientId = _clientId;
            this.ClientSecret = _clientSecret ?? "";
            this.ExpiresTime = _expiresTime;
        }
    }
}
