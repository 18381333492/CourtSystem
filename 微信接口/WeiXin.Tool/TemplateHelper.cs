using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;


namespace WeiXin.Tool
{

    /// <summary>
    /// 微信发送模板消息的助手
    /// </summary>
    public class TemplateHelper
    {
        /// <summary>
        ///  发送模板消息
        /// </summary>
        /// <param name="content">发送的内容</param>
        /// <param name="access_token">接口调用凭证</param>
        /// <returns></returns>
        public static void SendMessage(string content, string access_token)
        {
            string sUrl = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", access_token);
            var result=HttpHelper.HttpPost(sUrl, content);
            if (!string.IsNullOrEmpty(result))
            {
                var ParamData =C_Json.ParseObject(result);
                if (ParamData["errcode"].ToString()=="0")
                {//发送成功

                }
                else
                {

                }
            }
        } 
    }
}
