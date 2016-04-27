using System;
using System.Text;

namespace MicrosoftOnline.Ads.BingAdsApi
{
    public enum ServiceType
    {
        AdInsight,
        Bulk,
        CampaignManagement,
        CustomerBilling,
        CustomerManagement,
        Reporting,
        FileProcessor,
        Others
    }

    public enum LogLevel
    {
        Info,
        Warn,
        Error
    }

    public class LogEventArgs : EventArgs
    {
        public LogEventArgs(ServiceType serviceType,
                string entryPoint,
                string message = null,
                object parameters = null,
                Exception exception = null,
                string trakingId = null,
                LogLevel logLevel = BingAdsApi.LogLevel.Error)
        {
            this.LogLevel = logLevel;
            this.LogDateTime = DateTime.UtcNow.AddHours(8);
            this._serviceType = serviceType;
            this._entryPoint = entryPoint;
            this.Message = message;
            this.Parameters = parameters;
            this.Exception = exception;
            this.TrackingId = trakingId ?? Guid.NewGuid().ToString();
        }

        public LogLevel LogLevel { get; internal set; }
        public DateTime LogDateTime { get; private set; }
        private ServiceType _serviceType;
        private string _entryPoint;
        public string EntryPoint { get { return string.Format("{0} - {1}", this._serviceType, this._entryPoint); } }
        public string Message { get; private set; }
        public object Parameters { get; private set; }
        public Exception Exception { get; private set; }
        public string TrackingId { get; internal set; }
    }

    public class ProgressChangeEventArgs : EventArgs
    {
        public ProgressChangeEventArgs(double percent, string filename = null)
        {
            this.Percent = percent;
            this.FileName = filename;
        }

        public double Percent { get; private set; }

        public string FileName { get; private set; }
    }

    public class ProcessEventArgs : EventArgs
    {
        public ProcessEventArgs(object[] rowValues, bool completed)
        {
            this.RowValues = rowValues;
            this.Completed = completed;
        }

        public object[] RowValues { get; private set; }
        public bool Completed { get; private set; }
    }
}
