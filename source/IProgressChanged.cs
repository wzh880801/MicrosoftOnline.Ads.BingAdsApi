using System;

namespace MicrosoftOnline.Ads.BingAdsApi
{
    public interface IProgressChanged
    {
        void ReportProgress(double percent, string fileName = null);
    }
}
