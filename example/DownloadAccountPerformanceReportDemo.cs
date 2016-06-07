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
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("------------------------Start------------------------");
            Console.WriteLine("Description:\tDownloadAccountPerformanceReportDemo");

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
            using (CustomerMHelper cs = new CustomerMHelper(
                //you can use this handler to handle the errors
                LogHandler))
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

            //you can submit a report request which could most contain 1000 accountIds per time
            //if you have more than 1000 accounts, then you should submit the report request every 1000 accountIds
            //for demo, we just pick up the first 1000 accountIds
            if (accounts.Count > 1000)
                accounts = accounts.TakeWhile((p, i) => { return i < 1000; }).ToList();

            using (ReportingHelper rs = new ReportingHelper(
                //you can use this handler to handle the errors
                LogHandler))
            {
                //Submit & download
                //will try for most 3 times when error occured
                var succeed =
                    rs.TrySubmitGenerateReport(
                        //API Auth info
                        Common.GetApiAuth(),
                        //Report Request
                        BuildRequest(accounts.Select(p => p.Id).ToArray()),
                        //Your customerId
                        CustomerId,
                        //Your accountId
                        null,
                        //local path in which you want to store the report data
                        SaveFilePath);
            }

            //if you need process the report file
            //use this fileprocessor
            //it can help you UnZip the file and process the rows & return the values of each row
            using (FileProcessor fp = new FileProcessor(
                //you can use this handler to handle the errors
                LogHandler,
                //you can use this handler to receive the values of each row
                Process,
                //implement this handler if you want to receive a notification when process progress changed
                null,
                //the full path of the zip file
                SaveFilePath,
                //do not delete tsv file when process was done
                false,
                //do not delete zip file when process was done
                false))
            {
                var b = fp.Process();
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
                //if you want to use the fileProcessor to process the report file
                //then you must set the Format to TSV
                Format = ReportFormat.Tsv,
                Language = ReportLanguage.English,
                ReportName = "AccountPerformanceReport",
                ReturnOnlyCompleteData = true,
                Scope = new AccountReportScope
                {
                    //if you only owns one customer in BingAds
                    //to set this to null if you want to download the data of all accounts, e.g. AccountIds = null
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

        List<string> rows = new List<string>();
        void Process(object sender, ProcessEventArgs e)
        {
            /*
             * e.RowValues contains the whole values of each row
             * e.g. the structure of the report file is 
             * 
             *  Cloumn1  Column2 Column3
             *  1        2       3
             *  10       30      40
             *  
             * then the e.RowValues will be
             * [1,2,3]
             * 
             * you could get each column value by e.RowValues[index]
             */

            //Show the row content every two rows
            var line = "";
            foreach(object o in e.RowValues)
            {
                line += string.Format("\t{0}", o);
            }

            rows.Add(line.TrimStart('\t'));

            //this is to improve the performance
            //if the report is big
            //if you insert the data to DB for each row, the performance will be poor
            //instead, you should insert every 10000 or more rows
            if(rows.Count == 2 || e.Completed)
            {
                foreach (var row in rows)
                    Console.WriteLine(row);

                rows.Clear();
            }

            /*
             * You could also insert the data to DB, write to local file...
             * 
             * Report Schema:
             * 
             *  AccountPerformanceReportColumn.AccountId,
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
             */

            //var accountId = long.Parse(e.RowValues[0].ToString());
        }
    }
}
