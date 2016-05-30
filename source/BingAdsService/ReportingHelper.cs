using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using MicrosoftOnline.Ads.BingAdsApi.V9.ReportingService;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace MicrosoftOnline.Ads.BingAdsApi
{
    /// <summary>
    /// BingAds API V9 Reporting Service
    /// https://msdn.microsoft.com/en-us/library/bing-ads-reporting-service-reference(v=msads.90).aspx
    /// </summary>
    public class ReportingHelper : LogBase, IDisposable
    {
        private EventHandler<ProgressChangeEventArgs> _progressChangedHandler = null;

        public ReportingHelper(EventHandler<LogEventArgs> handler = null, EventHandler<ProgressChangeEventArgs> progressChangedHandler = null)
            : base(handler)
        {
            cs = new ReportingServiceClient("BasicHttpBinding_IReportingService");
            this._progressChangedHandler = progressChangedHandler;
        }

        private ReportingServiceClient cs = null;

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

        private ReportingServiceClient Check()
        {
            if (cs == null)
                cs = new ReportingServiceClient("BasicHttpBinding_IReportingService");
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

        protected void ReportProgress(double percent, string fileName = null)
        {
            if (this._progressChangedHandler != null)
                this._progressChangedHandler(this, new ProgressChangeEventArgs(percent, fileName));
        }

        /// <summary>
        /// Submit the reportRequest and download it to local
        /// https://msdn.microsoft.com/en-us/library/bing-ads-reporting-submitgeneratereport(v=msads.90).aspx
        /// You must use the same user credentials for the SubmitGenerateReport and PollGenerateReport.
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="reportRequest">do not using ReportRequest, using classes derives from it
        /// The report request. The request must be an object that derives from ReportRequest. For a list of report request types, see Report Types.
        /// http://go.microsoft.com/fwlink/?LinkId=691016</param>
        /// <param name="customerId">The identifier of the customer that owns the account.
        /// Note: As a best practice you should always specify this element.</param>
        /// <param name="accountId">The identifier of the account that owns the entities in the request. This header element must have the same value as the AccountId body element when both are required.
        /// Note: As a best practice you should always specify this element for operations limited in scope to a single account per service call.</param>
        /// <param name="zipFilePath">the full local file path that you want to save the report</param>
        /// <returns></returns>
        public bool SubmitGenerateReport(ApiAuthentication auth, ReportRequest reportRequest, long? customerId, long? accountId, string zipFilePath)
        {
            var response = SubmitReport(auth, reportRequest, customerId, accountId);
            if (response == null)
            {
                ReportProgress(1);
                return false;
            }

            Log(new LogEventArgs(
                ServiceType.Reporting,
                "SubmitGenerateReport",
                "ReportRequest was successfully submitted",
                new
                {
                    ReportRequestId = response.ReportRequestId
                }));

            ReportProgress(0.1);

            double _percent = 0.1;

            double maxWaitMinutes = 300;
            DateTime startTime = DateTime.Now;
            double elapsedMinutes = 0;

            var pullResponse = PollGenerateReport(auth, response.ReportRequestId, customerId, accountId);
            do
            {
                elapsedMinutes = DateTime.Now.Subtract(startTime).TotalMinutes;

                if (pullResponse == null)
                {
                    ReportProgress(1);
                    return false;
                }

                if (pullResponse.ReportRequestStatus.Status == ReportRequestStatusType.Success)
                {
                    Log(new LogEventArgs(
                        ServiceType.Reporting,
                        "SubmitGenerateReport",
                        string.Format("Report {0} was successfully generated! Begin to download...", response.ReportRequestId),
                        new
                        {
                            ReportRequestId = response.ReportRequestId,
                            CustomerId = customerId,
                            AccountId = accountId,
                            ReportDownloadUrl = pullResponse.ReportRequestStatus.ReportDownloadUrl
                        }));

                    ReportProgress(0.5);

                    try
                    {
                        DownloadFileFromUrl(pullResponse.ReportRequestStatus.ReportDownloadUrl, zipFilePath);

                        break;
                    }
                    catch (Exception ex)
                    {
                        Log(new LogEventArgs(
                                ServiceType.Reporting,
                                "SubmitGenerateReport.DownloadFileFromUrl",
                                string.Format("Error occured while downloading report {0}...", response.ReportRequestId),
                                new
                                {
                                    ReportRequestId = response.ReportRequestId,
                                    CustomerId = customerId,
                                    AccountId = accountId,
                                    ReportDownloadUrl = pullResponse.ReportRequestStatus.ReportDownloadUrl
                                }, ex));

                        ReportProgress(1);

                        return false;
                    }
                }
                else if (pullResponse.ReportRequestStatus.Status == ReportRequestStatusType.Pending)
                {
                    System.Threading.Thread.Sleep(2000);
                    pullResponse = PollGenerateReport(auth, response.ReportRequestId, customerId, accountId);
                    _percent += 0.01;

                    ReportProgress(_percent >= 0.5 ? 0.5 : _percent);

                    continue;
                }
                else
                {
                    Log(new LogEventArgs(
                        ServiceType.Reporting,
                        "ReportRequestStatus",
                        "ReportRequestStatus.Status = ReportRequestStatusType.Error",
                        new
                        {
                            ReportRequestId = response.ReportRequestId,
                            CustomerId = customerId,
                            AccountId = accountId
                        }));

                    ReportProgress(1);

                    return false;//Error Occured
                }
            } while (elapsedMinutes < maxWaitMinutes);

            Log(new LogEventArgs(
                ServiceType.Reporting,
                "SubmitGenerateReport",
                string.Format("Report {0} was successfully downloaded at {1}", response.ReportRequestId, zipFilePath),
                new
                {
                    ReportRequestId = response.ReportRequestId,
                    CustomerId = customerId,
                    AccountId = accountId,
                }));

            return true;
        }

        public bool TrySubmitGenerateReport(ApiAuthentication auth, ReportRequest reportRequest, long? customerId, long? accountId, string zipFilePath)
        {
            var response = TrySubmitReport(auth, reportRequest, customerId, accountId);
            if (response == null)
            {
                ReportProgress(1);
                return false;
            }

            Log(new LogEventArgs(
                ServiceType.Reporting,
                "TrySubmitGenerateReport",
                "ReportRequest was successfully submitted",
                new
                {
                    ReportRequestId = response.ReportRequestId
                }));

            ReportProgress(0.1);

            double _percent = 0.1;

            double maxWaitMinutes = 300;
            DateTime startTime = DateTime.Now;
            double elapsedMinutes = 0;

            var pullResponse = TryPollGenerateReport(auth, response.ReportRequestId, customerId, accountId);
            do
            {
                elapsedMinutes = DateTime.Now.Subtract(startTime).TotalMinutes;

                if (pullResponse == null)
                {
                    ReportProgress(1);
                    return false;
                }

                if (pullResponse.ReportRequestStatus.Status == ReportRequestStatusType.Success)
                {
                    Log(new LogEventArgs(
                        ServiceType.Reporting,
                        "TrySubmitGenerateReport",
                        string.Format("Report {0} was successfully generated! Begin to download...", response.ReportRequestId),
                        new
                        {
                            ReportRequestId = response.ReportRequestId,
                            CustomerId = customerId,
                            AccountId = accountId,
                            ReportDownloadUrl = pullResponse.ReportRequestStatus.ReportDownloadUrl
                        }));

                    ReportProgress(0.5);

                    try
                    {
                        TryDownloadFileFromUrl(pullResponse.ReportRequestStatus.ReportDownloadUrl, zipFilePath);

                        break;
                    }
                    catch (Exception ex)
                    {
                        Log(new LogEventArgs(
                                ServiceType.Reporting,
                                "TrySubmitGenerateReport.TryDownloadFileFromUrl",
                                string.Format("Error occured while downloading report {0}...", response.ReportRequestId),
                                new
                                {
                                    ReportRequestId = response.ReportRequestId,
                                    CustomerId = customerId,
                                    AccountId = accountId,
                                    ReportDownloadUrl = pullResponse.ReportRequestStatus.ReportDownloadUrl
                                }, ex));

                        ReportProgress(1);

                        return false;
                    }
                }
                else if (pullResponse.ReportRequestStatus.Status == ReportRequestStatusType.Pending)
                {
                    System.Threading.Thread.Sleep(2000);
                    pullResponse = TryPollGenerateReport(auth, response.ReportRequestId, customerId, accountId);
                    _percent += 0.01;

                    ReportProgress(_percent >= 0.5 ? 0.5 : _percent);

                    continue;
                }
                else
                {
                    Log(new LogEventArgs(
                        ServiceType.Reporting,
                        "ReportRequestStatus",
                        "ReportRequestStatus.Status = ReportRequestStatusType.Error",
                        new
                        {
                            ReportRequestId = response.ReportRequestId,
                            CustomerId = customerId,
                            AccountId = accountId
                        }));

                    ReportProgress(1);

                    return false;//Error Occured
                }
            } while (elapsedMinutes < maxWaitMinutes);

            Log(new LogEventArgs(
                ServiceType.Reporting,
                "TrySubmitGenerateReport",
                string.Format("Report {0} was successfully downloaded at {1}", response.ReportRequestId, zipFilePath),
                new
                {
                    ReportRequestId = response.ReportRequestId,
                    CustomerId = customerId,
                    AccountId = accountId,
                }));

            return true;
        }

        public async Task<bool> SubmitGenerateReportAsync(ApiAuthentication auth, ReportRequest reportRequest, long? customerId, long? accountId, string zipFilePath)
        {
            var response = await SubmitReportAsync(auth, reportRequest, customerId, accountId);
            if (response == null)
            {
                ReportProgress(1);
                return false;
            }

            Log(new LogEventArgs(
                ServiceType.Reporting,
                "SubmitGenerateReportAsync",
                "ReportRequest was successfully submitted",
                new
                {
                    ReportRequestId = response.ReportRequestId
                }));

            ReportProgress(0.1);

            double _percent = 0.1;

            double maxWaitMinutes = 300;
            DateTime startTime = DateTime.Now;
            double elapsedMinutes = 0;

            var pullResponse = await PollGenerateReportAsync(auth, response.ReportRequestId, customerId, accountId);
            do
            {
                elapsedMinutes = DateTime.Now.Subtract(startTime).TotalMinutes;

                if (pullResponse == null)
                {
                    ReportProgress(1);
                    return false;
                }

                if (pullResponse.ReportRequestStatus.Status == ReportRequestStatusType.Success)
                {
                    Log(new LogEventArgs(
                        ServiceType.Reporting,
                        "SubmitGenerateReportAsync",
                        string.Format("Report {0} was successfully generated! Begin to download...", response.ReportRequestId),
                        new
                        {
                            ReportRequestId = response.ReportRequestId,
                            CustomerId = customerId,
                            AccountId = accountId,
                            ReportDownloadUrl = pullResponse.ReportRequestStatus.ReportDownloadUrl
                        }));

                    ReportProgress(0.5);

                    try
                    {
                        await DownloadFileFromUrlAsync(pullResponse.ReportRequestStatus.ReportDownloadUrl, zipFilePath);

                        break;
                    }
                    catch (Exception ex)
                    {
                        Log(new LogEventArgs(
                                ServiceType.Reporting,
                                "SubmitGenerateReportAsync.DownloadFileFromUrlAsync",
                                string.Format("Error occured while downloading report {0}...", response.ReportRequestId),
                                new
                                {
                                    ReportRequestId = response.ReportRequestId,
                                    CustomerId = customerId,
                                    AccountId = accountId,
                                    ReportDownloadUrl = pullResponse.ReportRequestStatus.ReportDownloadUrl
                                }, ex));

                        ReportProgress(1);

                        return false;
                    }
                }
                else if (pullResponse.ReportRequestStatus.Status == ReportRequestStatusType.Pending)
                {
                    System.Threading.Thread.Sleep(2000);
                    pullResponse = await PollGenerateReportAsync(auth, response.ReportRequestId, customerId, accountId);
                    _percent += 0.01;

                    ReportProgress(_percent >= 0.5 ? 0.5 : _percent);

                    continue;
                }
                else
                {
                    Log(new LogEventArgs(
                        ServiceType.Reporting,
                        "ReportRequestStatus",
                        "ReportRequestStatus.Status = ReportRequestStatusType.Error",
                        new
                        {
                            ReportRequestId = response.ReportRequestId,
                            CustomerId = customerId,
                            AccountId = accountId
                        }));

                    ReportProgress(1);

                    return false;//Error Occured
                }
            } while (elapsedMinutes < maxWaitMinutes);

            Log(new LogEventArgs(
                ServiceType.Reporting,
                "SubmitGenerateReportAsync",
                string.Format("Report {0} was successfully downloaded at {1}", response.ReportRequestId, zipFilePath),
                new
                {
                    ReportRequestId = response.ReportRequestId,
                    CustomerId = customerId,
                    AccountId = accountId,
                }));

            return true;
        }

        private SubmitGenerateReportResponse SubmitReport(ApiAuthentication auth, ReportRequest reportRequest, long? customerId, long? accountId)
        {
            var request = new SubmitGenerateReportRequest
            {
                ReportRequest = reportRequest,
                CustomerId = customerId.HasValue ? string.Format("{0}", customerId.Value) : "",
                CustomerAccountId = accountId.HasValue ? string.Format("{0}", accountId.Value) : "",
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().SubmitGenerateReport(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.Reporting, "SubmitReport", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        private SubmitGenerateReportResponse TrySubmitReport(ApiAuthentication auth, ReportRequest reportRequest, long? customerId, long? accountId)
        {
            return MethodHelper.TryGet(SubmitReport, this, auth, reportRequest, customerId, accountId);
        }

        private async Task<SubmitGenerateReportResponse> SubmitReportAsync(ApiAuthentication auth, ReportRequest reportRequest, long? customerId, long? accountId)
        {
            var request = new SubmitGenerateReportRequest
            {
                ReportRequest = reportRequest,
                CustomerId = customerId.HasValue ? string.Format("{0}", customerId.Value) : "",
                CustomerAccountId = accountId.HasValue ? string.Format("{0}", accountId.Value) : "",
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().SubmitGenerateReportAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.Reporting, "SubmitReportAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// Gets the status of a report request.
        /// https://msdn.microsoft.com/en-us/library/bing-ads-reporting-pollgeneratereport(v=msads.90).aspx
        /// You must use the same user credentials for the SubmitGenerateReport and PollGenerateReport. For more information, see Request and Download a Report.
        /// </summary>
        /// <param name="auth">Do not use ApiAuthentication directly. Use PasswordAuthentication or OAuthAuthentication derives from it instead.</param>
        /// <param name="reportRequestId">The identifier of the report request. The SubmitGenerateReport operation returns the identifier.</param>
        /// <param name="customerId">The identifier of the customer that owns the account.
        /// Note: As a best practice you should always specify this element.</param>
        /// <param name="accountId">The identifier of the account that owns the entities in the request. This header element must have the same value as the AccountId body element when both are required.
        /// Note: As a best practice you should always specify this element for operations limited in scope to a single account per service call.</param>
        /// <returns></returns>
        public PollGenerateReportResponse PollGenerateReport(ApiAuthentication auth, string reportRequestId, long? customerId, long? accountId)
        {
            var request = new PollGenerateReportRequest
            {
                ReportRequestId = reportRequestId,
                CustomerId = customerId.HasValue ? string.Format("{0}", customerId.Value) : "",
                CustomerAccountId = accountId.HasValue ? string.Format("{0}", accountId.Value) : "",
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().PollGenerateReport(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.Reporting, "PollGenerateReport", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public PollGenerateReportResponse TryPollGenerateReport(ApiAuthentication auth, string reportRequestId, long? customerId, long? accountId)
        {
            return MethodHelper.TryGet(PollGenerateReport, this, auth, reportRequestId, customerId, accountId);
        }

        public async Task<PollGenerateReportResponse> PollGenerateReportAsync(ApiAuthentication auth, string reportRequestId, long? customerId, long? accountId)
        {
            var request = new PollGenerateReportRequest
            {
                ReportRequestId = reportRequestId,
                CustomerId = customerId.HasValue ? string.Format("{0}", customerId.Value) : "",
                CustomerAccountId = accountId.HasValue ? string.Format("{0}", accountId.Value) : "",
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().PollGenerateReportAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.Reporting, "PollGenerateReportAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public ReportTime GetCustomReportTime(DateTime startDate, DateTime endDate)
        {
            return new ReportTime()
            {
                CustomDateRangeStart = new Date()
                {
                    Year = startDate.Year,
                    Month = startDate.Month,
                    Day = startDate.Day
                },
                CustomDateRangeEnd = new Date()
                {
                    Year = endDate.Year,
                    Month = endDate.Month,
                    Day = endDate.Day
                },
            };
        }

        public void DownloadFileFromUrl(string downloadUrl, string zipFileName)
        {
            // Open a connection to the URL where the report is available.
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(downloadUrl);
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            Stream httpStream = response.GetResponseStream();

            // Open the report file.
            FileInfo zipFileInfo = new FileInfo(zipFileName);
            if (!zipFileInfo.Directory.Exists)
            {
                zipFileInfo.Directory.Create();
            }

            FileStream fileStream = new FileStream(zipFileInfo.FullName, FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            BinaryReader binaryReader = new BinaryReader(httpStream);

            // Read the report and save it to the file.
            int bufferSize = 10240;
            long writtenBytes = 0L;
            while (true)
            {
                // Read report data from API.
                byte[] buffer = binaryReader.ReadBytes(bufferSize);

                // Write report data to file.
                binaryWriter.Write(buffer);

                writtenBytes += buffer.Length;

                if (this._progressChangedHandler != null)
                {
                    var _percent = response.ContentLength == 0 ? 0.5 : writtenBytes * 0.5 / response.ContentLength;
                    this._progressChangedHandler(this, new ProgressChangeEventArgs(0.5 + _percent, zipFileName));
                }

                // If the end of the report is reached, break out of the 
                // loop.
                if (buffer.Length != bufferSize)
                {
                    break;
                }
            }

            // Clean up.
            binaryWriter.Close();
            binaryReader.Close();
            fileStream.Close();
            httpStream.Close();
        }

        public void TryDownloadFileFromUrl(string downloadUrl, string zipFileName)
        {
            MethodHelper.TryGetVoid(DownloadFileFromUrl, this, downloadUrl, zipFileName);
        }

        public async Task DownloadFileFromUrlAsync(string downloadUrl, string zipFileName)
        {
            // Open a connection to the URL where the report is available.
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(downloadUrl);
            HttpWebResponse response = (HttpWebResponse)await webRequest.GetResponseAsync();
            Stream httpStream = response.GetResponseStream();

            // Open the report file.
            FileInfo zipFileInfo = new FileInfo(zipFileName);
            if (!zipFileInfo.Directory.Exists)
            {
                zipFileInfo.Directory.Create();
            }

            FileStream fileStream = new FileStream(zipFileInfo.FullName, FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            BinaryReader binaryReader = new BinaryReader(httpStream);

            // Read the report and save it to the file.
            int bufferSize = 10240;
            long writtenBytes = 0L;
            while (true)
            {
                // Read report data from API.
                byte[] buffer = binaryReader.ReadBytes(bufferSize);

                // Write report data to file.
                binaryWriter.Write(buffer);

                writtenBytes += buffer.Length;

                if (this._progressChangedHandler != null)
                {
                    var _percent = response.ContentLength == 0 ? 0.5 : writtenBytes * 0.5 / response.ContentLength;
                    this._progressChangedHandler(this, new ProgressChangeEventArgs(0.5 + _percent, zipFileName));
                }

                // If the end of the report is reached, break out of the 
                // loop.
                if (buffer.Length != bufferSize)
                {
                    break;
                }
            }

            // Clean up.
            binaryWriter.Close();
            binaryReader.Close();
            fileStream.Close();
            httpStream.Close();
        }
    }
}