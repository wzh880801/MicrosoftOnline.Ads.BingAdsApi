using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using MicrosoftOnline.Ads.BingAdsApi.V10.AdInsightService;


namespace MicrosoftOnline.Ads.BingAdsApi
{
    public class AdInsightHelper : LogBase, IDisposable
    {
        private AdInsightServiceClient cs = null;

        public AdInsightHelper(EventHandler<LogEventArgs> handler = null)
            : base(handler)
        {

        }

        public void Dispose()
        {
        }

        private AdInsightServiceClient Check()
        {
            if (cs == null)
                cs = new AdInsightServiceClient("BasicHttpBinding_IAdInsightService");
            else if (cs.State != CommunicationState.Opened && cs.State != CommunicationState.Opening)
            {
                try
                {
                    cs.Open();
                }
                catch (Exception ex)
                {
                    Log(new LogEventArgs(ServiceType.AdInsight, "Check", ex.Message, null, ex, null, LogLevel.Warn));
                }
            }
            return cs;
        }

        /// <summary>
        /// Gets the keyword categories to which the specified keywords belong.
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="_keywords">An array of keywords for which you want to determine the possible keyword categories that each keyword belongs to. 
        /// The array can contain a maximum of 1,000 keywords, and each keyword can contain a maximum of 100 characters.</param>
        /// <param name="_publisherCoutry">The country code of the country/region to use as the source of the category data.
        /// Note: You must set this element to US.</param>
        /// <param name="_language">The language in which the keywords are written. You must set this element to English.</param>
        /// <param name="_maxCategor">The number of categories to include in the results. The maximum number of categories that you can request is 5.</param>
        /// <param name="_customerId">The identifier of the customer that owns the account.
        /// Note: As a best practice you should always specify this element.</param>
        /// <param name="_customerAccountId">The identifier of the account that owns the entities in the request. This header element must have the same value as the AccountId body element when both are required.
        /// Note: Required for service operations related to bid estimations. As a best practice you should always specify this element for operations limited in scope to a single account per service call.</param>
        /// <returns></returns>
        public GetKeywordCategoriesResponse GetKeywordCategories(ApiAuthentication auth, String[] _keywords,
            String _publisherCoutry, String _language, int _maxCategor, long? _customerId, long? _customerAccountId)
        {
            var request = new GetKeywordCategoriesRequest
            {
                Keywords = _keywords,
                PublisherCountry = _publisherCoutry,
                Language = _language,
                MaxCategories = _maxCategor,
                CustomerAccountId = string.Format("{0}", _customerAccountId),
                CustomerId = string.Format("{0}", _customerId),
            };

            SetAuthHelper.SetAuth(auth, request);

            return Check().GetKeywordCategories(request);
        }

        public GetEstimatedBidByKeywordsResponse GetEstimatedBidByKeywords(
            ApiAuthentication auth,
            KeywordAndMatchType[] keywords,
            long accountId,
            long? adGroupId = null,
            long? campaignId = null,
            Currency? currency = null,
            string entityLevelBid = "Keyword",
            string language = null,
            string[] publisherCountries = null,
            TargetAdPosition targetAdPosition = TargetAdPosition.MainLine)
        {
            var request = new GetEstimatedBidByKeywordsRequest
            {
                Keywords = keywords,
                AdGroupId = adGroupId,
                CampaignId = campaignId,
                Currency = currency,
                EntityLevelBid = entityLevelBid,
                Language = language,
                PublisherCountries = publisherCountries,
                TargetPositionForAds = targetAdPosition,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetEstimatedBidByKeywords(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "GetEstimatedBidByKeywords", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public async Task<GetEstimatedBidByKeywordsResponse> GetEstimatedBidByKeywordsAsync(
            ApiAuthentication auth,
            KeywordAndMatchType[] keywords,
            long accountId,
            long? adGroupId = null,
            long? campaignId = null,
            Currency? currency = null,
            string entityLevelBid = "Keyword",
            string language = null,
            string[] publisherCountries = null,
            TargetAdPosition targetAdPosition = TargetAdPosition.MainLine)
        {
            var request = new GetEstimatedBidByKeywordsRequest
            {
                Keywords = keywords,
                AdGroupId = adGroupId,
                CampaignId = campaignId,
                Currency = currency,
                EntityLevelBid = entityLevelBid,
                Language = language,
                PublisherCountries = publisherCountries,
                TargetPositionForAds = targetAdPosition,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetEstimatedBidByKeywordsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "GetEstimatedBidByKeywordsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetEstimatedBidByKeywordsResponse TryGetEstimatedBidByKeywords(
            ApiAuthentication auth,
            KeywordAndMatchType[] keywords,
            long accountId,
            long? adGroupId = null,
            long? campaignId = null,
            Currency? currency = null,
            string entityLevelBid = "Keyword",
            string language = null,
            string[] publisherCountries = null,
            TargetAdPosition targetAdPosition = TargetAdPosition.MainLine)
        {
            return MethodHelper.TryGet(
                GetEstimatedBidByKeywords,
                this,
                auth,
                keywords,
                accountId,
                adGroupId,
                campaignId,
                currency,
                entityLevelBid,
                language,
                publisherCountries,
                targetAdPosition);
        }
    }
}

