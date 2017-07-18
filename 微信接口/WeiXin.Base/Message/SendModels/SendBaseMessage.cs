using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXin.Base.Message.SendModels
{
    /// <summary>
    /// 发送的基础消息模型
    /// </summary>
    public class SendBaseMessage
    {
        /// <summary>
        /// 接收方帐号（收到的OpenID）
        /// </summary>
        [CDATA]
        public string ToUserName;

        /// <summary>
        /// 开发者微信号
        /// </summary>
        [CDATA]
        public string FromUserName;

        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public int  CreateTime;

        /// <summary>
        /// 发送的消息类型
        /// </summary>
        [CDATA]
        public string MsgType;


        public void Initialize(BaseMessage message)
        {
            this.ToUserName = message.FromUserName;
            this.FromUserName = message.ToUserName;
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.CreateTime = Convert.ToInt32(ts.TotalSeconds);
        }
    }
}
