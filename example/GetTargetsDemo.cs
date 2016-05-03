using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrosoftOnline.Ads.BingAdsApi;
using MicrosoftOnline.Ads.BingAdsApi.V10.CampaignManagementService;
using MicrosoftOnline.Ads.BingAdsApi.V9.CustomerManagementService;

namespace BingAdsApiDemo
{
    class GetTargetsDemo : IDemo
    {
        public long CustomerId { get; private set; }

        public GetTargetsDemo(long customerId)
        {
            this.CustomerId = customerId;
        }

        public void Run()
        {
            ApiAuthentication auth = null;

            //auth = new PasswordAuthentication("username", "passsword", "developerToken");

            auth = new OAuthAuthentication(
                "accessToken",
                "refreshToken",
                "developerToken",
                DateTime.UtcNow.Ticks,
                "clientId");

            IList<AccountInfo> accounts = null;
            using (CustomerMHelper cs = new CustomerMHelper(LogHandler))
            {
                var response = cs.TryGetAccountsInfo(auth, CustomerId, true);
                if (response != null && response.AccountsInfo != null)
                    accounts = response.AccountsInfo.ToList();
            }

            if (accounts == null || accounts.Count == 0)
                return;

            foreach(var account in accounts)
            {
                Campaign[] campaigns = null;
                using(CampaignMHelper cs = new CampaignMHelper(LogHandler))
                {
                    var response = cs.TryGetCampaignsByAccountId(
                        auth,
                        account.Id,
                        CampaignType.SearchAndContent,
                        CustomerId);

                    if (response == null)
                        continue;

                    campaigns = response.Campaigns;
                }

                if (campaigns == null || campaigns.Length == 0)
                    continue;

                foreach(var campaign in campaigns)
                {
                    Target[] targets = null;
                    using(CampaignMHelper cs = new CampaignMHelper(LogHandler))
                    {
                        var response = cs.TryGetTargetsByCampaignIds(
                            auth,
                            new long[] { campaign.Id.Value },
                            CustomerId,
                            account.Id);

                        if (response == null)
                            continue;

                        targets = response.Targets;
                    }

                    if (targets == null || targets.Length == 0)
                        continue;

                    foreach(Target target in targets)
                    {
                        var deviceTarget = target.DeviceOS;
                        if (deviceTarget == null || deviceTarget.Bids == null)
                            continue;

                        foreach (var bid in deviceTarget.Bids)
                        {
                            Console.WriteLine("Customer:\t{0}", CustomerId);
                            Console.WriteLine("Account:\t{0}", account.Id);
                            Console.WriteLine("Campaign:\t{0}", campaign.Id);
                            Console.WriteLine("DeviceName:\t{0}", bid.DeviceName);
                            Console.WriteLine("BidAdjustment:\t{0}", bid.BidAdjustment);
                            Console.WriteLine("OSNames:\t{0}", string.Join(",", bid.OSNames));
                        }
                    }
                }
            }
        }

        void LogHandler(object sender, LogEventArgs e)
        {
            MicrosoftOnline.Ads.LogAssistant.LogEventArgs _e =
                new MicrosoftOnline.Ads.LogAssistant.LogEventArgs(
                    e.LogDateTime,
                    MicrosoftOnline.Ads.LogAssistant.LogLevel.Error,
                    MicrosoftOnline.Ads.LogAssistant.LogCategoryType.Exception,
                    e.EntryPoint,
                    e.Message,
                    e.Parameters,
                    e.Exception,
                    e.TrackingId
                );
            MicrosoftOnline.Ads.LogAssistant.LogHelper.Log(_e);
        }
    }
}
