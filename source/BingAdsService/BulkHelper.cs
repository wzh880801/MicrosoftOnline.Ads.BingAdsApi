using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using MicrosoftOnline.Ads.BingAdsApi.V10.BulkService;
using System.Threading.Tasks;

namespace MicrosoftOnline.Ads.BingAdsApi
{
    public class BulkHelper : LogBase, IDisposable, IProgressChanged
    {
        private BulkServiceClient cs = null;
        private EventHandler<ProgressChangeEventArgs> _progressChangedHandler = null;

        public BulkHelper(EventHandler<LogEventArgs> handler = null, EventHandler<ProgressChangeEventArgs> progressChangedHandler = null)
            : base(handler)
        {
            cs = new BulkServiceClient("BasicHttpBinding_IBulkService");
            this._progressChangedHandler = progressChangedHandler;
        }

        public void ReportProgress(double percent, string fileName = null)
        {
            if (this._progressChangedHandler != null)
                this._progressChangedHandler(this, new ProgressChangeEventArgs(percent, fileName));
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

        private BulkServiceClient Check()
        {
            if (cs == null)
                cs = new BulkServiceClient("BasicHttpBinding_IBulkService");
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

        public GetBulkDownloadStatusResponse GetBulkDownloadStatus(ApiAuthentication auth, string requestId, long? accountId, long? customerId)
        {
            var request = new GetBulkDownloadStatusRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                RequestId = requestId,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetBulkDownloadStatus(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.Bulk, "GetBulkDownloadStatus", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetBulkDownloadStatusResponse TryGetBulkDownloadStatus(ApiAuthentication auth, string requestId, long? accountId, long? customerId)
        {
            return MethodHelper.TryGet(GetBulkDownloadStatus, this, auth, requestId, accountId, customerId);
        }

        public async Task<GetBulkDownloadStatusResponse> GetBulkDownloadStatusAsync(ApiAuthentication auth, string requestId, long? accountId, long? customerId)
        {
            var request = new GetBulkDownloadStatusRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                RequestId = requestId,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetBulkDownloadStatusAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.Bulk, "GetBulkDownloadStatusAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetBulkUploadStatusResponse GetBulkUploadStatus(ApiAuthentication auth, string requestId, long? accountId, long? customerId)
        {
            var request = new GetBulkUploadStatusRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                RequestId = requestId,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetBulkUploadStatus(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.Bulk, "GetBulkUploadStatus", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetBulkUploadStatusResponse TryGetBulkUploadStatus(ApiAuthentication auth, string requestId, long? accountId, long? customerId)
        {
            return MethodHelper.TryGet(GetBulkUploadStatus, this, auth, requestId, accountId, customerId);
        }

        public async Task<GetBulkUploadStatusResponse> GetBulkUploadStatusAsync(ApiAuthentication auth, string requestId, long? accountId, long? customerId)
        {
            var request = new GetBulkUploadStatusRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                RequestId = requestId,
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetBulkUploadStatusAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.Bulk, "GetBulkUploadStatusAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetBulkUploadUrlResponse GetBulkUploadUrl(ApiAuthentication auth, long accountId, ResponseMode responseMode, long? customerId)
        {
            var request = new GetBulkUploadUrlRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                AccountId = accountId,
                ResponseMode = ResponseMode.ErrorsAndResults
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().GetBulkUploadUrl(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.Bulk, "GetBulkUploadUrl", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public GetBulkUploadUrlResponse TryGetBulkUploadUrl(ApiAuthentication auth, long accountId, ResponseMode responseMode, long? customerId)
        {
            return MethodHelper.TryGet(GetBulkUploadUrl, this, auth, accountId, responseMode, customerId);
        }

        public async Task<GetBulkUploadUrlResponse> GetBulkUploadUrlAsync(ApiAuthentication auth, long accountId, ResponseMode responseMode, long? customerId)
        {
            var request = new GetBulkUploadUrlRequest
            {
                CustomerAccountId = string.Format("{0}", accountId),
                CustomerId = string.Format("{0}", customerId),
                AccountId = accountId,
                ResponseMode = ResponseMode.ErrorsAndResults
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().GetBulkUploadUrlAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.Bulk, "GetBulkUploadUrlAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// Downloads an account’s campaign data. You can request all campaign data or only the data that has changed since the last time you downloaded the account.
        /// https://msdn.microsoft.com/en-us/library/bing-ads-bulk-downloadcampaignsbyaccountids.aspx
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="accountId"></param>
        /// <param name="customerId"></param>
        /// <param name="dataScope">You may include performance data such as spend, in addition to entity data such as campaign settings. 
        ///     The default is EntityData which will exclude performance data from the download.
        ///     You may include multiple values as flags. How you specify multiple flags depends on the programming language that you use. 
        ///         For example, C# treats these values as flag values and Java treats them as an array of strings. The SOAP should include a string that contains a space-delimited list of values for example, 
        ///     Note: If BidSuggestionsData, EntityPerformanceData, or QualityScoreData are included, you must request a full sync to get performance data. To perform a full sync, set LastSyncTimeInUTC to NULL.
        ///     Note: If EntityPerformanceData is included, you must specify the CustomerId in the service request header.</param>
        /// <param name="entities">The entities to include in the download. For a list of entities that you can download, see the BulkDownloadEntity value set.
        /// You may include multiple values as flags. How you specify multiple flags depends on the programming language that you use. For example, C# treats these values as flag values and Java treats them as an array of strings. The SOAP should include a string that contains a space-delimited list of values for example, &lt;Entities&gt;Campaigns Keywords&lt;/Entities&gt;.
        /// Note: You must specify at least one entity, and otherwise the operation will error.</param>
        /// <param name="compressionType"></param>
        /// <param name="downloadFileType"></param>
        /// <param name="formatVersion">The format for records of the download file. Currently the only supported format version is 4.0.
        /// As a best practice you should always specify the latest format version, which is currently 4.0.</param>
        /// <param name="lastSyncTimeInUTC">The last time that you requested a download. The date and time is expressed in Coordinated Universal Time (UTC).
        ///     Typically, you request a full download the first time you call the operation by setting this element to null. On all subsequent calls you set the last sync time to the time stamp of the previous download.
        ///     The download file contains the time stamp of the download in the SyncTime column of the Account record. Use the time stamp to set LastSyncTimeInUTC the next time that you request a download.
        ///     If you specify the last sync time, only those entities that have changed (been updated or deleted) since the specified date and time will be downloaded. However, if the campaign data has not been previously downloaded, the operation performs a full download.
        ///     After requesting a full download, the only time that you should again request a full download would be if your account was included in a data migration (for example, the URL by match type migration). If you specify a last sync time that predates the end of the migration process, the download will fail with CampaignServiceFullSyncRequired (error code 3603).</param>
        /// <param name="start">Defines the start and end date when downloading performance data.
        ///     If the DataScope element includes EntityPerformanceData, then the start and end date must be set with this element.</param>
        /// <param name="end">Defines the start and end date when downloading performance data.
        ///     If the DataScope element includes EntityPerformanceData, then the start and end date must be set with this element.</param>
        /// <returns></returns>
        public DownloadCampaignsByAccountIdsResponse DownloadCampaignsByAccountIds(
            ApiAuthentication auth,
            long accountId,
            long? customerId,
            DataScope dataScope,
            BulkDownloadEntity entities,
            CompressionType compressionType = CompressionType.Zip,
            DownloadFileType downloadFileType = DownloadFileType.Tsv,
            string formatVersion = "4.0",
            DateTime? lastSyncTimeInUTC = null,
            DateTime? start = null,
            DateTime? end = null)
        {
            var request = new DownloadCampaignsByAccountIdsRequest
            {
                CustomerId = string.Format("{0}", customerId),
                CustomerAccountId = string.Format("{0}", accountId),
                AccountIds = new long[] { accountId },
                CompressionType = compressionType,
                DataScope = dataScope,
                DownloadFileType = downloadFileType,
                Entities = entities,
                FormatVersion = formatVersion,
                LastSyncTimeInUTC = lastSyncTimeInUTC,
                PerformanceStatsDateRange = BuildDateRange(start, end)
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().DownloadCampaignsByAccountIds(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.Bulk, "DownloadCampaignsByAccountIds", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public DownloadCampaignsByAccountIdsResponse TryDownloadCampaignsByAccountIds(
            ApiAuthentication auth,
            long accountId,
            long? customerId,
            DataScope dataScope,
            BulkDownloadEntity entities,
            CompressionType compressionType = CompressionType.Zip,
            DownloadFileType downloadFileType = DownloadFileType.Tsv,
            string formatVersion = "4.0",
            DateTime? lastSyncTimeInUTC = null,
            DateTime? start = null,
            DateTime? end = null)
        {
            return MethodHelper.TryGet(DownloadCampaignsByAccountIds, this,
                auth,
                accountId,
                customerId,
                dataScope,
                entities,
                compressionType,
                downloadFileType,
                formatVersion,
                lastSyncTimeInUTC,
                start,
                end);
        }

        public async Task<DownloadCampaignsByAccountIdsResponse> DownloadCampaignsByAccountIdsAsync(
            ApiAuthentication auth,
            long accountId,
            long? customerId,
            DataScope dataScope,
            BulkDownloadEntity entities,
            CompressionType compressionType = CompressionType.Zip,
            DownloadFileType downloadFileType = DownloadFileType.Tsv,
            string formatVersion = "4.0",
            DateTime? lastSyncTimeInUTC = null,
            DateTime? start = null,
            DateTime? end = null)
        {
            var request = new DownloadCampaignsByAccountIdsRequest
            {
                CustomerId = string.Format("{0}", customerId),
                CustomerAccountId = string.Format("{0}", accountId),
                AccountIds = new long[] { accountId },
                CompressionType = compressionType,
                DataScope = dataScope,
                DownloadFileType = downloadFileType,
                Entities = entities,
                FormatVersion = formatVersion,
                LastSyncTimeInUTC = lastSyncTimeInUTC,
                PerformanceStatsDateRange = BuildDateRange(start, end)
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().DownloadCampaignsByAccountIdsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.Bulk, "DownloadCampaignsByAccountIdsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        /// <summary>
        /// Downloads the specified campaigns’ data. You can request all campaign data or only the data that has changed since the last time you downloaded the campaign.
        /// https://msdn.microsoft.com/en-us/library/bing-ads-bulk-downloadcampaignsbycampaignids.aspx
        /// </summary>
        /// <param name="auth"></param>
        /// <param name="campaigns">The campaigns to download. You can specify a maximum of 1,000 campaigns. 
        ///     The campaigns that you specify must belong to the same account.</param>
        /// <param name="accountId"></param>
        /// <param name="customerId"></param>
        /// <param name="dataScope"></param>
        /// <param name="entities"></param>
        /// <param name="compressionType"></param>
        /// <param name="downloadFileType"></param>
        /// <param name="formatVersion"></param>
        /// <param name="lastSyncTimeInUTC"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DownloadCampaignsByCampaignIdsResponse DownloadCampaignsByCampaignIds(
            ApiAuthentication auth,
            CampaignScope[] campaigns,
            long accountId,
            long? customerId,
            DataScope dataScope,
            BulkDownloadEntity entities,
            CompressionType compressionType = CompressionType.Zip,
            DownloadFileType downloadFileType = DownloadFileType.Tsv,
            string formatVersion = "4.0",
            DateTime? lastSyncTimeInUTC = null,
            DateTime? start = null,
            DateTime? end = null)
        {
            var request = new DownloadCampaignsByCampaignIdsRequest
            {
                CustomerId = string.Format("{0}", customerId),
                CustomerAccountId = string.Format("{0}", accountId),
                Campaigns = campaigns,
                CompressionType = compressionType,
                DataScope = dataScope,
                DownloadFileType = downloadFileType,
                Entities = entities,
                FormatVersion = formatVersion,
                LastSyncTimeInUTC = lastSyncTimeInUTC,
                PerformanceStatsDateRange = BuildDateRange(start, end)
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return Check().DownloadCampaignsByCampaignIds(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.Bulk, "DownloadCampaignsByCampaignIds", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        public DownloadCampaignsByCampaignIdsResponse TryDownloadCampaignsByCampaignIds(
            ApiAuthentication auth,
            CampaignScope[] campaigns,
            long accountId,
            long? customerId,
            DataScope dataScope,
            BulkDownloadEntity entities,
            CompressionType compressionType = CompressionType.Zip,
            DownloadFileType downloadFileType = DownloadFileType.Tsv,
            string formatVersion = "4.0",
            DateTime? lastSyncTimeInUTC = null,
            DateTime? start = null,
            DateTime? end = null)
        {
            return MethodHelper.TryGet(DownloadCampaignsByCampaignIds, this,
                auth,
                campaigns,
                accountId,
                customerId,
                dataScope,
                entities,
                compressionType,
                downloadFileType,
                formatVersion,
                lastSyncTimeInUTC,
                start,
                end);
        }

        public async Task<DownloadCampaignsByCampaignIdsResponse> DownloadCampaignsByCampaignIdsAsync(
            ApiAuthentication auth,
            CampaignScope[] campaigns,
            long accountId,
            long? customerId,
            DataScope dataScope,
            BulkDownloadEntity entities,
            CompressionType compressionType = CompressionType.Zip,
            DownloadFileType downloadFileType = DownloadFileType.Tsv,
            string formatVersion = "4.0",
            DateTime? lastSyncTimeInUTC = null,
            DateTime? start = null,
            DateTime? end = null)
        {
            var request = new DownloadCampaignsByCampaignIdsRequest
            {
                CustomerId = string.Format("{0}", customerId),
                CustomerAccountId = string.Format("{0}", accountId),
                Campaigns = campaigns,
                CompressionType = compressionType,
                DataScope = dataScope,
                DownloadFileType = downloadFileType,
                Entities = entities,
                FormatVersion = formatVersion,
                LastSyncTimeInUTC = lastSyncTimeInUTC,
                PerformanceStatsDateRange = BuildDateRange(start, end)
            };

            try
            {
                SetAuthHelper.SetAuth(auth, request);

                return await Check().DownloadCampaignsByCampaignIdsAsync(request);
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.Bulk, "DownloadCampaignsByCampaignIdsAsync", ex.Message, new { Request = request }, ex));
            }

            return null;
        }

        private PerformanceStatsDateRange BuildDateRange(DateTime? start, DateTime? end)
        {
            if (start.HasValue && end.HasValue)
            {
                return new PerformanceStatsDateRange
                {
                    CustomDateRangeStart = new Date
                    {
                        Year = start.Value.Year,
                        Month = start.Value.Month,
                        Day = start.Value.Day
                    },
                    CustomDateRangeEnd = new Date
                    {
                        Year = end.Value.Year,
                        Month = end.Value.Month,
                        Day = end.Value.Day
                    }
                };
            }

            return null;
        }

        public bool BulkDownloadCampaignsByAccountIds(
            ApiAuthentication auth,
            long accountId,
            long? customerId,
            DataScope dataScope,
            BulkDownloadEntity entities,
            string zipFilePath,
            CompressionType compressionType = CompressionType.Zip,
            DownloadFileType downloadFileType = DownloadFileType.Tsv,
            string formatVersion = "4.0",
            DateTime? lastSyncTimeInUTC = null,
            DateTime? start = null,
            DateTime? end = null)
        {
            var request = DownloadCampaignsByAccountIds(auth, accountId, customerId, dataScope, entities, compressionType, downloadFileType, formatVersion, lastSyncTimeInUTC, start, end);
            if (request == null)
                return false;

            int errors = 0;
            int percent = 0;
            string downloadUrl = null;

            while (true)
            {
                if (errors >= 30)
                    return false;

                var _req = GetBulkDownloadStatus(auth, request.DownloadRequestId, accountId, customerId);
                if (_req == null)
                    return false;

                if (percent != _req.PercentComplete)
                {
                    percent = _req.PercentComplete;
                    ReportProgress(percent);
                }

                if (_req.RequestStatus == "Completed")//InProgress //Failed //FailedFullSyncRequired 
                {
                    downloadUrl = _req.ResultFileUrl;
                    break;
                }
                else if (_req.RequestStatus == "Failed" || _req.RequestStatus == "FailedFullSyncRequired")
                {
                    Log(new LogEventArgs(
                        ServiceType.Bulk, 
                        "BulkDownloadCampaignsByAccountIds", 
                        "GetBulkDownloadStatus Failed!",
                        new { Response = _req }, 
                        null, 
                        this.TrackingId));
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(downloadUrl))
            {
                Log(new LogEventArgs(
                        ServiceType.Bulk,
                        "BulkDownloadCampaignsByAccountIds",
                        "Beging to download...",
                        new { Url = downloadUrl, accountId = accountId },
                        null,
                        this.TrackingId, LogLevel.Info));

                using (FileDownloadHelper fd = new FileDownloadHelper())
                {
                    fd.DownloadFileFromUrl(downloadUrl, zipFilePath);
                }

                return true;
            }

            return false;
        }

        public async Task<bool> BulkDownloadCampaignsByAccountIdsAsync(
            ApiAuthentication auth,
            long accountId,
            long? customerId,
            DataScope dataScope,
            BulkDownloadEntity entities,
            string zipFilePath,
            CompressionType compressionType = CompressionType.Zip,
            DownloadFileType downloadFileType = DownloadFileType.Tsv,
            string formatVersion = "4.0",
            DateTime? lastSyncTimeInUTC = null,
            DateTime? start = null,
            DateTime? end = null)
        {
            var request = await DownloadCampaignsByAccountIdsAsync(auth, accountId, customerId, dataScope, entities, compressionType, downloadFileType, formatVersion, lastSyncTimeInUTC, start, end);
            if (request == null)
                return false;

            int errors = 0;
            int percent = 0;
            string downloadUrl = null;

            while (true)
            {
                if (errors >= 30)
                    return false;

                var _req = await GetBulkDownloadStatusAsync(auth, request.DownloadRequestId, accountId, customerId);
                if (_req == null)
                    return false;

                if (percent != _req.PercentComplete)
                {
                    percent = _req.PercentComplete;
                    ReportProgress(percent);
                }

                if (_req.RequestStatus == "Completed")//InProgress //Failed //FailedFullSyncRequired 
                {
                    downloadUrl = _req.ResultFileUrl;
                    break;
                }
                else if (_req.RequestStatus == "Failed" || _req.RequestStatus == "FailedFullSyncRequired")
                {
                    Log(new LogEventArgs(
                        ServiceType.Bulk,
                        "BulkDownloadCampaignsByAccountIds",
                        "GetBulkDownloadStatus Failed!",
                        new { Response = _req },
                        null,
                        this.TrackingId));
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(downloadUrl))
            {
                Log(new LogEventArgs(
                        ServiceType.Bulk,
                        "BulkDownloadCampaignsByAccountIds",
                        "Beging to download...",
                        new { Url = downloadUrl, accountId = accountId },
                        null,
                        this.TrackingId, LogLevel.Info));

                using (FileDownloadHelper fd = new FileDownloadHelper())
                {
                    await fd.DownloadFileFromUrlAsync(downloadUrl, zipFilePath);
                }

                return true;
            }

            return false;
        }

        public bool TryBulkDownloadCampaignsByAccountIds(
           ApiAuthentication auth,
           long accountId,
           long? customerId,
           DataScope dataScope,
           BulkDownloadEntity entities,
           string zipFilePath,
           CompressionType compressionType = CompressionType.Zip,
           DownloadFileType downloadFileType = DownloadFileType.Tsv,
           string formatVersion = "4.0",
           DateTime? lastSyncTimeInUTC = null,
           DateTime? start = null,
           DateTime? end = null)
        {
            return MethodHelper.TryGetBool(
                BulkDownloadCampaignsByAccountIds, this, 
                auth, accountId, customerId, dataScope, entities, zipFilePath, compressionType, downloadFileType, formatVersion, lastSyncTimeInUTC, start, end);
        }
    }
}
