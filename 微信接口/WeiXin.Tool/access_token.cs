using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using Common;

namespace WeiXin.Tool
{
    /// <summary>
    /// 获取微信access_token帮助类
    /// </summary>
    public class access_token
    {
      
        public string appid;

        public string secret;

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        public access_token(string appid,string secret)
        {
            this.appid = appid;
            this.secret = secret;
        }


        /// <summary>
        /// 获取access_token
        /// </summary>
        /// <returns></returns>
        public string Get()
        {
            string token = string.Empty;
            var TimeValue = C_Cache.GetCache("token_out_time");
            if (TimeValue != null)
            {
                var date = C_Convert.toDateTime(TimeValue);//获取过期时间
                if (DateTime.Now > date)
                {
                    token = Get_access_token();
                }
                else
                {
                    token = C_Cache.GetCache("access_token").ToString();
                }
            }
            else
                token = Get_access_token();
            return token;
        }

        /// <summary>
        /// 获取access_token
        /// </summary>
        /// <returns></returns>
        public string Get_access_token()
        {
            string token = string.Empty;
            string sUrl = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, secret);
            string result = HttpHelper.HttpGet(sUrl);
            JObject res = JObject.Parse(result);
            if (res["access_token"] != null)
            {//返回成功
                C_Cache.SetCache("token_out_time",DateTime.Now.AddSeconds(7000),7000);
                C_Cache.SetCache("access_token", res["access_token"].ToString(), 7000);
                token = res["access_token"].ToString();
            }
            return token;
        }
    }
}
