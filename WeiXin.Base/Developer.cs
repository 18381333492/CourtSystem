using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;


namespace WeiXin.Base
{
    /// <summary>
    /// 微信开发者
    /// </summary>
    public class Developer
    {

        /// <summary>
        /// 验证消息是否来自微信服务器
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        public static string Valiate(string signature,string timestamp,string nonce, string echostr,string token)
        {
            // 开发者通过检验signature对请求进行校验（下面有校验方式）。若确认此次GET请求来自微信服务器，请原样返回echostr参数内容，则接入生效，成为开发者成    功，否则接入失败。加密 / 校验流程如下：
            //1）将token、timestamp、nonce三个参数进行字典序排序
            //2）将三个参数字符串拼接成一个字符串进行sha1加密
            //3）开发者获得加密后的字符串可与signature对比，标识该请求来源于微信

            string[] array ={ token, timestamp, nonce };
            Array.Sort(array);//字典排序
            string newSignature = C_Security.SHA1(string.Join("", array));
            if (newSignature == signature.ToUpper())
                return echostr;
            else
                return "Valiate Fail";
        
        }
    }
}
