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

        public AddAdExtensionsResponse AddAdExtensions(
            ApiAuthentication auth,
            long accountId,
            AdExtension[] adExtensions,
            long? customerId)
        {
            var request = new AddAdExtensionsRequest
            {
                AccountId = accountId,
                AdExtensions = adExtensions,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().AddAdExtensions(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddAdExtensions", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public async Task<AddAdExtensionsResponse> AddAdExtensionsAsync(
            ApiAuthentication auth,
            long accountId,
            AdExtension[] adExtensions,
            long? customerId)
        {
            var request = new AddAdExtensionsRequest
            {
                AccountId = accountId,
                AdExtensions = adExtensions,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().AddAdExtensionsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddAdExtensionsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddAdExtensionsResponse TryAddAdExtensions(
            ApiAuthentication auth,
            long accountId,
            AdExtension[] adExtensions,
            long? customerId)
        {
            return MethodHelper.TryGet(AddAdExtensions, this, auth, accountId, adExtensions, customerId);
        }

        public SetAdExtensionsAssociationsResponse SetAdExtensionsAssociations(
            ApiAuthentication auth,
            long accountId,
            AdExtensionIdToEntityIdAssociation[] assiciations,
            AssociationType associationType,
            long? customerId)
        {
            var request = new SetAdExtensionsAssociationsRequest
            {
                AccountId = accountId,
                AdExtensionIdToEntityIdAssociations = assiciations,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                AssociationType = associationType
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().SetAdExtensionsAssociations(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "SetAdExtensionsAssociations", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public async Task<SetAdExtensionsAssociationsResponse> SetAdExtensionsAssociationsAsync(
            ApiAuthentication auth,
            long accountId,
            AdExtensionIdToEntityIdAssociation[] assiciations,
            AssociationType associationType,
            long? customerId)
        {
            var request = new SetAdExtensionsAssociationsRequest
            {
                AccountId = accountId,
                AdExtensionIdToEntityIdAssociations = assiciations,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                AssociationType = associationType
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().SetAdExtensionsAssociationsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "SetAdExtensionsAssociationsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public SetAdExtensionsAssociationsResponse TrySetAdExtensionsAssociations(
            ApiAuthentication auth,
            long accountId,
            AdExtensionIdToEntityIdAssociation[] assiciations,
            AssociationType associationType,
            long? customerId)
        {
            return MethodHelper.TryGet(SetAdExtensionsAssociations, this, auth, accountId, assiciations, associationType, customerId);
        }

        public SetTargetToCampaignResponse SetTargetToCampaign(
            ApiAuthentication auth,
            long campaignId,
            long targerId,
            bool replaceAssociation,
            long accountId,
            long customerId)
        {
            var request = new SetTargetToCampaignRequest
            {
                CampaignId = campaignId,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                ReplaceAssociation = replaceAssociation,
                TargetId = targerId
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().SetTargetToCampaign(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "SetTargetToCampaign", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public async Task<SetTargetToCampaignResponse> SetTargetToCampaignAsync(
            ApiAuthentication auth,
            long campaignId,
            long targerId,
            bool replaceAssociation,
            long accountId,
            long customerId)
        {
            var request = new SetTargetToCampaignRequest
            {
                CampaignId = campaignId,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                ReplaceAssociation = replaceAssociation,
                TargetId = targerId
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().SetTargetToCampaignAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "SetTargetToCampaignAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public SetTargetToCampaignResponse TrySetTargetToCampaign(
            ApiAuthentication auth,
            long campaignId,
            long targerId,
            bool replaceAssociation,
            long accountId,
            long customerId)
        {
            return MethodHelper.TryGet(SetTargetToCampaign, this, auth, campaignId, targerId, replaceAssociation, accountId, customerId);
        }

        public SetTargetToAdGroupResponse SetTargetToAdGroup(
            ApiAuthentication auth,
            long adGroupId,
            long targerId,
            bool replaceAssociation,
            long accountId,
            long customerId)
        {
            var request = new SetTargetToAdGroupRequest
            {
                AdGroupId = adGroupId,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                ReplaceAssociation = replaceAssociation,
                TargetId = targerId
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().SetTargetToAdGroup(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "SetTargetToAdGroup", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public async Task<SetTargetToAdGroupResponse> SetTargetToAdGroupAsync(
            ApiAuthentication auth,
            long adGroupId,
            long targerId,
            bool replaceAssociation,
            long accountId,
            long customerId)
        {
            var request = new SetTargetToAdGroupRequest
            {
                AdGroupId = adGroupId,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                ReplaceAssociation = replaceAssociation,
                TargetId = targerId
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().SetTargetToAdGroupAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "SetTargetToAdGroupAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public SetTargetToAdGroupResponse TrySetTargetToAdGroup(
            ApiAuthentication auth,
            long adGroupId,
            long targerId,
            bool replaceAssociation,
            long accountId,
            long customerId)
        {
            return MethodHelper.TryGet(SetTargetToAdGroup, this, auth, adGroupId, targerId, replaceAssociation, accountId, customerId);
        }

        public AddBudgetsResponse AddBudgets(
            ApiAuthentication auth,
            Budget[] budgets,
            long accountId,
            long customerId)
        {
            var request = new AddBudgetsRequest
            {
                Budgets = budgets,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().AddBudgets(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddBudgets", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public async Task<AddBudgetsResponse> AddBudgetsAsync(
           ApiAuthentication auth,
           Budget[] budgets,
           long accountId,
           long customerId)
        {
            var request = new AddBudgetsRequest
            {
                Budgets = budgets,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().AddBudgetsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddBudgetsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddBudgetsResponse TryAddBudgets(
            ApiAuthentication auth,
            Budget[] budgets,
            long accountId,
            long customerId)
        {
            return MethodHelper.TryGet(TryAddBudgets, this, auth, budgets, accountId, customerId);
        }

        public AddSharedEntityResponse AddSharedEntity(
            ApiAuthentication auth,
            SharedEntity shareEntity,
            SharedListItem[] sharedListItems,
            long accountId,
            long customerId)
        {
            var request = new AddSharedEntityRequest
            {
                SharedEntity = shareEntity,
                ListItems = sharedListItems,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().AddSharedEntity(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddSharedEntity", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public async Task<AddSharedEntityResponse> AddSharedEntityAsync(
            ApiAuthentication auth,
            SharedEntity shareEntity,
            SharedListItem[] sharedListItems,
            long accountId,
            long customerId)
        {
            var request = new AddSharedEntityRequest
            {
                SharedEntity = shareEntity,
                ListItems = sharedListItems,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().AddSharedEntityAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddSharedEntityAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddSharedEntityResponse TryAddSharedEntity(
            ApiAuthentication auth,
            SharedEntity shareEntity,
            SharedListItem[] sharedListItems,
            long accountId,
            long customerId)
        {
            return MethodHelper.TryGet(AddSharedEntity, this, auth, shareEntity, sharedListItems, accountId, customerId);
        }

        public AddNegativeKeywordsToEntitiesResponse AddNegativeKeywordsToEntities(
            ApiAuthentication auth,
            EntityNegativeKeyword[] entityNegativeKeywords,
            long accountId,
            long customerId)
        {
            var request = new AddNegativeKeywordsToEntitiesRequest
            {
                EntityNegativeKeywords = entityNegativeKeywords,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().AddNegativeKeywordsToEntities(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddNegativeKeywordsToEntities", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public async Task<AddNegativeKeywordsToEntitiesResponse> AddNegativeKeywordsToEntitiesAsync(
           ApiAuthentication auth,
           EntityNegativeKeyword[] entityNegativeKeywords,
           long accountId,
           long customerId)
        {
            var request = new AddNegativeKeywordsToEntitiesRequest
            {
                EntityNegativeKeywords = entityNegativeKeywords,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().AddNegativeKeywordsToEntitiesAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddNegativeKeywordsToEntitiesAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddNegativeKeywordsToEntitiesResponse TryAddNegativeKeywordsToEntities(
           ApiAuthentication auth,
           EntityNegativeKeyword[] entityNegativeKeywords,
           long accountId,
           long customerId)
        {
            return MethodHelper.TryGet(AddNegativeKeywordsToEntities, this, auth, entityNegativeKeywords, accountId, customerId);
        }

        public AddTargetsToLibraryResponse AddTargetsToLibrary(
            ApiAuthentication auth,
            Target[] targets,
            long accountId,
           long customerId)
        {
            var request = new AddTargetsToLibraryRequest
            {
                Targets = targets,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().AddTargetsToLibrary(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddTargetsToLibrary", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public async Task<AddTargetsToLibraryResponse> AddTargetsToLibraryAsync(
            ApiAuthentication auth,
            Target[] targets,
            long accountId,
           long customerId)
        {
            var request = new AddTargetsToLibraryRequest
            {
                Targets = targets,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().AddTargetsToLibraryAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddTargetsToLibraryAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddTargetsToLibraryResponse TryAddTargetsToLibrary(
            ApiAuthentication auth,
            Target[] targets,
            long accountId,
           long customerId)
        {
            return MethodHelper.TryGet(AddTargetsToLibrary, this, auth, targets, accountId, customerId);
        }

        public AddUetTagsResponse AddUetTags(
            ApiAuthentication auth,
            UetTag[] tags,
            long accountId,
            long customerId)
        {
            var request = new AddUetTagsRequest
            {
                UetTags = tags,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().AddUetTags(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddUetTags", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public async Task<AddUetTagsResponse> AddUetTagsAsync(
            ApiAuthentication auth,
            UetTag[] tags,
            long accountId,
            long customerId)
        {
            var request = new AddUetTagsRequest
            {
                UetTags = tags,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().AddUetTagsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "AddUetTagsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddUetTagsResponse TryAddUetTags(
            ApiAuthentication auth,
            UetTag[] tags,
            long accountId,
            long customerId)
        {
            return MethodHelper.TryGet(AddUetTags, this, auth, tags, accountId, customerId);
        }

        public GetBudgetsByIdsResponse GetBudgetsByIds(
            ApiAuthentication auth,
            long[] ids,
            long accountId,
            long customerId)
        {
            var request = new GetBudgetsByIdsRequest
            {
                BudgetIds = ids,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetBudgetsByIds(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetBudgetsByIds", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public async Task<GetBudgetsByIdsResponse> GetBudgetsByIdsAsync(
            ApiAuthentication auth,
            long[] ids,
            long accountId,
            long customerId)
        {
            var request = new GetBudgetsByIdsRequest
            {
                BudgetIds = ids,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetBudgetsByIdsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetBudgetsByIdsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetBudgetsByIdsResponse TryGetBudgetsByIds(
            ApiAuthentication auth,
            long[] ids,
            long accountId,
            long customerId)
        {
            return MethodHelper.TryGet(GetBudgetsByIds, this, auth, ids, accountId, customerId);
        }

        public GetAdExtensionIdsByAccountIdResponse GetAdExtensionIdsByAccountId(
            ApiAuthentication auth,
            AdExtensionsTypeFilter extensionType,
            AssociationType associationType,
            long accountId,
            long customerId)
        {
            var request = new GetAdExtensionIdsByAccountIdRequest
            {
                AccountId = accountId,
                AdExtensionType = extensionType,
                AssociationType = associationType,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetAdExtensionIdsByAccountId(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetAdExtensionIdsByAccountId", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public async Task<GetAdExtensionIdsByAccountIdResponse> GetAdExtensionIdsByAccountIdAsync(
            ApiAuthentication auth,
            AdExtensionsTypeFilter extensionType,
            AssociationType associationType,
            long accountId,
            long customerId)
        {
            var request = new GetAdExtensionIdsByAccountIdRequest
            {
                AccountId = accountId,
                AdExtensionType = extensionType,
                AssociationType = associationType,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetAdExtensionIdsByAccountIdAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetAdExtensionIdsByAccountIdAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetAdExtensionIdsByAccountIdResponse TryGetAdExtensionIdsByAccountId(
            ApiAuthentication auth,
            AdExtensionsTypeFilter extensionType,
            AssociationType associationType,
            long accountId,
            long customerId)
        {
            return MethodHelper.TryGet(GetAdExtensionIdsByAccountId, this, auth, extensionType, associationType, accountId, customerId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="extensionType">Filters the returned associations by ad extension type.</param>
        /// <param name="associationType">Filters the returned associations by entity type.</param>
        /// <param name="entityIds">The list of entity identifiers by which you may request the respective ad extension associations.</param>
        /// <param name="adExtensionAdditionalField">The list of additional elements that you want included within each returned AdExtension object. This set of flags enables you to get the latest features using the current version of Bing Ads Campaign Management API, and in the next version the corresponding elements will be included by default.</param>
        /// <param name="accountId">The identifier of the account that owns the extensions.</param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public GetAdExtensionsAssociationsResponse GetAdExtensionsAssociations(
            ApiAuthentication auth,
            AdExtensionsTypeFilter extensionType,
            AssociationType associationType,
            long[] entityIds,
            AdExtensionAdditionalField? adExtensionAdditionalField,
            long accountId,
            long customerId)
        {
            var request = new GetAdExtensionsAssociationsRequest
            {
                AccountId = accountId,
                AdExtensionType = extensionType,
                AssociationType = associationType,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                EntityIds = entityIds,
                ReturnAdditionalFields = adExtensionAdditionalField,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetAdExtensionsAssociations(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetAdExtensionsAssociations", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public async Task<GetAdExtensionsAssociationsResponse> GetAdExtensionsAssociationsAsync(
            ApiAuthentication auth,
            AdExtensionsTypeFilter extensionType,
            AssociationType associationType,
            long[] entityIds,
            AdExtensionAdditionalField? adExtensionAdditionalField,
            long accountId,
            long customerId)
        {
            var request = new GetAdExtensionsAssociationsRequest
            {
                AccountId = accountId,
                AdExtensionType = extensionType,
                AssociationType = associationType,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                EntityIds = entityIds,
                ReturnAdditionalFields = adExtensionAdditionalField,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetAdExtensionsAssociationsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetAdExtensionsAssociationsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetAdExtensionsAssociationsResponse TryGetAdExtensionsAssociations(
           ApiAuthentication auth,
           AdExtensionsTypeFilter extensionType,
           AssociationType associationType,
           long[] entityIds,
           AdExtensionAdditionalField? adExtensionAdditionalField,
           long accountId,
           long customerId)
        {
            return MethodHelper.TryGet(GetAdExtensionsAssociations, this, auth, extensionType, associationType, entityIds, adExtensionAdditionalField, accountId, customerId);
        }

        public GetAdExtensionsByIdsResponse GetAdExtensionsByIds(
            ApiAuthentication auth,
            AdExtensionsTypeFilter extensionType,
            AdExtensionAdditionalField? adExtensionAdditionalField,
            long[] ids,
            long accountId,
            long customerId)
        {
            var request = new GetAdExtensionsByIdsRequest
            {
                AccountId = accountId,
                AdExtensionType = extensionType,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                ReturnAdditionalFields = adExtensionAdditionalField,
                AdExtensionIds = ids
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetAdExtensionsByIds(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetAdExtensionsByIds", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public async Task<GetAdExtensionsByIdsResponse> GetAdExtensionsByIdsAsync(
            ApiAuthentication auth,
            AdExtensionsTypeFilter extensionType,
            AdExtensionAdditionalField? adExtensionAdditionalField,
            long[] ids,
            long accountId,
            long customerId)
        {
            var request = new GetAdExtensionsByIdsRequest
            {
                AccountId = accountId,
                AdExtensionType = extensionType,
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                ReturnAdditionalFields = adExtensionAdditionalField,
                AdExtensionIds = ids
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetAdExtensionsByIdsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "GetAdExtensionsByIdsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetAdExtensionsByIdsResponse TryGetAdExtensionsByIds(
            ApiAuthentication auth,
            AdExtensionsTypeFilter extensionType,
            AdExtensionAdditionalField? adExtensionAdditionalField,
            long[] ids,
            long accountId,
            long customerId)
        {
            return MethodHelper.TryGet(GetAdExtensionsByIds, this, auth, extensionType, adExtensionAdditionalField, ids, accountId, customerId);
        }

        public GetTargetsInfoFromLibraryResponse GetTargetsInfoFromLibrary(
            ApiAuthentication auth,
            long accountId,
            long customerId)
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

        public async Task<GetTargetsInfoFromLibraryResponse> GetTargetsInfoFromLibraryAsync(
            ApiAuthentication auth,
            long accountId,
            long customerId)
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

        public GetTargetsInfoFromLibraryResponse TryGetTargetsInfoFromLibrary(
            ApiAuthentication auth,
            long accountId,
            long customerId)
        {
            return MethodHelper.TryGet(GetTargetsInfoFromLibrary, this, auth, accountId, customerId);
        }

        public SetSharedEntityAssociationsResponse SetSharedEntityAssociations(
            ApiAuthentication auth,
            SharedEntityAssociation[] sharedEntityAssociations,
            long accountId,
            long customerId)
        {
            var request = new SetSharedEntityAssociationsRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                Associations = sharedEntityAssociations,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().SetSharedEntityAssociations(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "SetSharedEntityAssociations", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public async Task<SetSharedEntityAssociationsResponse> SetSharedEntityAssociationsAsync(
            ApiAuthentication auth,
            SharedEntityAssociation[] sharedEntityAssociations,
            long accountId,
            long customerId)
        {
            var request = new SetSharedEntityAssociationsRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                Associations = sharedEntityAssociations,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().SetSharedEntityAssociationsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CampaignManagement, "SetSharedEntityAssociationsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public SetSharedEntityAssociationsResponse TrySetSharedEntityAssociations(
            ApiAuthentication auth,
            SharedEntityAssociation[] sharedEntityAssociations,
            long accountId,
            long customerId)
        {
            return MethodHelper.TryGet(SetSharedEntityAssociations, this, auth, sharedEntityAssociations, accountId, customerId);
        }
    }
}