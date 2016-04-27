using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingAdsApiDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Run(new DownloadAccountPerformanceReportDemo(
                new DateTime(2016, 1, 1),
                new DateTime(2016, 1, 1),
                527228,
                @"d:\report.zip"));
        }

        static void Run(IDemo demo)
        {
            demo.Run();
        }
    }
}
