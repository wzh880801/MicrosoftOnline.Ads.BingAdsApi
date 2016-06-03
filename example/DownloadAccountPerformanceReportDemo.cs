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
        }

        void Work()
        {
            ApiAuthentication auth = null;

            //auth = new PasswordAuthentication("username", "passsword", "developerToken");

            auth = new OAuthAuthentication(
                //accessToken
                "EwB4AnhlBAAUxT83/QvqiAZEx5SuwyhZqHzk21oAATo1L+ZidOwC+A7XuwE5iM2aMgpH+jsm4QPw/z8qHHdp/uwX2PE6mM2vuKFo4jvzAg/O9iHDPSePHL7tCliKXo7ZAFN/N8fG1xcEaiiUpwkavJSjo5LBEqqarur0FvNFdF6Ie2ZJ7yG6ayMbwOlppdnjzgnebyDMDUCFUmNBOBUdhd7q3ucVVxknHMUPHjC9XmP4+ZP8rtpAZil/P6Oq45hnhzICqBLJ2V5kxQKNrfJvtCfYTCNyDJA79NmDwUfYqsnZWnJM+7OVwRVsxjWCV1xx59vzLlu6l6p9jWy2afPrm9v0jcwvJzxgVNwQPh4Oxn0MrRkKC2sQQzZytzJu8TIDZgAACCIoUitsnpRwSAGeVH5noWGzfbhML3sOwgBlq9SVIjGtr4mSGfFXN3SCGGYRQLSrNKoziuPsNBI0CioNQBcihZq3GchYmQ+znDBkjmgH9rn9+GJ1rCiy2PKt96GMGw7HF3WAtAldj7SzLEno9MKCyd9GHil432UKMwEkH9Al5cqfKJFgrMtlbRDt/jcL46lpDdUnq5A/2fYoce7X71WKybmez5vKTMOWy+Vw4ejhvgZ98Vbq/GFMtEGy71wM6q850egvcuFACZzVaTwztrJcWyVUpbEUGdrfNVwwCfdNo+Xy8gagyf+fVgC/e8qO4qJh2BERLzakEr7RWvzW+P/g3QMDXiibph8syohcPocHaYZnecbfmDzg3lDO6sXyS8JlA9QvanMIu+fo1s+ZBUb8K07q8T6O7fc7JW/BiYjnBTznWZmfnGdPXSIGduf8ISU6b4GFdwE=",
                //refreshToken
                "MCUdoFKVCaEWbNBEciYzkgNqrjr3rjZDdFtYxw0waplqvZ1Ujlx3v3oDOxyawzE6*HsFbx4JWs9wcgtwcW4!VWDfLEJ4TgmSc*tL5!SJvkCNALpCSWFbmbw*F1Bd18BtUwIBNx!KzDK*g0cNeNVtNV6VafOUBmMvF0zcE7c4iTtpD*DfSwwTvWQHAJCxsLsW2vKHEskennvS*jIOQEEEJ2qjfr*wC5sO8sRNyKQwM2A0H3hhnTkpTeo0eM3EC281uFCJ6rQEBQLpO9oRURuUl4RGCSYYih*XQgB!rQ6dRn3FptV06u!1Jpg4lYcbyMjy4RYAnKzJQ6LycMaLcQjr1pdA$",
                //developerToken
                "0417MW27U4928703",
                DateTime.Parse("2016-06-03 11:23:13.449").ToUniversalTime().Ticks,
                //clientId
                "000000004418E25F",
                null,
                //save the token when a new token was generated
                TokenRefreshEventHandler);

            IList<AccountInfo> accounts = null;
            using (CustomerMHelper cs = new CustomerMHelper(
                //you can use this handler to handle the errors
                LogHandler))
            {
                //will try for 3 times if error occured
                var response = cs.TryGetAccountsInfo(auth, CustomerId, true);

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
                        auth,
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

        void TokenRefreshEventHandler(object sender, TokenRefreshedEventArgs e)
        {
            //you can store new tokens
            //or you can replace the old one
            //for demo, we just print them
            Console.WriteLine("Access token refreshed:");
            Console.WriteLine("\tNew AccessToken:\t\t{0}", e.AuthenticationToken);
            Console.WriteLine("\tNew RefreshToken:\t\t{0}", e.RefreshToken);
            Console.WriteLine("\tExpireTime(BeijingTime):\t\t{0:yyyy-MM-dd HH:mm:ss.fff}", new DateTime(e.ExpiresTime.Value).AddHours(8));
        }
    }
}
