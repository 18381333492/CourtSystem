using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeiXin.Tool
{
    /// <summary>
    /// 微信获取用户信息助手
    /// </summary>
    public class UserInfoHelper
    {
        /// <summary>
        /// 根据openId获取用户信息
        /// </summary>
        /// <param name="sOpenId"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static WeChatUserInfo GetUserInfo(string sOpenId,string access_token)
        {
            var userInfo = new WeChatUserInfo();
            if (!(string.IsNullOrEmpty(sOpenId)||string.IsNullOrEmpty(access_token)))
            {
                string sUrl = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", access_token, sOpenId);
                var resString=HttpHelper.HttpGet(sUrl);
                var result = JObject.Parse(resString);
                if (result["errcode"] == null)
                {
                    userInfo = JsonConvert.DeserializeObject<WeChatUserInfo>(resString);    
                }
                else
                {
                    userInfo.message = result["errcode"] + " " + result["errmsg"];
                }
            }
            else
            {//sOpenId或者access_token为空
                userInfo.message = "sOpenId或者access_token为空";
            }
            return userInfo;
        }
    }
}
