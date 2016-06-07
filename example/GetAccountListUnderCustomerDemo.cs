using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MicrosoftOnline.Ads.BingAdsApi;
using MicrosoftOnline.Ads.BingAdsApi.V9.CustomerManagementService;

namespace BingAdsApiDemo
{
    class GetAccountListUnderCustomerDemo : IDemo
    {
        public long CustomerId { get; private set; }

        public async void Run()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("------------------------Start------------------------");
            Console.WriteLine("Description:\tGetAccountListUnderCustomerDemo");

            try
            {
                var response = await GetAccountsUnderCustomer();

                if (response != null && response.AccountsInfo != null && response.AccountsInfo.Length > 0)
                {
                    Console.WriteLine("Total {0} accounts", response.AccountsInfo.Length);

                    foreach(var account in response.AccountsInfo)
                    {
                        Console.WriteLine("AccountName:\t{0}\tStatus:\t{1}", account.Name, account.AccountLifeCycleStatus);
                    }
                }
            }
            catch (Exception ex)
            {
                MicrosoftOnline.Ads.LogAssistant.LogHelper.Error(
                    MicrosoftOnline.Ads.LogAssistant.LogCategoryType.Exception,
                    "Work",
                    ex.Message,
                    this,
                    ex);
            }

            Console.WriteLine("------------------------End--------------------------");
        }

        async Task<GetAccountsInfoResponse> GetAccountsUnderCustomer()
        {
            using (CustomerMHelper cs = new CustomerMHelper())
            {
                return await cs.GetAccountsInfoAsync(Common.GetApiAuth(), Common.CustomerId, true);
            }
        }
    }
}
