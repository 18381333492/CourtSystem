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
        /// 通过关注微信公众号获取微信用户信息
        /// </summary>
        /// <param name="sOpenId"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static WeChatUserInfo GetUserByConcern(string sOpenId,string access_token)
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

        /// <summary>
        ///  通过网页授权获取微信用户信息
        /// </summary>
        /// <param name="sAppId">微信公众号APPID</param>
        /// <param name="sAppSecret">sAppSecret</param>
        /// <param name="sRedirectUrl">跳转地址</param>
        /// <returns></returns>
        public static WeChatUserInfo GetUserByAuthorize(string sAppid, string sAppSecret, out string sRedirectUrl)
        {
            sRedirectUrl = string.Empty;
            //redirect_url = string.Empty;
            //string sOpenId = string.Empty;
            //string code = HttpContext.Current.Request.QueryString["code"];
            //if (string.IsNullOrEmpty(code))
            //{//获取网页Code
            //    string requestUrl = ConfigurationManager.AppSettings["WebUrl"] + HttpContext.Current.Request.Path;
            //    string code_url = string.Format(@"https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state=lk#wechat_redirect",
            //           appid,
            //           HttpUtility.UrlEncode(requestUrl, Encoding.UTF8));
            //    redirect_url = code_url;
            //}
            //else
            //{
            //    // 通过code换取网页授权access_token
            //    string url = string.Format(
            //            @"https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code",
            //            appid, secret, code);
            //    var client = new System.Net.WebClient();
            //    //获取返回的数据
            //    if (!string.IsNullOrEmpty(parmasData["openid"]))
            //    {//获取openid
            //        sOpenId = parmasData["openid"];
            //    }
            //    else
            //    {
            //        I
            //    }
            //}
            return null;
        }
    }
}
