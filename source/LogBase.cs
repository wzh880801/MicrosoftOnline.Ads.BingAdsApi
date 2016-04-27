using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftOnline.Ads.BingAdsApi
{
    public abstract class LogBase
    {
        private EventHandler<LogEventArgs> LogHandler = null;

        public Exception LastException { get; private set; }

        private bool _ignoreError = false;
        public bool IgnoreError
        {
            get { return _ignoreError; }
            set { _ignoreError = value; }
        }

        protected void Log(LogEventArgs e)
        {
            e.TrackingId = this.TrackingId;
            if (e.Exception == null && e.LogLevel == LogLevel.Error)
                e.LogLevel = LogLevel.Info;

            this.LastException = e.Exception;

            if (this.LogHandler != null && (!this.IgnoreError || e.LogLevel != LogLevel.Error))
                this.LogHandler(this, e);
        }

        public LogBase(EventHandler<LogEventArgs> handler)
        {
            this.LogHandler = handler;
            this._trackingId = Guid.NewGuid().ToString();
        }

        private string _trackingId;

        public string TrackingId
        {
            get
            {
                return this._trackingId;
            }
        }

        //public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        //protected void RaisePropertyChanged(string propertyName)
        //{
        //    System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
        //    if ((propertyChanged != null))
        //    {
        //        propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        //    }
        //}

        //public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);
    }
}
