using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeiXin.Base.Message.EventModels;
using WeiXin.Base.Message.ReceiveModels;

namespace WeiXin.Base.Message
{
    /// <summary>
    /// 消息接口
    /// </summary>
    public abstract class IBaseAction
    {
        #region 消息推送接口
        public virtual string HandleText(TextMessage message)
        {
            return string.Empty;
        }

        public virtual string HandleImage(ImageMessage message)
        {
            return string.Empty;
        }

        public virtual string HandleLink(LinkMessage message)
        {
            return string.Empty;
        }

        public virtual string HandleLocation(LocationMessage message)
        {
            return string.Empty;
        }

     
        public virtual string HandleVideo(VideoMessage message)
        {
            return string.Empty;
        }

        public virtual string HandleVoice(VoiceMessage message)
        {
            return string.Empty;
        }

        #endregion


        #region 事件推送的接口

        /// <summary>
        /// 关注时触发
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual string HandleSubscribe(SubscribeEvent message)
        {
            return string.Empty;
        }

        /// <summary>
        /// 取消关注时触发
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual string HandleUnSubscribe(UnSubscribeEvent message)
        {
            return string.Empty;
        }

        #endregion
 

        ///// <summary>
        ///// 模板消息发送结果
        ///// </summary>
        ///// <param name="eventdata"></param>
        ///// <returns></returns>
        //public string HandleTemplateMessageStatus(WeiXin.Base.Send.Template.Message.TemplateMessageStatusEvent eventdata)
        //{
        //    #region 模板消息发送结果
        //    string sResult = string.Empty;
        //    return sResult;
        //    #endregion
        //}
    }
}
