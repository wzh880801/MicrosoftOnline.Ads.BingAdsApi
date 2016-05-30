using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MicrosoftOnline.Ads.BingAdsApi.V10.CampaignManagementService;
using System.Threading.Tasks;

namespace MicrosoftOnline.Ads.BingAdsApi
{
    /// <summary>
    /// BingAds API V10 CampaignManagement Service
    /// https://msdn.microsoft.com/en-us/library/bing-ads-campaign-management-service-reference.aspx
    /// </summary>
    public class CampaignMHelper : LogBase, IDisposable
    {
        private CampaignManagementServiceClient cs = null;

        public void Dispose()
        {
            if (cs != null)
            {
                try
                {
                    cs.Close();
                    cs = null;
                }
                catch { }
            }
        }

        public CampaignMHelper(EventHandler<LogEventArgs> handler = null)
            : base(handler)
        {
            cs = new CampaignManagementServiceClient("BasicHttpBinding_ICampaignManagementService");
        }

        private CampaignManagementServiceClient Check()
        {
            if (cs == null)
                cs = new CampaignManagementServiceClient("BasicHttpBinding_ICampaignManagementService");
            else if (cs.State != CommunicationState.Opened && cs.State != CommunicationState.Opening)
            {
                try
                {
                    cs.Open();
                }
                catch { }
            }
            return cs;
        }

        /// <summary>
        /// Retrieves all the campaigns that exist within a specified account.
        /// https://msdn.microsoft.com/en-US/library/bing-ads-campaign-management-getcampaignsbyaccountid.aspx
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="accountId">The identifier of the account that contains the campaigns to get.</param>
        /// <param name="campaignType">The type of campaign to get. You can specify one or more types.
        /// https://msdn.microsoft.com/en-US/library/bing-ads-campaign-management-campaigntype.aspx
        /// </param>
        /// <param name="customerId">The identifier of the customer that owns the account.
        /// Note: Required for service operations related to targeting and editorial. As a best practice you should always specify this element.</param>
        /// <returns></returns>
        public GetCampaignsByAccountIdResponse GetCampaignsByAccountId(
            ApiAuthentication auth, 
            long accountId, 
            CampaignType campaignType, 
            long? customerId = null)
        {
            var request = new GetCampaignsByAccountIdRequest
            {
                AccountId = accountId,
                CampaignType = campaignType,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetCampaignsByAccountId(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetCampaignsByAccountId", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetCampaignsByAccountIdResponse TryGetCampaignsByAccountId(
            ApiAuthentication auth, 
            long accountId, 
            CampaignType campaignType, 
            long? customerId = null)
        {
            return MethodHelper.TryGet(GetCampaignsByAccountId, this, auth, accountId, campaignType, customerId);
        }

        public async Task<GetCampaignsByAccountIdResponse> GetCampaignsByAccountIdAsync(
            ApiAuthentication auth, 
            long accountId, 
            CampaignType campaignType, 
            long? customerId = null)
        {
            var request = new GetCampaignsByAccountIdRequest
            {
                AccountId = accountId,
                CampaignType = campaignType,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetCampaignsByAccountIdAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetCampaignsByAccountIdAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// Retrieves the specified campaigns from the specified account.
        /// https://msdn.microsoft.com/en-US/library/bing-ads-campaign-management-getcampaignsbyids.aspx
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="campaignIds">
        /// A maximum of 100 identifiers of the campaigns to get from the specified account.
        /// The identifiers must correspond to campaigns of the specified CampaignType or types, and otherwise the service will return error code EntityIdFilterMismatch (Code 516).</param>
        /// <param name="campaignType">The type of campaigns to get. You can specify one or more types.</param>
        /// <param name="accountId">The identifier of the account that contains the campaigns to get.</param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public GetCampaignsByIdsResponse GetCampaignsByIds(
            ApiAuthentication auth, 
            long[] campaignIds, 
            CampaignType campaignType, 
            long accountId, 
            long? customerId = null)
        {
            var request = new GetCampaignsByIdsRequest
            {
                CampaignIds = campaignIds,
                CampaignType = campaignType,
                AccountId = accountId,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetCampaignsByIds(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetCampaignsByIds", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetCampaignsByIdsResponse TryGetCampaignsByIds(
            ApiAuthentication auth, 
            long[] campaignIds, 
            CampaignType campaignType, 
            long accountId, 
            long? customerId = null)
        {
            return MethodHelper.TryGet(GetCampaignsByIds, this, auth, campaignIds, campaignType, accountId, customerId);
        }

        public async Task<GetCampaignsByIdsResponse> GetCampaignsByIdsAsync(
            ApiAuthentication auth, 
            long[] campaignIds, 
            CampaignType campaignType, 
            long accountId, 
            long? customerId = null)
        {
            var request = new GetCampaignsByIdsRequest
            {
                CampaignIds = campaignIds,
                CampaignType = campaignType,
                AccountId = accountId,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetCampaignsByIdsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetCampaignsByIdsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="campaignCriterionIds"></param>
        /// <param name="campaignId"></param>
        /// <param name="criterionType"></param>
        /// <param name="accountId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public GetCampaignCriterionsByIdsResponse GetCampaignCriterionsByIds(
            ApiAuthentication auth, 
            long[] campaignCriterionIds, 
            long campaignId, 
            CampaignCriterionType criterionType, 
            long? accountId, 
            long? customerId = null)
        {
            var request = new GetCampaignCriterionsByIdsRequest
            {
                CampaignCriterionIds = campaignCriterionIds,
                CampaignId = campaignId,
                CriterionType = criterionType,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetCampaignCriterionsByIds(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetCampaignCriterionsByIds", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetCampaignCriterionsByIdsResponse TryGetCampaignCriterionsByIds(
            ApiAuthentication auth, 
            long[] campaignCriterionIds, 
            long campaignId, 
            CampaignCriterionType criterionType, 
            long? accountId, 
            long? customerId = null)
        {
            return MethodHelper.TryGet(
                GetCampaignCriterionsByIds, 
                this, 
                auth, 
                campaignCriterionIds, 
                campaignId, 
                criterionType, 
                accountId,
                customerId);
        }

        public async Task<GetCampaignCriterionsByIdsResponse> GetCampaignCriterionsByIdsAsync(
            ApiAuthentication auth, 
            long[] campaignCriterionIds, 
            long campaignId, 
            CampaignCriterionType criterionType, 
            long? accountId, 
            long? customerId = null)
        {
            var request = new GetCampaignCriterionsByIdsRequest
            {
                CampaignCriterionIds = campaignCriterionIds,
                CampaignId = campaignId,
                CriterionType = criterionType,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetCampaignCriterionsByIdsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetCampaignCriterionsByIdsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="accountId"></param>
        /// <param name="campaignIds"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public GetCampaignSizesByAccountIdResponse GetCampaignSizesByAccountId(
            ApiAuthentication auth, 
            long accountId, 
            long[] campaignIds, 
            long? customerId = null)
        {
            var request = new GetCampaignSizesByAccountIdRequest
            {
                AccountId = accountId,
                CampaignIds = campaignIds,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetCampaignSizesByAccountId(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetCampaignSizesByAccountId", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetCampaignSizesByAccountIdResponse TryGetCampaignSizesByAccountId(
            ApiAuthentication auth, 
            long accountId, 
            long[] campaignIds, 
            long? customerId = null)
        {
            return MethodHelper.TryGet(
                GetCampaignSizesByAccountId, 
                this, 
                auth, 
                accountId, 
                campaignIds, 
                customerId);
        }

        public async Task<GetCampaignSizesByAccountIdResponse> GetCampaignSizesByAccountIdAsync(
            ApiAuthentication auth,
            long accountId,
            long[] campaignIds,
            long? customerId = null)
        {
            var request = new GetCampaignSizesByAccountIdRequest
            {
                AccountId = accountId,
                CampaignIds = campaignIds,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetCampaignSizesByAccountIdAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetCampaignSizesByAccountIdAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddCampaignsResponse AddCampaigns(
            ApiAuthentication auth, 
            long accountId, 
            long? customerId, 
            Campaign[] campaigns)
        {
            var request = new AddCampaignsRequest
            {
                AccountId = accountId,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                Campaigns = campaigns,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().AddCampaigns(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddCampaigns", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddCampaignsResponse TryAddCampaigns(
            ApiAuthentication auth,
            long accountId,
            long? customerId,
            Campaign[] campaigns)
        {
            return MethodHelper.TryGet(
                AddCampaigns,
                this,
                auth,
                accountId,
                customerId,
                campaigns);
        }

        public async Task<AddCampaignsResponse> AddCampaignsAsync(
            ApiAuthentication auth,
            long accountId,
            long? customerId,
            Campaign[] campaigns)
        {
            var request = new AddCampaignsRequest
            {
                AccountId = accountId,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                Campaigns = campaigns,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().AddCampaignsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddCampaignsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddAdGroupsResponse AddAdGroups(
            ApiAuthentication auth, 
            long? accountId, 
            long? customerId, 
            long campaignId, 
            AdGroup[] adGroups)
        {
            var request = new AddAdGroupsRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                CampaignId = campaignId,
                AdGroups = adGroups,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().AddAdGroups(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddAdGroups", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddAdGroupsResponse TryAddAdGroups(
            ApiAuthentication auth, 
            long? accountId, 
            long? customerId, 
            long campaignId, 
            AdGroup[] adGroups)
        {
            return MethodHelper.TryGet(AddAdGroups,
                this,
                auth,
                accountId,
                customerId,
                campaignId,
                adGroups);
        }

        public async Task<AddAdGroupsResponse> AddAdGroupsAsync(
            ApiAuthentication auth, 
            long? accountId, 
            long? customerId, 
            long campaignId, 
            AdGroup[] adGroups)
        {
            var request = new AddAdGroupsRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                CampaignId = campaignId,
                AdGroups = adGroups,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().AddAdGroupsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddAdGroupsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddAdsResponse AddAds(
            ApiAuthentication auth, 
            long? accountId, 
            long? customerId, 
            long adGroupId, 
            Ad[] ads)
        {
            var request = new AddAdsRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                AdGroupId = adGroupId,
                Ads = ads
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().AddAds(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddAds", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddAdsResponse TryAddAds(
            ApiAuthentication auth, 
            long? accountId, 
            long? customerId, 
            long adGroupId, 
            Ad[] ads)
        {
            return MethodHelper.TryGet(AddAds,
                this, 
                auth,
                accountId,
                customerId,
                adGroupId,
                ads);
        }

        public async Task<AddAdsResponse> AddAdsAsync(
            ApiAuthentication auth, 
            long? accountId,
            long? customerId, 
            long adGroupId, 
            Ad[] ads)
        {
            var request = new AddAdsRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                AdGroupId = adGroupId,
                Ads = ads
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().AddAdsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddAdsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddKeywordsResponse AddKeywords(
            ApiAuthentication auth,
            long? accountId,
            long? customerId, 
            long adGroupId, 
            Keyword[] keywords)
        {
            var request = new AddKeywordsRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                AdGroupId = adGroupId,
                Keywords = keywords,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().AddKeywords(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddKeywords", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddKeywordsResponse TryAddKeywords(
            ApiAuthentication auth,
            long? accountId,
            long? customerId, 
            long adGroupId, 
            Keyword[] keywords)
        {
            return MethodHelper.TryGet(AddKeywords, this, auth, accountId, customerId, adGroupId, keywords);
        }

        public async Task<AddKeywordsResponse> AddKeywordsAsync(
            ApiAuthentication auth,
            long? accountId,
            long? customerId, 
            long adGroupId, 
            Keyword[] keywords)
        {
            var request = new AddKeywordsRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                AdGroupId = adGroupId,
                Keywords = keywords,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().AddKeywordsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddKeywordsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }


        public GetAdGroupsByCampaignIdResponse GetAdGroupsByCampaignId(
            ApiAuthentication auth,
            long? accountId,
            long? customerId, 
            long campaignId)
        {
            var request = new GetAdGroupsByCampaignIdRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                CampaignId = campaignId
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetAdGroupsByCampaignId(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetAdGroupsByCampaignId", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetAdGroupsByCampaignIdResponse TryGetAdGroupsByCampaignId(
            ApiAuthentication auth,
            long? accountId,
            long? customerId, 
            long campaignId)
        {
            return MethodHelper.TryGet(GetAdGroupsByCampaignId, this, auth, accountId, customerId, campaignId);
        }

        public async Task<GetAdGroupsByCampaignIdResponse> GetAdGroupsByCampaignIdAsync(
            ApiAuthentication auth,
            long? accountId,
            long? customerId, 
            long campaignId)
        {
            var request = new GetAdGroupsByCampaignIdRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                CampaignId = campaignId
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetAdGroupsByCampaignIdAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetAdGroupsByCampaignIdAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetAdsByAdGroupIdResponse GetAdsByAdGroupId(
            ApiAuthentication auth,
            long? accountId,
            long? customerId, 
            long adGroupId)
        {
            var request = new GetAdsByAdGroupIdRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                AdGroupId = adGroupId,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetAdsByAdGroupId(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetAdsByAdGroupId", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetAdsByAdGroupIdResponse TryGetAdsByAdGroupId(
            ApiAuthentication auth,
            long? accountId,
            long? customerId, 
            long adGroupId)
        {
            return MethodHelper.TryGet(GetAdsByAdGroupId, this, auth, accountId, customerId, adGroupId);
        }

        public async Task<GetAdsByAdGroupIdResponse> GetAdsByAdGroupIdAsync(
            ApiAuthentication auth,
            long? accountId,
            long? customerId, 
            long adGroupId)
        {
            var request = new GetAdsByAdGroupIdRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                AdGroupId = adGroupId,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetAdsByAdGroupIdAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetAdsByAdGroupIdAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }


        public GetKeywordsByAdGroupIdResponse GetKeywordsByAdGroupId(
            ApiAuthentication auth,
            long? accountId,
            long? customerId, 
            long adGroupId)
        {
            var request = new GetKeywordsByAdGroupIdRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                AdGroupId = adGroupId,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetKeywordsByAdGroupId(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetKeywordsByAdGroupId", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetKeywordsByAdGroupIdResponse TryGetKeywordsByAdGroupId(
            ApiAuthentication auth,
            long? accountId,
            long? customerId, 
            long adGroupId)
        {
            return MethodHelper.TryGet(GetKeywordsByAdGroupId,
                this,
                auth,
                accountId,
                customerId,
                adGroupId);
        }

        public async Task<GetKeywordsByAdGroupIdResponse> GetKeywordsByAdGroupIdAsync(
            ApiAuthentication auth,
            long? accountId,
            long? customerId, 
            long adGroupId)
        {
            var request = new GetKeywordsByAdGroupIdRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                AdGroupId = adGroupId,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetKeywordsByAdGroupIdAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetKeywordsByAdGroupIdAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="campaignIds">100 CampaignIds most</param>
        /// <param name="customerId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public GetTargetsByCampaignIdsResponse GetTargetsByCampaignIds(
            ApiAuthentication auth,
            long[] campaignIds,
            long? customerId,
            long? accountId)
        {
            var request = new GetTargetsByCampaignIdsRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                CampaignIds = campaignIds,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetTargetsByCampaignIds(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetTargetsByCampaignIds", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="campaignIds">100 CampaignIds most</param>
        /// <param name="customerId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public GetTargetsByCampaignIdsResponse TryGetTargetsByCampaignIds(
            ApiAuthentication auth,
            long[] campaignIds,
            long? customerId,
            long? accountId)
        {
            return MethodHelper.TryGet(
                GetTargetsByCampaignIds, 
                this, 
                auth, 
                campaignIds, 
                customerId, 
                accountId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="campaignIds">100 CampaignIds most</param>
        /// <param name="customerId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<GetTargetsByCampaignIdsResponse> GetTargetsByCampaignIdsAsync(
            ApiAuthentication auth,
            long[] campaignIds,
            long? customerId,
            long? accountId)
        {
            var request = new GetTargetsByCampaignIdsRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                CampaignIds = campaignIds,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetTargetsByCampaignIdsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetTargetsByCampaignIdsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="adGroupIds">100 AdGroupIds most</param>
        /// <param name="customerId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public GetTargetsByAdGroupIdsResponse GetTargetsByAdGroupIds(
            ApiAuthentication auth,
            long[] adGroupIds,
            long? customerId,
            long? accountId)
        {
            var request = new GetTargetsByAdGroupIdsRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                AdGroupIds = adGroupIds
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetTargetsByAdGroupIds(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetTargetsByAdGroupIds", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="adGroupIds">100 AdGroupIds most</param>
        /// <param name="customerId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public GetTargetsByAdGroupIdsResponse TryGetTargetsByAdGroupIds(
            ApiAuthentication auth,
            long[] adGroupIds,
            long? customerId,
            long? accountId)
        {
            return MethodHelper.TryGet(GetTargetsByAdGroupIds,
                this,
                auth,
                adGroupIds,
                customerId,
                accountId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="adGroupIds">100 AdGroupIds most</param>
        /// <param name="customerId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<GetTargetsByAdGroupIdsResponse> GetTargetsByAdGroupIdsAsync(
            ApiAuthentication auth,
            long[] adGroupIds,
            long? customerId,
            long? accountId)
        {
            var request = new GetTargetsByAdGroupIdsRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                AdGroupIds = adGroupIds
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetTargetsByAdGroupIdsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetTargetsByAdGroupIdsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="targetIds">100 TargetIds most</param>
        /// <param name="customerId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public GetTargetsByIdsResponse GetTargetsByIds(
            ApiAuthentication auth,
            long[] targetIds,
            long? customerId,
            long? accountId)
        {
            var request = new GetTargetsByIdsRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                TargetIds = targetIds
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetTargetsByIds(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetTargetsByIds", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="targetIds">100 TargetIds most</param>
        /// <param name="customerId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public GetTargetsByIdsResponse TryGetTargetsByIds(
            ApiAuthentication auth,
            long[] targetIds,
            long? customerId,
            long? accountId)
        {
            return MethodHelper.TryGet(GetTargetsByIds,
                this,
                auth,
                targetIds,
                customerId,
                accountId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="targetIds">100 TargetIds most</param>
        /// <param name="customerId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<GetTargetsByIdsResponse> GetTargetsByIdsAsync(
            ApiAuthentication auth,
            long[] targetIds,
            long? customerId,
            long? accountId)
        {
            var request = new GetTargetsByIdsRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                TargetIds = targetIds
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetTargetsByIdsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetTargetsByIdsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetTargetsInfoFromLibraryResponse GetTargetsInfoFromLibrary(
            ApiAuthentication auth,
            long? customerId,
            long? accountId)
        {
            var request = new GetTargetsInfoFromLibraryRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetTargetsInfoFromLibrary(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetTargetsInfoFromLibrary", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetTargetsInfoFromLibraryResponse TryGetTargetsInfoFromLibrary(
           ApiAuthentication auth,
           long? customerId,
           long? accountId)
        {
            return MethodHelper.TryGet(
                GetTargetsInfoFromLibrary,
                this,
                auth,
                customerId,
                accountId);
        }

        public async Task<GetTargetsInfoFromLibraryResponse> GetTargetsInfoFromLibraryAsync(
           ApiAuthentication auth,
           long? customerId,
           long? accountId)
        {
            var request = new GetTargetsInfoFromLibraryRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetTargetsInfoFromLibraryAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetTargetsInfoFromLibraryAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }
    }
}