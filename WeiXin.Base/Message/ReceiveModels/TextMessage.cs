using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXin.Base.Message.ReceiveModels
{
    
    /// <summary>
    /// 文本消息实体
    /// </summary>
    public class TextMessage: BaseMessage
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content;
    }
}
