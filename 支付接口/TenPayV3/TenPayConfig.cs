using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace TenPayV3
{
    /// <summary>
    /// 微信支付相关的一些配置参数
    /// </summary>
    public class TenPayConfig
    {
        /// <summary>
        /// 微信公众号唯一标识
        /// </summary>
        public static string appid = C_XmlHelp.ReadXmlConfig("appid");

        /// <summary>
        /// 微信公众号密码
        /// </summary>
        public static string appsecret = C_XmlHelp.ReadXmlConfig("appsecret");

        /// <summary>
        /// 商户号(微信支付分配的商户号)
        /// </summary>
        public static string mch_id = C_XmlHelp.ReadXmlConfig("mch_id");

        /// <summary>
        /// 密钥(key设置路径：微信商户平台(pay.weixin.qq.com)-->账户设置-->API安全-->密钥设置)
        /// </summary>
        public static string key = C_XmlHelp.ReadXmlConfig("key");

        /// <summary>
        /// 微信异步回调地址
        /// </summary>
        public static string notify_url = C_XmlHelp.ReadXmlConfig("notify_url");

        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <returns></returns>
        public static string nonce_str()
        {
            // 生成随机数算法
            //微信支付API接口协议中包含字段nonce_str，主要保证签名不可预测。
            //我们推荐生成随机数算法如下：调用随机数函数生成，将得到的值转换为字符串。
            Random Ran = new Random();
            string nonce_str=Ran.Next(11111111, 99999999).ToString()+"CNMB";
            return C_Security.MD5(nonce_str);
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        public static string time_stamp()
        {
            //时间戳
            //标准北京时间，时区为东八区，自1970年1月1日 0点0分0秒以来的秒数。
            //注意：部分系统取到的值为毫秒级，需要转换成秒(10位数字).
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

    }

    /// <summary>
    /// 微信js支付的配置参数
    /// </summary>
    public class JS_PayConfig
    {
        public string appId;
        public string timeStamp;
        public string nonceStr;
        public string package;
        public string paySign;
    }
}
