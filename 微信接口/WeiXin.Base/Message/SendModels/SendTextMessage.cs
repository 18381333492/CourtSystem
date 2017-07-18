using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXin.Base.Message.SendModels
{
    public class SendTextMessage: SendBaseMessage
    {
        /// <summary>
        /// 回复的消息内容
        /// </summary>
        [CDATA]
        public string Content;
    }
}
