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
            //Run(new DownloadAccountPerformanceReportDemo(
            //    new DateTime(2016, 1, 1),
            //    new DateTime(2016, 6, 1),
            //    42164768,
            //    @"d:\report.zip"));

            //Run(new GetTargetsDemo(42164768));

            //GetCustomerIndo
            //Run(new GetCustomerInfoDemo());

            //GetAccounts
            //Run(new GetAccountListUnderCustomerDemo());

            //Insertion Orders
            Run(new GetAccountInsertionOrdersDemo());

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static void Run(IDemo demo)
        {
            demo.Run();
        }
    }
}
