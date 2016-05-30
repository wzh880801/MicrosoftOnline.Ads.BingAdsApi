using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MicrosoftOnline.Ads.BingAdsApi
{
    //static class this 关键词才有效
    public static class Common
    {
        public static object[] ToColumnTypeValues(this object[] rowValues, DataColumnCollection columns)
        {
            if (rowValues == null || rowValues.Length == 0 || columns == null || columns.Count == 0)
                throw new Exception("行或列长度为空");
            if (rowValues.Length != columns.Count)
                throw new Exception("行的数据长度和列的长度不一致");

            List<object> result = new List<object>();
            for (int i = 0; i < rowValues.Length; i++)
            {
                result.Add(Convert(rowValues[i], columns[i].DataType));
            }

            return result.ToArray();
        }

        private static object Convert(object o, Type dataType)
        {
            if (o == null)
                return new object();

            if (dataType == typeof(System.String))
                return string.Format("{0}", o);

            try
            {
                return System.Convert.ChangeType(o, dataType);
            }
            catch
            {
                return Default(dataType);
            }
        }

        private static object Default(Type dataType)
        {
            if (dataType == typeof(System.Int16) ||
                dataType == typeof(System.Int32) ||
                dataType == typeof(System.Int64) ||
                dataType == typeof(System.Byte) ||
                dataType == typeof(System.SByte))
                return 0;
            else if (dataType == typeof(System.Double))
                return 0d;
            else if (dataType == typeof(System.Decimal))
                return 0m;
            else if (dataType == typeof(System.DateTime))
                return new DateTime();
            else if (dataType == typeof(System.Boolean))
                return false;

            return new object();
        }

        public static int ToYearMonth(this DateTime date)
        {
            return date.Year * 100 + date.Month;
        }

        public static int ToYearMonthDay(this DateTime date)
        {
            return date.Year * 10000 + date.Month * 100 + date.Day;
        }

        public static DateTime ToFirstDayInThisMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime ToLastDayInThisMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        public static DateTime ToDateTime(this int date)
        {
            if (date.ToString().Length != 6)
                return DateTime.Now;
            return DateTime.Parse(string.Format("{0}-{1}-01", date.ToString().Substring(0, 4), date.ToString().Substring(4, 2)));
        }
    }
}
