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
        ///// 关注时触发
        ///// </summary>
        ///// <param name="eventdata"></param>
        ///// <returns></returns>
        //public string HandleSubscribeEvent(WeiXin.Base.Receive.Event.SubscribeEvent eventdata)
        //{
        //    #region 关注时 触发
        //    string sResult = string.Empty;
        //    try
        //    {
        //        string fromUserName = eventdata.FromUserName.Text;              //openid
        //        string toUserName = eventdata.ToUserName.Text;                  //原始id
        //        string eventKey = eventdata.
        //                            EventKey.
        //                            Text.
        //                            Replace("qrscene_", string.Empty);          //通过二维码扫码关注 获得的

        //        sResult = WeiXinTool.BuildAttentionAutoReply(fromUserName, toUserName, eventKey);

        //    }
        //    catch (Exception ex)
        //    {
        //        SystemLog.Logs.GetLog().WriteErrorLog(ex);
        //    }
        //    return sResult;
        //    #endregion
        //}

        ///// <summary>
        ///// 取消关注
        ///// </summary>
        ///// <param name="eventdata"></param>
        ///// <returns></returns>
        //public string HandleUnSubscribeEvent(WeiXin.Base.Receive.Event.UnSubscribeEvent eventdata)
        //{
        //    #region 取消关注
        //    string sResult = string.Empty;
        //    return sResult;
        //    #endregion
        //}

        //public string HandleViewClick(WeiXin.Base.Receive.Event.ViewClick eventdata)
        //{
        //    return string.Empty;
        //}

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
