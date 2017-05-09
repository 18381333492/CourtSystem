using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using WeiXin.Base.Message;
using WeiXin.Base.Message.EventModels;
using WeiXin.Base.Message.ReceiveModels;
using WeiXin.Base.Message.SendModels;

namespace Web.App_Start.WeiXinMessage
{
    public class AutoHandleMessage : IBaseAction
    {
        //需要处理那些消息就重写

        /// <summary>
        /// 处理微信文本消息
        /// </summary>
        /// <returns></returns>
        public override string HandleText(TextMessage message)
        {
            List<item> Articles = new List<item>();
            Articles.Add(new item()
            {
                Title = "测试图文消息1",
                Url = "https://www.baidu.com",
                PicUrl = "https://ss0.bdstatic.com/70cFuHSh_Q1YnxGkpoWK1HF6hhy/it/u=584375705,2843051093&fm=116&gp=0.jpg",
            });
            Articles.Add(new item()
            {
                Title = "测试图文消息2",
                Url = "https://www.baidu.com",
                PicUrl = "https://ss0.bdstatic.com/70cFuHSh_Q1YnxGkpoWK1HF6hhy/it/u=584375705,2843051093&fm=116&gp=0.jpg",
            });
            return MessageHelper.News(Articles,message);
           // return  MessageHelper.Text("成功了啊", message);

        }

        /// <summary>
        /// 处理用户关注
        /// </summary>
        /// <returns></returns>
        public override string HandleSubscribe(SubscribeEvent message)
        {
            return MessageHelper.Text("关注时回复成功了", message);
        }

    }
}