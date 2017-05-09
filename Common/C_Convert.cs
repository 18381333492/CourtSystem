using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

    /// <summary>
    /// C# 基本数据类型的扩展
    /// </summary>
    public static class C_Convert
    {

        /// <summary>
        /// 将Object类型转化为Int16
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static short toInt16(this object s, short result = 0)
        {
            Int16.TryParse(s.ToString(), out result);
            return result;
        }

        /// <summary>
        /// 将Object类型转化为Int32
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static int toInt32(this object s,int result=0)
        {
            Int32.TryParse(s.ToString(), out result);
            return result;
        }

        /// <summary>
        /// 将Object类型转化为Int64
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static long toInt64(this object s, long result = 0)
        {
            Int64.TryParse(s.ToString(), out result);
            return result;
        }

        /// <summary>
        /// 将Object类型转化为boolean
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool toBoolean(this object s,bool result=false)
        {
            Boolean.TryParse(s.ToString(), out result);
            return result;
        }

        /// <summary>
        /// 将Object类型转化为decimal
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static decimal toDecimal(this object s, decimal result = 0)
        {
            decimal.TryParse(s.ToString(), out result);
            return result;
        }

        /// <summary>
        /// 将Object类型转化为float
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static float toFloat(this object s,float result = 0)
        {
            float.TryParse(s.ToString(),out result);
            return result;
        }

        /// <summary>
        /// 将Object类型转化为double
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static double toDouble(this object s, double result = 0)
        {
            double.TryParse(s.ToString(),out result);
            return result;
        }

        /// <summary>
        /// 将Object类型转化为UInt16
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static UInt16 toUInt16(this object s, UInt16 result = 0)
        {
            UInt16.TryParse(s.ToString(), out result);
            return result;
        }

        /// <summary>
        /// 将Object类型转化为UInt32
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static UInt32 toUInt16(this object s, UInt32 result = 0)
        {
            UInt32.TryParse(s.ToString(), out result);
            return result;
        }

        /// <summary>
        /// 将Object类型转化为UInt64
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static UInt64 toUInt16(this object s, UInt64 result = 0)
        {
            UInt64.TryParse(s.ToString(), out result);
            return result;
        }

        /// <summary>
        /// 将Object类型转化为DateTime
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static DateTime toDateTime(this object s,DateTime result=default(DateTime))
        {
            DateTime.TryParse(s.ToString(),out result);
            return result;
        }

        /// <summary>
        /// 将时间的字符串格式化成相应格式时间的字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string toDateString(this string s,int iType)
        {
            if (iType == 0)
                return string.Format("{0} 00:00:00", DateTime.Parse(s).ToString("yyyy-MM-dd"));
            else
                return string.Format("{0} 23:59:59", DateTime.Parse(s).ToString("yyyy-MM-dd"));
        }
    }
}
