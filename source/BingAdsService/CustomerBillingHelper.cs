using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MicrosoftOnline.Ads.BingAdsApi.V9.CustomerBillingService;
using System.Threading.Tasks;

namespace MicrosoftOnline.Ads.BingAdsApi
{
    /// <summary>
    /// BingAds API V9 CustomerBilling Service
    /// https://msdn.microsoft.com/en-us/library/bing-ads-customer-billing-api-reference(v=msads.90).aspx
    /// </summary>
    public class CustomerBillingHelper : LogBase, IDisposable
    {
        private CustomerBillingServiceClient cs = null;

        public CustomerBillingHelper(EventHandler<LogEventArgs> handler = null)
            : base(handler)
        {
            cs = new CustomerBillingServiceClient("BasicHttpBinding_ICustomerBillingService");
        }

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

        public CustomerBillingServiceClient Check()
        {
            if (cs == null)
                cs = new CustomerBillingServiceClient("BasicHttpBinding_ICustomerBillingService");
            else if (cs.State != CommunicationState.Opened && cs.State != CommunicationState.Opening)
            {
                try
                {
                    cs.Open();
                }
                catch (Exception ex)
                {
                    Log(new LogEventArgs(ServiceType.Reporting, "Check", ex.Message, null, ex, null, LogLevel.Warn));
                }
            }
            return cs;
        }

        /// <summary>
        /// Adds an insertion order to the specified account.
        /// https://msdn.microsoft.com/en-US/library/bing-ads-billing-addinsertionorder(v=msads.90).aspx
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="order">An insertion order to add to the account specified in the InsertionOrder object.</param>
        /// <returns>AddInsertionOrderResponse</returns>
        public AddInsertionOrderResponse AddInsertionOrder(ApiAuthentication auth, InsertionOrder order)
        {
            var request = new AddInsertionOrderRequest
            {
                InsertionOrder = order,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().AddInsertionOrder(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "AddInsertionOrder", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddInsertionOrderResponse TryAddInsertionOrder(ApiAuthentication auth, InsertionOrder order)
        {
            return MethodHelper.TryGet(AddInsertionOrder, this, auth, order);
        }

        public async Task<AddInsertionOrderResponse> AddInsertionOrderAsync(ApiAuthentication auth, InsertionOrder order)
        {
            var request = new AddInsertionOrderRequest
            {
                InsertionOrder = order,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().AddInsertionOrderAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "AddInsertionOrderAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// Searches for insertion orders that match a specified criteria.
        /// https://msdn.microsoft.com/en-us/library/bing-ads-billing-searchinsertionorders(v=msads.90).aspx
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="ordering">
        /// Determines the order of results by the specified property of an account. Note: You should only specify one OrderBy element in the array. Additional elements are not supported and will be ignored by the service.
        /// For this service operation, the following values are supported in the Field element of a OrderBy object.
        /// Id - The order is determined by the InsertionOrderIdelement of the returned InsertionOrder.
        /// Name - The order is determined by the Name element of the returned InsertionOrder.</param>
        /// <param name="pageing">Determines the index and size of results per page.</param>
        /// <param name="predicates">Determines the request conditions. This operation's response will include accounts that match all of the specified predicates. Note: You may specify up to 6 predicates, and one of the predicate fields must be AccountId. You may use the StartDate and EndDate predicate fields twice each to specify start and end date ranges, and otherwise may only use each predicate field once.
        /// For a list of supported Field and Operator elements of a Predicate object for this service operation, see Predicate Field and Operator.</param>
        /// <returns>SearchInsertionOrdersResponse</returns>
        public SearchInsertionOrdersResponse SearchInsertionOrders(ApiAuthentication auth, OrderBy[] ordering, Paging pageing, Predicate[] predicates)
        {
            var request = new SearchInsertionOrdersRequest
            {
                Ordering = ordering,
                PageInfo = pageing,
                Predicates = predicates,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().SearchInsertionOrders(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "SearchInsertionOrders", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public SearchInsertionOrdersResponse TrySearchInsertionOrders(ApiAuthentication auth, OrderBy[] ordering, Paging pageing, Predicate[] predicates)
        {
            return MethodHelper.TryGet(SearchInsertionOrders, this, auth, ordering, pageing, predicates);
        }

        public async Task<SearchInsertionOrdersResponse> SearchInsertionOrdersAsync(ApiAuthentication auth, OrderBy[] ordering, Paging pageing, Predicate[] predicates)
        {
            var request = new SearchInsertionOrdersRequest
            {
                Ordering = ordering,
                PageInfo = pageing,
                Predicates = predicates,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().SearchInsertionOrdersAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "SearchInsertionOrdersAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// Updates an insertion order within the specified account.
        /// https://msdn.microsoft.com/en-us/library/bing-ads-billing-updateinsertionorder(v=msads.90).aspx
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="order">InsertionOrder</param>
        /// <returns>UpdateInsertionOrderResponse</returns>
        public UpdateInsertionOrderResponse UpdateInsertionOrder(ApiAuthentication auth, InsertionOrder order)
        {
            var request = new UpdateInsertionOrderRequest
            {
                InsertionOrder = order,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().UpdateInsertionOrder(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "UpdateInsertionOrder", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public UpdateInsertionOrderResponse TryUpdateInsertionOrder(ApiAuthentication auth, InsertionOrder order)
        {
            return MethodHelper.TryGet(UpdateInsertionOrder, this, auth, order);
        }

        public async Task<UpdateInsertionOrderResponse> UpdateInsertionOrderAsync(ApiAuthentication auth, InsertionOrder order)
        {
            var request = new UpdateInsertionOrderRequest
            {
                InsertionOrder = order,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().UpdateInsertionOrderAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "UpdateInsertionOrderAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// GetAccountMonthlySpend
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="accountId"></param>
        /// <param name="monthYear"></param>
        /// <returns>GetAccountMonthlySpendResponse</returns>
        public GetAccountMonthlySpendResponse GetAccountMonthlySpend(ApiAuthentication auth, long accountId, DateTime monthYear)
        {
            var request = new GetAccountMonthlySpendRequest
            {
                AccountId = accountId,
                MonthYear = monthYear,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetAccountMonthlySpend(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "GetAccountMonthlySpend", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetAccountMonthlySpendResponse TryGetAccountMonthlySpend(ApiAuthentication auth, long accountId, DateTime monthYear)
        {
            return MethodHelper.TryGet(GetAccountMonthlySpend, this, auth, accountId, monthYear);
        }

        public async Task<GetAccountMonthlySpendResponse> GetAccountMonthlySpendAsync(ApiAuthentication auth, long accountId, DateTime monthYear)
        {
            var request = new GetAccountMonthlySpendRequest
            {
                AccountId = accountId,
                MonthYear = monthYear,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetAccountMonthlySpendAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "GetAccountMonthlySpendAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// Gets a list of objects that contains billing document identification information, for example the billing document identifier, amount, and account identifier.
        /// https://msdn.microsoft.com/en-US/library/bing-ads-billing-getbillingdocumentsinfo(v=msads.90).aspx
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="accountIds">A list of identifiers of the accounts whose billing document information you want to get.</param>
        /// <param name="start">The start date to use for specifying the billing documents to get.
        /// The start date cannot be later than the end date. You must specify the date in Coordinated Universal Time (UTC).</param>
        /// <param name="end">
        /// The end date to use for specifying the billing documents to get.
        /// To specify today’s date as the end date, set EndDate to NULL.
        /// The end date cannot be earlier than the start date. You must specify the date in Coordinated Universal Time (UTC).</param>
        /// <returns></returns>
        public GetBillingDocumentsInfoResponse GetBillingDocumentsInfo(ApiAuthentication auth, long[] accountIds, DateTime start, DateTime end)
        {
            var request = new GetBillingDocumentsInfoRequest
            {
                AccountIds = accountIds,
                StartDate = start,
                EndDate = end,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetBillingDocumentsInfo(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "GetBillingDocumentsInfo", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetBillingDocumentsInfoResponse TryGetBillingDocumentsInfo(ApiAuthentication auth, long[] accountIds, DateTime start, DateTime end)
        {
            return MethodHelper.TryGet(GetBillingDocumentsInfo,
                this,
                auth,
                accountIds,
                start,
                end);
        }

        public async Task<GetBillingDocumentsInfoResponse> GetBillingDocumentsInfoAsync(ApiAuthentication auth, long[] accountIds, DateTime start, DateTime end)
        {
            var request = new GetBillingDocumentsInfoRequest
            {
                AccountIds = accountIds,
                StartDate = start,
                EndDate = end,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetBillingDocumentsInfoAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "GetBillingDocumentsInfoAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// Gets the specified billing documents.
        /// https://msdn.microsoft.com/en-us/library/bing-ads-billing-getbillingdocuments(v=msads.90).aspx
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="documentIds">A list of identifiers of the billing documents to get. To get a list of identifiers, call the GetBillingDocumentsInfo operation.
        /// You can most set 10 document Ids here</param>
        /// <param name="dataType">The format to use to generate the billing document. For example, you can generate the billing document in PDF or XML format.</param>
        /// <returns></returns>
        public GetBillingDocumentsResponse GetBillingDocuments(ApiAuthentication auth, long[] documentIds, DataType dataType = DataType.Xml)
        {
            var request = new GetBillingDocumentsRequest
            {
                DocumentIds = documentIds,
                Type = dataType,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetBillingDocuments(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "GetBillingDocuments", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// Gets the specified billing documents.
        /// https://msdn.microsoft.com/en-us/library/bing-ads-billing-getbillingdocuments(v=msads.90).aspx
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="documentIds">A list of identifiers of the billing documents to get. To get a list of identifiers, call the GetBillingDocumentsInfo operation.
        /// You can most set 10 document Ids here</param>
        /// <param name="dataType">The format to use to generate the billing document. For example, you can generate the billing document in PDF or XML format.</param>
        /// <returns></returns>
        public GetBillingDocumentsResponse TryGetBillingDocuments(ApiAuthentication auth, long[] documentIds, DataType dataType = DataType.Xml)
        {
            return MethodHelper.TryGet(GetBillingDocuments, this, auth, documentIds, dataType);
        }

        /// <summary>
        /// Gets the specified billing documents.
        /// https://msdn.microsoft.com/en-us/library/bing-ads-billing-getbillingdocuments(v=msads.90).aspx
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="documentIds">A list of identifiers of the billing documents to get. To get a list of identifiers, call the GetBillingDocumentsInfo operation.
        /// You can most set 10 document Ids here</param>
        /// <param name="dataType">The format to use to generate the billing document. For example, you can generate the billing document in PDF or XML format.</param>
        /// <returns></returns>
        public async Task<GetBillingDocumentsResponse> GetBillingDocumentsAsync(ApiAuthentication auth, long[] documentIds, DataType dataType = DataType.Xml)
        {
            var request = new GetBillingDocumentsRequest
            {
                DocumentIds = documentIds,
                Type = dataType,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetBillingDocumentsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "GetBillingDocumentsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }


        /// <summary>
        /// Gets a list of insertion orders for the specified account.
        /// https://msdn.microsoft.com/en-us/library/bing-ads-billing-getinsertionordersbyaccount(v=msads.90).aspx
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="accountId">The identifier of the account that contains the insertion orders to get.</param>
        /// <param name="insertionOrderIds">A list of identifiers of the insertion orders to get. To get all insertion orders, including those that have expired, set to NULL.</param>
        /// <returns></returns>
        public GetInsertionOrdersByAccountResponse GetInsertionOrdersByAccount(ApiAuthentication auth, long accountId, long[] insertionOrderIds = null)
        {
            var request = new GetInsertionOrdersByAccountRequest
            {
                AccountId = accountId,
                InsertionOrderIds = insertionOrderIds,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetInsertionOrdersByAccount(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "GetInsertionOrdersByAccount", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetInsertionOrdersByAccountResponse TryGetInsertionOrdersByAccount(ApiAuthentication auth, long accountId, long[] insertionOrderIds = null)
        {
            return MethodHelper.TryGet(GetInsertionOrdersByAccount, this, auth, accountId, insertionOrderIds);
        }

        public async Task<GetInsertionOrdersByAccountResponse> GetInsertionOrdersByAccountAsync(ApiAuthentication auth, long accountId, long[] insertionOrderIds = null)
        {
            var request = new GetInsertionOrdersByAccountRequest
            {
                AccountId = accountId,
                InsertionOrderIds = insertionOrderIds,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetInsertionOrdersByAccountAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "GetInsertionOrdersByAccountAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }


        public GetDisplayInvoicesResponse GetDisplayInvoices(ApiAuthentication auth, long[] invoiceIds, DataType dataType)
        {
            var request = new GetDisplayInvoicesRequest
            {
                InvoiceIds = invoiceIds,
                Type = dataType,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetDisplayInvoices(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "GetDisplayInvoices", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetDisplayInvoicesResponse TryGetDisplayInvoices(ApiAuthentication auth, long[] invoiceIds, DataType dataType)
        {
            return MethodHelper.TryGet(GetDisplayInvoices, this, auth, invoiceIds, dataType);
        }

        public async Task<GetDisplayInvoicesResponse> GetDisplayInvoicesAsync(ApiAuthentication auth, long[] invoiceIds, DataType dataType)
        {
            var request = new GetDisplayInvoicesRequest
            {
                InvoiceIds = invoiceIds,
                Type = dataType,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetDisplayInvoicesAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "GetDisplayInvoicesAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }


        public GetKOHIOInvoicesResponse GetKOHIOInvoices(ApiAuthentication auth, string[] invoiceIds)
        {
            var request = new GetKOHIOInvoicesRequest
            {
                InvoiceIds = invoiceIds,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetKOHIOInvoices(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "GetKOHIOInvoices", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetKOHIOInvoicesResponse TryGetKOHIOInvoices(ApiAuthentication auth, string[] invoiceIds)
        {
            return MethodHelper.TryGet(GetKOHIOInvoices, this, auth, invoiceIds);
        }

        public async Task<GetKOHIOInvoicesResponse> GetKOHIOInvoicesAsync(ApiAuthentication auth, string[] invoiceIds)
        {
            var request = new GetKOHIOInvoicesRequest
            {
                InvoiceIds = invoiceIds,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetKOHIOInvoicesAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "GetKOHIOInvoicesAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }
    }
}