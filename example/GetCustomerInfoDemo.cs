using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MicrosoftOnline.Ads.BingAdsApi;
using MicrosoftOnline.Ads.BingAdsApi.V9.CustomerManagementService;

namespace BingAdsApiDemo
{
    class GetCustomerInfoDemo : IDemo
    {
        public long CustomerId { get; private set; }

        public async void Run()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------------Start------------------------");
            Console.WriteLine("Description:\tGetCustomerInfoDemo");

            try
            {
                var response = await GetCustomer();

                if (response != null && response.Customer != null)
                {
                    Console.WriteLine("Customer:\t{0}", response.Customer.Name);
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

        async Task<GetCustomerResponse> GetCustomer()
        {
            using(CustomerMHelper cs = new CustomerMHelper())
            {
                return await cs.GetCustomerAsync(Common.GetApiAuth(), Common.CustomerId);
            }
        }
    }
}
