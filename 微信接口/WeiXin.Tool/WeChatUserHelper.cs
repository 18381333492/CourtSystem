using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Common;

namespace WeiXin.Tool
{
    /// <summary>
    /// 微信获取用户信息助手
    /// </summary>
    public class WeChatUserHelper
    {
        /// <summary>
        /// 通过关注微信公众号获取微信用户信息
        /// </summary>
        /// <param name="sOpenId"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static WeChatUser GetUserByConcern(string sOpenId,string access_token)
        {
            var userInfo = new WeChatUser();
            if (!(string.IsNullOrEmpty(sOpenId)||string.IsNullOrEmpty(access_token)))
            {
                string sUrl = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", access_token, sOpenId);
                var resString=HttpHelper.HttpGet(sUrl);
                var result =C_Json.ParseObject(resString);
                if (result["errcode"] == null)
                {
                    userInfo = C_Json.Deserialize<WeChatUser>(resString);
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
        public static WeChatUser GetUserByAuthorize(string sAppid, string sAppSecret, out string sRedirectUrl)
        {
            sRedirectUrl = string.Empty;
            string code = HttpContext.Current.Request.QueryString["code"];
            if (string.IsNullOrEmpty(code))
            {//获取网页Code
                string requestUrl = string.Format("http://{0}{1}", HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Path);
                string code_url = string.Format(@"https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state=lk#wechat_redirect",
                       sAppid,
                       HttpUtility.UrlEncode(requestUrl, Encoding.UTF8));
                sRedirectUrl = code_url;
                return null;
            }
            else
            {
                var userInfo = new WeChatUser();
                // 通过code换取网页授权access_token
                string url = string.Format(
                        @"https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code",
                        sAppid, sAppSecret, code);
                var resString = HttpHelper.HttpGet(url);
                var result = C_Json.ParseObject(resString);
                //获取返回的数据
                if (!string.IsNullOrEmpty(result["openid"].ToString()))
                {
                    //通过openid和access_token获取用户信息
                    url = string.Format(@"https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", result["access_token"], result["openid"]);
                    resString=HttpHelper.HttpGet(url);
                    result = C_Json.ParseObject(resString);
                    if (!string.IsNullOrEmpty(result["openid"].ToString()))
                        userInfo = C_Json.Deserialize<WeChatUser>(resString);
                    else
                        userInfo.message = result["errcode"] + " " + result["errmsg"];
                }
                else
                {
                    userInfo.message = result["errcode"] + " " + result["errmsg"];
                }
                return userInfo;
            }
        }
    }
}
