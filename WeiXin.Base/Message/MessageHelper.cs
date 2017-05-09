using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeiXin.Base.Message.ReceiveModels;
using WeiXin.Base.Message.SendModels;
using WeiXin.Tool;

namespace WeiXin.Base.Message
{
    /// <summary>
    /// 消息处理函数
    /// </summary>
    public class MessageHelper
    {
        /// <summary>
        /// 处理带有CDATA特性的字段
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string Handle_CDATA(Object message)
        {
            Type t = message.GetType();
            FieldInfo[] Fileds =t.GetFields();
            foreach (FieldInfo m in Fileds)
            {
                if (m.GetCustomAttribute(typeof(CDATAAttribute)) != null)
                {//是否有CDATA特性
                    if (m.FieldType == typeof(List<item>))
                    {//是图文消息
                        List<item> Articles = m.GetValue(message) as List<item>;
                        foreach(var item in Articles)
                        {
                            item.Title = string.Format("<![CDATA[{0}]]>", item.Title);
                            item.Description = string.Format("<![CDATA[{0}]]>", item.Description);
                            item.PicUrl = string.Format("<![CDATA[{0}]]>", item.PicUrl);
                            item.Url = string.Format("<![CDATA[{0}]]>", item.Url);
                        }
                        m.SetValue(message, Articles);
                    }
                    else
                    {
                        string res = m.GetValue(message).ToString();
                        res = string.Format("<![CDATA[{0}]]>", res);
                        m.SetValue(message, res);
                    }
                }
            }
            return XmlHelper.ObjectToXml(message);
        }

        /// <summary>
        /// 回复文本消息
        /// </summary>
        /// <param name="text">回复的内容</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string Text(string text, BaseMessage message)
        {
            SendTextMessage send = new SendTextMessage();
            send.Initialize(message);
            send.Content = text;
            send.MsgType = "text";
            return Handle_CDATA(send);
        }

        /// <summary>
        /// 回复图文消息
        /// </summary>
        /// <returns></returns>
        public static string News(List<item> Articles, BaseMessage message)
        {
            SendNewsMessage send = new SendNewsMessage();
            send.Initialize(message);
            send.MsgType = "news";
            send.ArticleCount = Articles.Count;
            send.Articles = Articles;
            return Handle_CDATA(send);
        }
    }
}
