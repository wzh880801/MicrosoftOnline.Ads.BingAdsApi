using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using MicrosoftOnline.Ads.BingAdsApi;

namespace MicrosoftOnline.Ads.UnitTestCase
{
    [TestClass]
    public class TsvReportFileProcessorUnitTest
    {
        private string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        private string testFile = null;

        [TestInitialize]
        public void Init()
        {
            testFile = Path.Combine(appPath, "30000000182688593.zip");
        }

        [TestMethod]
        public void TestParseReportFile()
        {
            Console.WriteLine("TestFile:\t{0}", testFile);

            using (TsvReportFileProcessor bp = new TsvReportFileProcessor(Log, Process, null, testFile, true, false))
            {
                var p = bp.Process();

                Assert.AreEqual(true, p);
                Assert.AreEqual(bp.Completed, true);
            }
        }

        private void Process(object sender, BulkProcessEventArgs e)
        {
            foreach (var key in e.RowValues.Keys)
            {
                Console.WriteLine("{0}\t{1}", key, e.RowValues[key]);
            }
        }

        private void Log(object sender, LogEventArgs e)
        {
            Console.WriteLine(new MicrosoftOnline.Ads.LogAssistant.LogEventArgs(
                e.LogDateTime,
                (MicrosoftOnline.Ads.LogAssistant.LogLevel)e.LogLevel,
                LogAssistant.LogCategoryType.Exception,
                e.EntryPoint,
                e.Message,
                e.Parameters,
                e.Exception,
                e.TrackingId).ToString());
        }
    }
}
