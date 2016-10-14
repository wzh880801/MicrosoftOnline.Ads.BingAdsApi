using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrosoftOnline.Ads.BingAdsApi;
using MicrosoftOnline.Ads.BingAdsApi.V9.CustomerManagementService;
using MicrosoftOnline.Ads.BingAdsApi.V9.CustomerBillingService;

namespace BingAdsApiDemo
{
    class GetAccountInsertionOrdersDemo : IDemo
    {
        public void Run()
        {
            var api = Common.GetApiAuth();

            //get account list
            AccountInfo[] accounts = null;
            using(CustomerMHelper cs = new CustomerMHelper())
            {
                var request = cs.TryGetAccountsInfo(api, Common.CustomerId, true);
                if (request != null)
                    accounts = request.AccountsInfo;
            }

            if (accounts == null || accounts.Length == 0)
                return;

            var _accountIds = accounts.Where(p => p.AccountLifeCycleStatus != AccountLifeCycleStatus.Inactive).Select(p => p.Id).ToList();

            Console.WriteLine("Account #:\t{0}", _accountIds.Count);

            using (CustomerBillingHelper cs = new CustomerBillingHelper())
            {
                foreach (var id in _accountIds)
                {
                    Console.WriteLine("\tGetting {0}'s Insertion Orders...", id);

                    var orderBy = new MicrosoftOnline.Ads.BingAdsApi.V9.CustomerBillingService.OrderBy[]
                    {
                        new MicrosoftOnline.Ads.BingAdsApi.V9.CustomerBillingService.OrderBy
                        {
                            Field =  MicrosoftOnline.Ads.BingAdsApi.V9.CustomerBillingService.OrderByField.Id,
                            Order = MicrosoftOnline.Ads.BingAdsApi.V9.CustomerBillingService.SortOrder.Ascending
                        }
                    };

                    var paging = new MicrosoftOnline.Ads.BingAdsApi.V9.CustomerBillingService.Paging
                    {
                        Index = 0,
                        Size = 100
                    };

                    var predicate = new MicrosoftOnline.Ads.BingAdsApi.V9.CustomerBillingService.Predicate[]
                    {
                        new MicrosoftOnline.Ads.BingAdsApi.V9.CustomerBillingService.Predicate
                        {
                            Field = "AccountId",
                            Operator = MicrosoftOnline.Ads.BingAdsApi.V9.CustomerBillingService.PredicateOperator.Equals,
                            Value = string.Format("{0}", id)
                        },
                        new MicrosoftOnline.Ads.BingAdsApi.V9.CustomerBillingService.Predicate
                        {
                            Field = "EndDate",
                            Operator = MicrosoftOnline.Ads.BingAdsApi.V9.CustomerBillingService.PredicateOperator.GreaterThanEquals,
                            Value = string.Format("{0:yyyy-MM-dd}", DateTime.UtcNow)
                        }
                    };

                    var request = cs.TrySearchInsertionOrders(api, orderBy, paging, predicate);

                    if (request != null && request.InsertionOrders != null)
                    {
                        var sum = request.InsertionOrders.Where(p => p.Status == InsertionOrderStatus.Active).Sum(p => p.BalanceAmount);

                        Console.WriteLine("\t--> {0}\t{1}", id, sum);
                    }
                    else
                    {
                        Console.WriteLine("\t-->NULL");
                    }
                }
            }
        }
    }
}
