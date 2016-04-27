using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrosoftOnline.Ads.BingAdsApi;
using MicrosoftOnline.Ads.BingAdsApi.V9.ReportingService;
using MicrosoftOnline.Ads.BingAdsApi.V9.CustomerManagementService;

namespace BingAdsApiDemo
{
    class DownloadAccountPerformanceReportDemo : IDemo
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public long CustomerId { get; private set; }
        public string SaveFilePath { get; private set; }

        public DownloadAccountPerformanceReportDemo(DateTime start, DateTime end, long customerId, string zipFilePath)
        {
            this.StartDate = start;
            this.EndDate = end;
            this.CustomerId = customerId;
            this.SaveFilePath = zipFilePath;
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
        }

        ReportRequest BuildRequest(long[] accountIds)
        {
            return new AccountPerformanceReportRequest
            {
                Aggregation = ReportAggregation.Daily,
                Columns = new AccountPerformanceReportColumn[]
                {
                    AccountPerformanceReportColumn.AccountId,
                    AccountPerformanceReportColumn.AccountName,
                    AccountPerformanceReportColumn.AccountNumber,
                    AccountPerformanceReportColumn.AdDistribution,
                    AccountPerformanceReportColumn.DeviceOS,
                    AccountPerformanceReportColumn.DeviceType,
                    AccountPerformanceReportColumn.CurrencyCode,
                    AccountPerformanceReportColumn.Network,
                    AccountPerformanceReportColumn.TopVsOther,
                    AccountPerformanceReportColumn.AverageCpc,
                    AccountPerformanceReportColumn.Impressions,
                    AccountPerformanceReportColumn.Clicks,
                    AccountPerformanceReportColumn.Spend,
                    AccountPerformanceReportColumn.TimePeriod
                },
                Filter = new AccountPerformanceReportFilter
                {
                    AdDistribution = null,
                    DeviceOS = null,
                    DeviceType = null
                },
                Format = ReportFormat.Tsv,
                Language = ReportLanguage.English,
                ReportName = "AccountPerformanceReport",
                ReturnOnlyCompleteData = true,
                Scope = new AccountReportScope
                {
                    AccountIds = accountIds,
                },
                Time = new ReportTime()
                {
                    CustomDateRangeStart = new Date()
                    {
                        Year = this.StartDate.Year,
                        Month = this.StartDate.Month,
                        Day = this.StartDate.Day
                    },
                    CustomDateRangeEnd = new Date()
                    {
                        Year = this.EndDate.Year,
                        Month = this.EndDate.Month,
                        Day = this.EndDate.Day
                    },
                }
            };
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
