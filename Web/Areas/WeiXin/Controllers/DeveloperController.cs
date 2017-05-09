using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using WeiXin.Base;
using System.IO;
using System.Text;
using WeiXin.Tool;
using WeiXin.Base.Message;
using Web.App_Start.WeiXinMessage;

namespace Web.Areas.WeiXin.Controllers
{
    /// <summary>
    /// 开发者控制器
    /// </summary>
    public class DeveloperController : Controller
    {
        //
        // GET: /WeiXin/Developer/
        /// <summary>
        /// 验证微信开发者
        /// </summary>
        public void Check()
        {
            if (Request.HttpMethod.ToUpper() == "GET")
            {
                string signature = Request["signature"].ToString();//微信加密签名
                string timestamp = Request["timestamp"].ToString();//时间戳
                string nonce = Request["nonce"].ToString();        //随机数
                string echostr = Request["echostr"].ToString();    //随机字符串
                string token = C_Config.ReadAppSetting("token");   //获取微信配置token

                string result = Developer.Valiate(signature, timestamp, nonce, echostr, token);
                Response.Write(result);
            }
            else
            {//接收微信消息
                string result = HandleMessage(Request,new AutoHandleMessage());
                Response.Write(result);
            }
        }


        /// <summary>
        /// 处理微信消息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string HandleMessage(HttpRequestBase request,IBaseAction Action)
        {
            StreamReader sr = new StreamReader(request.InputStream, Encoding.UTF8);
            string requestXmlMessage = sr.ReadToEnd();

            // 获取微信发送来的消息类型
            string sMsgType = XmlHelper.getTextByNode(requestXmlMessage, "MsgType");

            var handle = new HandleMessage(Action);

            if (sMsgType.ToUpper() == "EVENT")
            {//事件的推送
                string EventType= XmlHelper.getTextByNode(requestXmlMessage, "Event");//获取事件类型
                Event eventType = (Event)Enum.Parse(typeof(Event), EventType.ToUpper());
                return handle.ProcessEvent(eventType, requestXmlMessage);
            }
            else
            {//消息的推送

                // 找到对应的消息类型
                MsgType msgType = (MsgType)Enum.Parse(typeof(MsgType), sMsgType.ToUpper());
                return handle.ProcessMessage(msgType, requestXmlMessage);
            }
        }
    }
}
