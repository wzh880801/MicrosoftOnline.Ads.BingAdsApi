using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicrosoftOnline.Ads.BingAdsApi.V9.CustomerManagementService;
using System.ServiceModel;
using System.Threading.Tasks;

namespace MicrosoftOnline.Ads.BingAdsApi
{
    /// <summary>
    /// BingAds API V9 CustomerManagement Service
    /// https://msdn.microsoft.com/en-us/library/bing-ads-customer-management-api-reference(v=msads.90).aspx
    /// </summary>
    public class CustomerMHelper : LogBase, IDisposable
    {
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

        private CustomerManagementServiceClient cs = null;

        public CustomerMHelper(EventHandler<LogEventArgs> handler = null)
            : base(handler)
        {
            cs = new CustomerManagementServiceClient("BasicHttpBinding_ICustomerManagementService");
        }

        private CustomerManagementServiceClient Check()
        {
            if (cs == null)
                cs = new CustomerManagementServiceClient("BasicHttpBinding_ICustomerManagementService");
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
        /// Gets a list of objects that contain customer identification information, for example the name and identifier of the customer.
        /// https://msdn.microsoft.com/en-US/library/dn451282(v=msads.90).aspx
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="_customerNameFilter">A partial or full name of the customers that you want to get. 
        /// The operation includes the customer in the result if the customer’s name begins with the specified filter name. 
        /// If you do not want to filter by customer name, set this element to an empty string.
        /// The operation performs a case-insensitive comparison when it compares your name filter value to the customer names. 
        /// For example, if you specify “t” as the name filter, the list will include customers whose names begin with “t” or “T”.</param>
        /// <param name="_topN">A nonzero positive integer that specifies the number of customers to return in the result.</param>
        /// <returns>An array of CustomerInfo objects that identifies the list of customers that meet the filter criteria.
        /// To get the customer data for a customer in the list, access the Id element of the CustomerInfo object and use it to call the GetCustomer operation.</returns>
        public GetCustomersInfoResponse GetCustomersInfo(ApiAuthentication auth, string _customerNameFilter, int _topN)
        {
            var request = new GetCustomersInfoRequest
            {
                //A value that determines whether to return results for advertising customers or publishing customers. If you do not specify the scope, the list may include both types of customers.
                ApplicationScope = ApplicationType.Advertiser,
                CustomerNameFilter = _customerNameFilter ?? "",
                TopN = _topN,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetCustomersInfo(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "GetCustomersInfo", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetCustomersInfoResponse TryGetCustomersInfo(ApiAuthentication auth, string _customerNameFilter, int _topN)
        {
            return MethodHelper.TryGet(GetCustomersInfo, this, auth, _customerNameFilter, _topN);
        }

        public async Task<GetCustomersInfoResponse> GetCustomersInfoAsync(ApiAuthentication auth, string _customerNameFilter, int _topN)
        {
            var request = new GetCustomersInfoRequest
            {
                //A value that determines whether to return results for advertising customers or publishing customers. If you do not specify the scope, the list may include both types of customers.
                ApplicationScope = ApplicationType.Advertiser,
                CustomerNameFilter = _customerNameFilter ?? "",
                TopN = _topN,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetCustomersInfoAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "GetCustomersInfoAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// Gets the details of a customer.
        /// https://msdn.microsoft.com/en-US/library/dn451279(v=msads.90).aspx
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="customerId">The identifier of the customer whose information you want to get.</param>
        /// <returns>A Customer object that contains information about the customer.</returns>
        public GetCustomerResponse GetCustomer(ApiAuthentication auth, long customerId)
        {
            var request = new GetCustomerRequest
            {
                CustomerId = customerId,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetCustomer(request);
            }
            catch (Exception ex)
            {           
                Log(new LogEventArgs(ServiceType.CustomerManagement, "GetCustomer", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetCustomerResponse TryGetCustomer(ApiAuthentication auth, long customerId)
        {
            return MethodHelper.TryGet(GetCustomer, this, auth, customerId);
        }

        public async Task<GetCustomerResponse> GetCustomerAsync(ApiAuthentication auth, long customerId)
        {
            var request = new GetCustomerRequest
            {
                CustomerId = customerId,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetCustomerAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "GetCustomerAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// Gets the details of an account.
        /// https://msdn.microsoft.com/en-US/library/dn451273(v=msads.90).aspx
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="accountId">The identifier of the account to get.</param>
        /// <returns>An account object that contains information about the account, such as payment method, account type, and parent customer.</returns>
        public GetAccountResponse GetAccount(ApiAuthentication auth, long accountId)
        {
            var request = new GetAccountRequest
            {
                AccountId = accountId,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetAccount(request);
            }
            catch (Exception ex)
            {               
                Log(new LogEventArgs(ServiceType.CustomerManagement, "GetAccount", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetAccountResponse TryGetAccount(ApiAuthentication auth, long accountId)
        {
            return MethodHelper.TryGet(GetAccount, this, auth, accountId);
        }

        public async Task<GetAccountResponse> GetAccountAsync(ApiAuthentication auth, long accountId)
        {
            var request = new GetAccountRequest
            {
                AccountId = accountId,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetAccountAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "GetAccountAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// Gets a list of objects that contains account identification information, for example the name and identifier of the account, for the specified customer.
        /// https://msdn.microsoft.com/en-US/library/dn451289(v=msads.90).aspx
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="customerId">The identifier of the customer who owns the accounts to get. If not set, the user’s credentials are used to determine the customer.</param>
        /// <param name="onlyParentAccounts">Determines whether to return only the accounts that belong to the customer or to also return the accounts that the customer manages for other customers. 
        /// To return all accounts (those that belong to the customer and those that the customer manages), set this element to false; 
        /// otherwise, set to true to return account information for only the specified customer. The default is false.</param>
        /// <returns>An array of AccountInfo objects that identifies the list of accounts that the customer owns.
        /// To get the account data for an account in the list, access the Id element of the AccountInfo object and use it to call the GetAccount operation.
        ///Note that there can be a delay of up to five minutes from the time that you add an account until the GetAccountsInfo operation includes the account in the response.</returns>
        public GetAccountsInfoResponse GetAccountsInfo(ApiAuthentication auth, long? customerId, bool onlyParentAccounts)
        {
            var request = new GetAccountsInfoRequest
            {
                CustomerId = customerId,
                OnlyParentAccounts = onlyParentAccounts,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetAccountsInfo(request);
            }
            catch (Exception ex)
            {             
                Log(new LogEventArgs(ServiceType.CustomerManagement, "GetAccountsInfo", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetAccountsInfoResponse TryGetAccountsInfo(ApiAuthentication auth, long? customerId, bool onlyParentAccounts)
        {
            return MethodHelper.TryGet(GetAccountsInfo, this, auth, customerId, onlyParentAccounts);
        }

        public async Task<GetAccountsInfoResponse> GetAccountsInfoAsync(ApiAuthentication auth, long? customerId, bool onlyParentAccounts)
        {
            var request = new GetAccountsInfoRequest
            {
                CustomerId = customerId,
                OnlyParentAccounts = onlyParentAccounts,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetAccountsInfoAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "GetAccountsInfoAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// Gets the details of a user.
        /// https://msdn.microsoft.com/en-US/library/dn451280(v=msads.90).aspx
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="userId">The identifier of the user to get. Note: If this element is null or not provided, the response will include details for the authenticated user specified in the request header.</param>
        /// <returns>
        /// Accounts:  An array of identifiers of the accounts to which the user has access permissions. If the Roles element contains an account role and the Accounts element contains a 0 (zero)-length array, it indicates that the user has access permissions to all of the customer’s accounts.
        /// Customers: An array of identifiers of the customers to which the user has access permissions. If the Roles element contains a customer role and the Customers element contains a 0 (zero)-length array, it indicates that the user has access permissions to all customers.
        /// Roles:     An array of roles that determines the permissions that the user has to manage the customer or account data.
        ///            Possible values include the following:
        ///               16 - The user has the Advertiser Campaign Manager role.
        ///               33 - The user has the Aggregator role.
        ///               41 - The user has the Super Admin role.
        ///               100 - The user has the Viewer role.
        ///            For more information, see User Roles and Available Service Operations.
        ///            Note: The list above provides examples of possible return values. Other values might be returned. Deprecated or internal roles can be included in the response.
        ///User:       A user object that contains information about the user.</returns>
        public GetUserResponse GetUser(ApiAuthentication auth, long? userId)
        {
            var request = new GetUserRequest
            {
                UserId = userId
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetUser(request);
            }
            catch (Exception ex)
            {              
                Log(new LogEventArgs(ServiceType.CustomerManagement, "GetUser", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetUserResponse TryGetUser(ApiAuthentication auth, long? userId)
        {
            return MethodHelper.TryGet(GetUser, this, auth, userId);
        }

        public async Task<GetUserResponse> GetUserAsync(ApiAuthentication auth, long? userId)
        {
            var request = new GetUserRequest
            {
                UserId = userId
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetUserAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "GetUserAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// Gets a list of objects that contains user identification information, for example the user name and identifier of the user.
        /// https://msdn.microsoft.com/en-US/library/dn451283(v=msads.90).aspx
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="customerId">The identifier of the customer to which the users belong.</param>
        /// <param name="statusFilter">The status value that the operation uses to filter the list of users that it returns. The operation returns only those users with the specified status.</param>
        /// <returns>
        /// UsersInfo : A list of UserInfo objects that identifies the list of users who meet the filter criteria.
        ///             To get the user data for a user in the list, access the Id element of the UserInfo object and use it to call the GetUser operation.</returns>
        public GetUsersInfoResponse GetUsersInfo(ApiAuthentication auth, long customerId, UserLifeCycleStatus? statusFilter)
        {
            var request = new GetUsersInfoRequest
            {
                CustomerId = customerId,
                StatusFilter = statusFilter
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetUsersInfo(request);
            }
            catch (Exception ex)
            {   
                Log(new LogEventArgs(ServiceType.CustomerManagement, "GetUsersInfo", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetUsersInfoResponse TryGetUsersInfo(ApiAuthentication auth, long customerId, UserLifeCycleStatus? statusFilter)
        {
            return MethodHelper.TryGet(GetUsersInfo, this, auth, customerId, statusFilter);
        }

        public async Task<GetUsersInfoResponse> GetUsersInfoAsync(ApiAuthentication auth, long customerId, UserLifeCycleStatus? statusFilter)
        {
            var request = new GetUsersInfoRequest
            {
                CustomerId = customerId,
                StatusFilter = statusFilter
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetUsersInfoAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "GetUsersInfoAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public GetAccessibleCustomerResponse GetAccessibleCustomer(ApiAuthentication auth, long customerId)
        {
            var request = new GetAccessibleCustomerRequest
            {
                CustomerId = customerId,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetAccessibleCustomer(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "GetAccessibleCustomer", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetAccessibleCustomerResponse TryGetAccessibleCustomer(ApiAuthentication auth, long customerId)
        {
            return MethodHelper.TryGet(GetAccessibleCustomer, this, auth, customerId);
        }

        public async Task<GetAccessibleCustomerResponse> GetAccessibleCustomerAsync(ApiAuthentication auth, long customerId)
        {
            var request = new GetAccessibleCustomerRequest
            {
                CustomerId = customerId,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetAccessibleCustomerAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "GetAccessibleCustomerAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// Gets a list of the pilot programs in which the specified customer participates.
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public GetCustomerPilotFeaturesResponse GetCustomerPilotFeatures(ApiAuthentication auth, long customerId)
        {
            var request = new GetCustomerPilotFeaturesRequest
            {
                CustomerId = customerId
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetCustomerPilotFeatures(request);
            }
            catch (Exception ex)
            {  
                Log(new LogEventArgs(ServiceType.CustomerManagement, "GetCustomerPilotFeatures", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetCustomerPilotFeaturesResponse TryGetCustomerPilotFeatures(ApiAuthentication auth, long customerId)
        {
            return MethodHelper.TryGet(GetCustomerPilotFeatures, this, auth, customerId);
        }

        public async Task<GetCustomerPilotFeaturesResponse> GetCustomerPilotFeaturesAsync(ApiAuthentication auth, long customerId)
        {
            var request = new GetCustomerPilotFeaturesRequest
            {
                CustomerId = customerId
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetCustomerPilotFeaturesAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "GetCustomerPilotFeaturesAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// Sends an invitation for a Microsoft account user to manage one or more Bing Ads customer accounts. When the invitation is accepted, the user's Microsoft account is linked to the specified Bing Ads customer accounts. For more information about user authentication, see Managing User Authentication with OAuth.
        /// https://msdn.microsoft.com/en-US/library/bing-ads-customer-management-senduserinvitation(v=msads.90).aspx
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="userInvitation">The user invitation to send.
        /// https://msdn.microsoft.com/en-US/library/bing-ads-customer-management-userinvitation(v=msads.90).aspx</param>
        /// <returns>
        /// UserInvitationId: A system-generated identifier for the user invitation that was sent.</returns>
        public SendUserInvitationResponse SendUserInvitation(ApiAuthentication auth, UserInvitation userInvitation)
        {
            var request = new SendUserInvitationRequest
            {
                UserInvitation = userInvitation,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().SendUserInvitation(request);
            }
            catch (Exception ex)
            { 
                Log(new LogEventArgs(ServiceType.CustomerManagement, "SendUserInvitation", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public SendUserInvitationResponse TrySendUserInvitation(ApiAuthentication auth, UserInvitation userInvitation)
        {
            return MethodHelper.TryGet(SendUserInvitation, this, auth, userInvitation);
        }

        public async Task<SendUserInvitationResponse> SendUserInvitationAsync(ApiAuthentication auth, UserInvitation userInvitation)
        {
            var request = new SendUserInvitationRequest
            {
                UserInvitation = userInvitation,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().SendUserInvitationAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "SendUserInvitationAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// Signs up a customer with Bing Ads.
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="account">An Account object that specifies the details of the customer’s primary account.
        /// Note: Do not instantiate the Account data object. Instead, instantiate the AdvertiserAccount that derives from the Account data object.
        /// https://msdn.microsoft.com/en-US/library/bing-ads-customer-management-account(v=msads.90).aspx</param>
        /// <param name="applicationType">Determines the type of customer application. The default is Advertiser.The scope of this customer and the scope of the parent customer must be the same; for example, they must both be set to Advertiser.</param>
        /// <param name="customer">A Customer object that specifies the details of the customer that you are adding.
        /// https://msdn.microsoft.com/en-US/library/bing-ads-customer-management-customer(v=msads.90).aspx</param>
        /// <param name="parentCustomerId">The customer identifier of the reseller that will manage this customer.</param>
        /// <returns>
        /// AccountId:      A system-generated account identifier corresponding to the new account specified in the request.
        ///                 Use this identifier with operation requests that require an AccountId element and a CustomerAccountId SOAP header element.
        /// AccountNumber:  A system-generated account number that is used to identify the account in the Bing Ads web application. 
        ///                 The account number has the form, Xnnnnnnn, where nnnnnnn is a series of digits.
        /// CreateTime:     The date and time that the account was added. The date and time value reflects the date and time at the server, not the client. 
        ///                 For information about the format of the date and time, see the dateTime entry in Primitive XML Data Types.
        /// CustomerId:     A system-generated customer identifier corresponding to the new customer specified in the request.
        ///                 Use this identifier with operation requests that require a CustomerId SOAP header element.
        /// CustomerNumber: A system-generated customer number that is used in the Bing Ads web application. The customer number is of the form, Cnnnnnnn, where nnnnnnn is a series of digits.</returns>
        public SignupCustomerResponse SignupCustomer(ApiAuthentication auth, Account account, ApplicationType applicationType, Customer customer, long parentCustomerId)
        {
            var request = new SignupCustomerRequest
            {
                Account = account,
                ApplicationScope = applicationType,
                Customer = customer,
                ParentCustomerId = parentCustomerId,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().SignupCustomer(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "SignupCustomer", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public SignupCustomerResponse TrySignupCustomer(ApiAuthentication auth, Account account, ApplicationType applicationType, Customer customer, long parentCustomerId)
        {
            return MethodHelper.TryGet(SignupCustomer, this, auth, account, applicationType, customer, parentCustomerId);
        }

        public async Task<SignupCustomerResponse> SignupCustomerAsync(ApiAuthentication auth, Account account, ApplicationType applicationType, Customer customer, long parentCustomerId)
        {
            var request = new SignupCustomerRequest
            {
                Account = account,
                ApplicationScope = applicationType,
                Customer = customer,
                ParentCustomerId = parentCustomerId,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().SignupCustomerAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "SignupCustomerAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// Updates the details of the specified account.
        /// https://msdn.microsoft.com/en-US/library/dn451286(v=msads.90).aspx
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="account">
        /// AdvertiserAccount:  An AdvertiserAccount object that contains the updated account information.This operation overwrites the existing account data with the contents of the account object that you pass. 
        ///                     This operation performs a full update, and not a partial update. The Account object must contain the time stamp value from the last time that the Account object was written to. 
        ///                     To ensure that the time stamp contains the correct value, call the GetAccount operation. You can then update the account data as appropriate, and call UpdateAccount.
        ///                     https://msdn.microsoft.com/en-US/library/bing-ads-customer-management-advertiseraccount(v=msads.90).aspx</param>
        /// <returns>UpdateAccountResponse</returns>
        public UpdateAccountResponse UpdateAccount(ApiAuthentication auth, Account account)
        {
            var request = new UpdateAccountRequest
            {
                Account = account,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().UpdateAccount(request);
            }
            catch (Exception ex)
            {  
                Log(new LogEventArgs(ServiceType.CustomerManagement, "UpdateAccount", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public UpdateAccountResponse TryUpdateAccount(ApiAuthentication auth, Account account)
        {
            return MethodHelper.TryGet(UpdateAccount, this, auth, account);
        }

        public async Task<UpdateAccountResponse> UpdateAccountAsync(ApiAuthentication auth, Account account)
        {
            var request = new UpdateAccountRequest
            {
                Account = account,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().UpdateAccountAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "UpdateAccountAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// Updates the details of the specified customer.
        /// https://msdn.microsoft.com/en-US/library/dn451294(v=msads.90).aspx
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="customer">A customer object that contains the updated customer information.
        /// This operation overwrites the existing customer data with the contents of the customer object that you pass. 
        /// This operation performs a full update, and not a partial update. 
        /// The Customer object must contain the time stamp value from the last time that the Customer object was written to. 
        /// To ensure that the time stamp contains the correct value, call the GetCustomer operation. 
        /// You can then update the customer data as appropriate, and call UpdateCustomer.</param>
        /// <returns>UpdateCustomerResponse</returns>
        public UpdateCustomerResponse UpdateCustomer(ApiAuthentication auth, Customer customer)
        {
            var request = new UpdateCustomerRequest
            {
                Customer = customer,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().UpdateCustomer(request);
            }
            catch (Exception ex)
            {   
                Log(new LogEventArgs(ServiceType.CustomerManagement, "UpdateCustomer", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public UpdateCustomerResponse TryUpdateCustomer(ApiAuthentication auth, Customer customer)
        {
            return MethodHelper.TryGet(UpdateCustomer, this, auth, customer);
        }

        public async Task<UpdateCustomerResponse> UpdateCustomerAsync(ApiAuthentication auth, Customer customer)
        {
            var request = new UpdateCustomerRequest
            {
                Customer = customer,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().UpdateCustomerAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "UpdateCustomerAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/bing-ads-customer-management-addclientlinks(v=msads.90).aspx
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="clientLinks"></param>
        /// <returns></returns>
        public AddClientLinksResponse AddClientLinks(ApiAuthentication auth, ClientLink[] clientLinks)
        {
            var request = new AddClientLinksRequest
            {
                ClientLinks = clientLinks,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().AddClientLinks(request);
            }
            catch (Exception ex)
            {    
                Log(new LogEventArgs(ServiceType.CustomerManagement, "AddClientLinks", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddClientLinksResponse TryAddClientLinks(ApiAuthentication auth, ClientLink[] clientLinks)
        {
            return MethodHelper.TryGet(AddClientLinks, this, auth, clientLinks);
        }

        public async Task<AddClientLinksResponse> AddClientLinksAsync(ApiAuthentication auth, ClientLink[] clientLinks)
        {
            var request = new AddClientLinksRequest
            {
                ClientLinks = clientLinks,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().AddClientLinksAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "AddClientLinksAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/bing-ads-customer-management-updateclientlinks(v=msads.90).aspx
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="clientLinks"></param>
        /// <returns></returns>
        public UpdateClientLinksResponse UpdateClientLinks(ApiAuthentication auth, ClientLink[] clientLinks)
        {
            var request = new UpdateClientLinksRequest
            {
                ClientLinks = clientLinks,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().UpdateClientLinks(request);
            }
            catch (Exception ex)
            {   
                Log(new LogEventArgs(ServiceType.CustomerManagement, "UpdateClientLinks", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public UpdateClientLinksResponse TryUpdateClientLinks(ApiAuthentication auth, ClientLink[] clientLinks)
        {
            return MethodHelper.TryGet(UpdateClientLinks, this, auth, clientLinks);
        }

        public async Task<UpdateClientLinksResponse> UpdateClientLinksAsync(ApiAuthentication auth, ClientLink[] clientLinks)
        {
            var request = new UpdateClientLinksRequest
            {
                ClientLinks = clientLinks,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().UpdateClientLinksAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "UpdateClientLinksAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/bing-ads-customer-management-searchclientlinks(v=msads.90).aspx
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="orderBy">Optional</param>
        /// <param name="paging">Required</param>
        /// <param name="predicate">Required</param>
        /// <returns></returns>
        public SearchClientLinksResponse SearchClientLinks(ApiAuthentication auth, OrderBy[] orderBy, Paging paging, Predicate[] predicates)
        {
            var request = new SearchClientLinksRequest
            {
                Ordering = orderBy,
                PageInfo = paging,
                Predicates = predicates
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().SearchClientLinks(request);
            }
            catch (Exception ex)
            {    
                Log(new LogEventArgs(ServiceType.CustomerManagement, "SearchClientLinks", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public SearchClientLinksResponse TrySearchClientLinks(ApiAuthentication auth, OrderBy[] orderBy, Paging paging, Predicate[] predicates)
        {
            return MethodHelper.TryGet(SearchClientLinks, this, auth, orderBy, paging, predicates);
        }

        public async Task<SearchClientLinksResponse> SearchClientLinksAsync(ApiAuthentication auth, OrderBy[] orderBy, Paging paging, Predicate[] predicates)
        {
            var request = new SearchClientLinksRequest
            {
                Ordering = orderBy,
                PageInfo = paging,
                Predicates = predicates
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().SearchClientLinksAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "SearchClientLinksAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddAccountResponse AddAccount(ApiAuthentication auth, AdvertiserAccount account)
        {
            var request = new AddAccountRequest
            {
                Account = account,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().AddAccount(request);
            }
            catch (Exception ex)
            {  
                Log(new LogEventArgs(ServiceType.CustomerManagement, "AddAccount", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddAccountResponse TryAddAccount(ApiAuthentication auth, AdvertiserAccount account)
        {
            return MethodHelper.TryGet(AddAccount, this, auth, account);
        }

        public async Task<AddAccountResponse> AddAccountAsync(ApiAuthentication auth, AdvertiserAccount account)
        {
            var request = new AddAccountRequest
            {
                Account = account,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().AddAccountAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "AddAccountAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddPrepayAccountResponse AddPrepayAccount(ApiAuthentication auth, AdvertiserAccount prePayAccount)
        {
            var request = new AddPrepayAccountRequest
            {
                Account = prePayAccount
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().AddPrepayAccount(request);
            }
            catch (Exception ex)
            {  
                Log(new LogEventArgs(ServiceType.CustomerManagement, "AddPrepayAccount", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public AddPrepayAccountResponse TryAddPrepayAccount(ApiAuthentication auth, AdvertiserAccount prePayAccount)
        {
            return MethodHelper.TryGet(AddPrepayAccount, this, auth, prePayAccount);
        }

        public async Task<AddPrepayAccountResponse> AddPrepayAccountAsync(ApiAuthentication auth, AdvertiserAccount prePayAccount)
        {
            var request = new AddPrepayAccountRequest
            {
                Account = prePayAccount
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().AddPrepayAccountAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "AddPrepayAccountAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public DeleteAccountResponse DeleteAccount(ApiAuthentication auth, long accountId)
        {
            var _request = GetAccount(auth, accountId);
            if (_request == null)
                return null;

            var request = new DeleteAccountRequest
            {
                AccountId = accountId,
                TimeStamp = _request.Account.TimeStamp,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().DeleteAccount(request);
            }
            catch (Exception ex)
            {  
                Log(new LogEventArgs(ServiceType.CustomerManagement, "DeleteAccount", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public DeleteAccountResponse TryDeleteAccount(ApiAuthentication auth, long accountId)
        {
            return MethodHelper.TryGet(DeleteAccount, this, auth, accountId);
        }

        public async Task<DeleteAccountResponse> DeleteAccountAsync(ApiAuthentication auth, long accountId)
        {
            var _request = GetAccount(auth, accountId);
            if (_request == null)
                return null;

            var request = new DeleteAccountRequest
            {
                AccountId = accountId,
                TimeStamp = _request.Account.TimeStamp,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().DeleteAccountAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerManagement, "DeleteAccountAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }
    }
}