# MicrosoftOnline.Ads.BingAdsApi
.NET SDK for BingAds API

`NuGet`: 
You could get MicrosoftOnline.Ads.Log.dll & Microsoft.Ads.BingAdsApi.dll from [MyNuGet](http://nuget.esobing.com/nuget/)`http://nuget.esobing.com/nuget/`

# Usage:

```c#
using MicrosoftOnline.Ads.BingAdsApi;
using MicrosoftOnline.Ads.BingAdsApi.V9.ReportingService;
using MicrosoftOnline.Ads.BingAdsApi.V9.CustomerManagementService;

using (CustomerMHelper cs = new CustomerMHelper(LogHandler))
{
    var response = cs.TryGetAccountsInfo(auth, CustomerId, true);
}

using (ReportingHelper rs = new ReportingHelper(LogHandler))
{
    //Submit & download
    var succeed = 
        rs.TrySubmitGenerateReport(auth, BuildRequest(accounts.Select(p => p.Id).ToArray()), CustomerId, null, SaveFilePath);
}
```

#Support async...


