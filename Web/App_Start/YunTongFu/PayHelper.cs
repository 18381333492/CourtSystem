using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;

namespace Web.App_Start
{
    /// <summary>
    /// 云通付支付通用的方法
    /// </summary>
    public class PayHelper
    {

        public static string partner =C_Config.ReadAppSetting("partner") ;//云通付PID(必填)

        public static string user_seller =C_Config.ReadAppSetting("user_seller");//云通付商户号(必填)

        // 服务器异步通知页面路径  需http://格式的完整路径，不能加?id=123这类自定义参数，必须外网可以正常访问
        public static string notify_url = "http://mall.ltgirl.com/Mobile/Pay/PayNotify"; //异步回调地址(必填)

        // 页面跳转同步通知页面路径 需http://格式的完整路径，不能加?id=123这类自定义参数，必须外网可以正常访问
        public static string return_url = "http://mall.ltgirl.com/Mobile/Pay/Success"; //同步回调地址(必填)

        public static string key = C_Config.ReadAppSetting("key"); //密钥(必填)


        /// <summary>
        /// 获取支付跳转链接
        /// </summary>
        /// <param name="out_order_no">商户订单号</param>
        /// <param name="subject">商品名称</param>
        /// <param name="total_fee">金额</param>
        /// <param name="body">订单描述</param>
        /// <returns></returns>
        public static string sGetPayUrl(string out_order_no, string subject, string total_fee, string body)
        {
            string sUrlParm = GetUrlParm(out_order_no, subject, total_fee, body);
            return string.Format("http://www.passpay.net/PayOrder/payorder?{0}", sUrlParm);
        }

        /// <summary>
        /// 获取URL参数
        /// </summary>
        /// <param name="partner">云通付PID(必填)</param>
        /// <param name="user_seller">云通付商户号(必填)</param>
        /// <param name="out_order_no">商户网站订单号（唯一）(必填)</param>
        /// <param name="subject">订单名称(必填)</param>
        /// <param name="total_fee">订单价格（整元）(必填)</param>
        /// <param name="body">订单描述(选填)</param>
        /// <param name="notify_url">异步回调地址(必填)</param>
        /// <param name="return_url">同步回调地址(必填)</param>
        /// <param name="key">密钥(必填)</param>
        /// <returns></returns>
        public static string GetUrlParm(string out_order_no,  string subject, string total_fee, string body)
        {
            string prestr = string.Empty;
            if (!string.IsNullOrEmpty(body))
            {
                prestr += "body=" + body + "&";
            }
            prestr += "notify_url=" + notify_url + "&";
            prestr += "out_order_no=" + out_order_no + "&";
            prestr += "partner=" + partner + "&";
            prestr += "return_url=" + return_url + "&";
            prestr += "subject=" + subject + "&";
            prestr += "total_fee=" + total_fee + "&";
            prestr += "user_seller=" + user_seller;
            string sign = md5(prestr + key);
            return prestr + "&sign=" + sign;
        }


        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string md5(string s)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();
            string ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }
            return ret.PadLeft(32, '0');
        }

        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <param name="digits">随机数位数</param>
        /// <returns></returns>
        public static string GetRandom(int digits)
        {
            Random rad = new Random();//实例化随机数产生器rad；
            int value = rad.Next(int.Parse(Math.Pow(10, digits - 1).ToString()), int.Parse(Math.Pow(10, digits).ToString()));
            return value.ToString();
        }
    }
}