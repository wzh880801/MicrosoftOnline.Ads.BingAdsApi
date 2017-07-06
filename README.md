# MicrosoftOnline.Ads.BingAdsApi
.NET SDK for BingAds API

[![Build Status](https://travis-ci.org/wzh880801/MicrosoftOnline.Ads.BingAdsApi.svg?branch=master)](https://travis-ci.org/wzh880801/MicrosoftOnline.Ads.BingAdsApi)

`NuGet`: 
<br>You could get `MicrosoftOnline.Ads.Log.dll` & `Microsoft.Ads.BingAdsApi.dll` from [MyNuGet](http://nuget.esobing.com/nuget/) [`http://nuget.esobing.com/nuget/`](http://nuget.esobing.com/nuget/)

# Usage:

```C#
using MicrosoftOnline.Ads.BingAdsApi;
using MicrosoftOnline.Ads.BingAdsApi.V9.ReportingService;
using MicrosoftOnline.Ads.BingAdsApi.V9.CustomerManagementService;

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

            //you can submit a report request which could most contain 1000 accountIds per time
            //if you have more than 1000 accounts, then you should submit the report request every 1000 accountIds
            //for demo, we just pick up the first 1000 accountIds
            if (accounts.Count > 1000)
                accounts = accounts.TakeWhile((p, i) => { return i < 1000; }).ToList();

            using (ReportingHelper rs = new ReportingHelper(LogHandler))
            {
                //Submit & download
                var succeed = 
                    rs.TrySubmitGenerateReport(auth, BuildRequest(accounts.Select(p => p.Id).ToArray()), CustomerId, null, SaveFilePath);
            }

            using(FileProcessor fp = new FileProcessor(null, Process, null, SaveFilePath))
            {
                fp.Process();
            }
```

#Support async...


