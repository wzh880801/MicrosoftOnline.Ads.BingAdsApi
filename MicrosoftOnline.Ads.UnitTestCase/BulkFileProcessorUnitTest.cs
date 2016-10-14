using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using MicrosoftOnline.Ads.BingAdsApi;

namespace MicrosoftOnline.Ads.UnitTestCase
{
    [TestClass]
    public class BulkFileProcessorUnitTest
    {
        private string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        private string testFile = null;

        [TestInitialize]
        public void Init()
        {
            testFile = Path.Combine(appPath, "AccountsTest.zip");
        }

        [TestMethod]
        public void TestParse()
        {
            Console.WriteLine("TestFile:\t{0}", testFile);

            using (BulkFileProcessor bp = new BulkFileProcessor(Log, Process, null, testFile, true, false))
            {
                var p = bp.Process();

                Assert.AreEqual(true, p);

                Assert.AreEqual(bp.AccountId, 12345678);
                Assert.AreEqual(bp.Columns.Length, 157);
                Assert.AreEqual(bp.FormatVersion, "4.0");
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
