using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeiXin.Base.Message
{
    /// <summary>
    /// 微信接收消息基础
    /// </summary>
    public class BaseMessage
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName;

        /// <summary>
        /// 发送方帐号(微信用户的OpenID)
        /// </summary>
        public string FromUserName;

        /// <summary>
        /// 发送时间(时间戳)
        /// </summary>
        public int CreateTime;

        /// <summary>
        /// 发送的消息类型
        /// </summary>
        public string MsgType;

        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public long MsgId;

    }

    /// <summary>
    /// 微信的消息类型
    /// </summary>
    public enum MsgType
    {
        TEXT,             //文本

        IMAGE,           //图片

        VOICE,           //声音

        VIDEO,           //视频

        SHORTVIDEO,      //小视屏
            
        LOCATION,        //位置

        LINK,            //链接

        EVENT            //事件类型

    }

    /// <summary>
    /// 微信事件的类型
    /// </summary>
    public enum Event
    {
        SUBSCRIBE,      //订阅/关注事件

        UNSUBSCRIBE,    //取消订阅/取消关注事件

        SCAN,           //用户已关注时的扫码的事件推送

        LOCATION,       //上报地理位置事件

        CLICK,          //自定义菜单事件

        VIEW           //点击菜单跳转链接时的事件推送
    }

}
