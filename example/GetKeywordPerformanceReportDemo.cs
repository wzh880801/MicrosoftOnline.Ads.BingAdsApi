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
    public class GetKeywordPerformanceReportDemo : IDemo
    {
        static string AppPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public long CustomerId { get; private set; }

        public GetKeywordPerformanceReportDemo(DateTime start, DateTime end, long customerId)
        {
            this.StartDate = start;
            this.EndDate = end;
            this.CustomerId = customerId;
        }

        public void Run()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("------------------------Start------------------------");
            Console.WriteLine("Description:\tKeywordPerformanceReportDemo");

            try
            {
                Work();
            }
            catch (Exception ex)
            {
                MicrosoftOnline.Ads.LogAssistant.LogHelper.Error(
                    MicrosoftOnline.Ads.LogAssistant.LogCategoryType.Exception,
                    "Work",
                    ex.Message,
                    new
                    {
                        CustomerId = this.CustomerId,
                        Start = this.StartDate,
                        End = this.EndDate
                    },
                    ex);
            }

            Console.WriteLine("------------------------End--------------------------");
        }

        void Work()
        {
            IList<AccountInfo> accounts = null;
            using (CustomerMHelper cs = new CustomerMHelper(LogHandler))
            {
                //will try for 3 times if error occured
                var response = cs.TryGetAccountsInfo(Common.GetApiAuth(), CustomerId, true);

                //if you want to throw exception
                //check whethere the response is null
                if (response == null)
                    throw new BingAdsApiException("Error occured when calling BingAdsAPI - GetAccountsInfo");

                if (response != null && response.AccountsInfo != null)
                    accounts = response.AccountsInfo.ToList();
            }

            if (accounts == null || accounts.Count == 0)
                return;

            //每次最多只能提交1000个账户，超过1000要分多次提交
            var ids = accounts.Select(p => p.Id).ToList();
            var perIds = new List<long>();
            while (ids.Count > 0)
            {
                perIds.Add(ids[0]);
                ids.RemoveAt(0);

                if (perIds.Count == 1000 || ids.Count == 0)
                {
                    DownloadReportAndProcess(perIds.ToArray());

                    perIds.Clear();
                }
            } 
        }

        void DownloadReportAndProcess(long[] accountIds)
        {
            var _reportFilePath = System.IO.Path.Combine(AppPath, "reports");
            if (!System.IO.Directory.Exists(_reportFilePath))
                System.IO.Directory.CreateDirectory(_reportFilePath);

            var _file = System.IO.Path.Combine(_reportFilePath, string.Format("{0}.zip", Guid.NewGuid().ToString()));

            using (ReportingHelper rs = new ReportingHelper(LogHandler))
            {
                //Submit & download
                //will try for most 3 times when error occured
                var succeed =
                    rs.TrySubmitGenerateReport(
                        //API Auth info
                        Common.GetApiAuth(),
                        //Report Request
                        BuildRequest(accountIds),
                        //Your customerId
                        CustomerId,
                        //Your accountId
                        null,
                        //local path in which you want to store the report data
                        _file);
            }

            //if you need process the report file
            //use this fileprocessor
            //it can help you UnZip the file and process the rows & return the values of each row
            using (TsvReportFileProcessor fp = new TsvReportFileProcessor(
                //you can use this handler to handle the errors
                LogHandler,
                //you can use this handler to receive the values of each row
                Process,
                //implement this handler if you want to receive a notification when process progress changed
                null,
                //the full path of the zip file
                _file))
            {
                var b = fp.Process();
            }
        }

        ReportRequest BuildRequest(long[] accountIds)
        {
            return new KeywordPerformanceReportRequest
            {
                Aggregation = ReportAggregation.Daily,
                Columns = new KeywordPerformanceReportColumn[]
                {
                    KeywordPerformanceReportColumn.AccountId,
                    KeywordPerformanceReportColumn.AccountName,
                    KeywordPerformanceReportColumn.AccountNumber,
                    KeywordPerformanceReportColumn.Impressions,
                    KeywordPerformanceReportColumn.Clicks,
                    KeywordPerformanceReportColumn.Spend,
                    KeywordPerformanceReportColumn.Conversions,
                    KeywordPerformanceReportColumn.TimePeriod
                },
                Filter = null,
                //if you want to use the fileProcessor to process the report file
                //then you must set the Format to TSV
                Format = ReportFormat.Tsv,
                Language = ReportLanguage.English,
                ReportName = "KeywordPerformanceReport",
                ReturnOnlyCompleteData = true,
                Scope = new AccountThroughAdGroupReportScope
                {
                    AccountIds = accountIds
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
            if (e.LogLevel == LogLevel.Error || e.LogLevel == LogLevel.Warn)
                Console.WriteLine("Error/Warn occured while calling BingAds API, see the Log for details.");

            var _logLevel = MicrosoftOnline.Ads.LogAssistant.LogLevel.Error;
            switch (e.LogLevel)
            {
                case LogLevel.Error:
                    break;
                case LogLevel.Info:
                    _logLevel = MicrosoftOnline.Ads.LogAssistant.LogLevel.Info;
                    break;
                case LogLevel.Warn:
                    _logLevel = MicrosoftOnline.Ads.LogAssistant.LogLevel.Warn;
                    break;
            }

            MicrosoftOnline.Ads.LogAssistant.LogEventArgs _e =
                new MicrosoftOnline.Ads.LogAssistant.LogEventArgs(
                    e.LogDateTime,
                    _logLevel,
                    _logLevel == MicrosoftOnline.Ads.LogAssistant.LogLevel.Error ? MicrosoftOnline.Ads.LogAssistant.LogCategoryType.Exception : MicrosoftOnline.Ads.LogAssistant.LogCategoryType.System,
                    e.EntryPoint,
                    e.Message,
                    e.Parameters,
                    e.Exception,
                    e.TrackingId
                );
            MicrosoftOnline.Ads.LogAssistant.LogHelper.Log(_e);
        }

        void Process(object sender, BulkProcessEventArgs e)
        {
            //列名
            //"AccountId"	"AccountName"	"AccountNumber"	"Impressions"	"Clicks"	"Spend"	"Conversions"	"GregorianDate"

            //想要获取某一行Spend的值，使用如下方式即可，注意大小写敏感
            Console.WriteLine("{0}\t{1}\t{2}", e.RowValues["AccountId"], e.RowValues["GregorianDate"], e.RowValues["Spend"]);
        }
    }
}
