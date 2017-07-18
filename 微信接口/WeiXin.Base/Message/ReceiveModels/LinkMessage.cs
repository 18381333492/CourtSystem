using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXin.Base.Message.ReceiveModels
{
    
    /// <summary>
    /// 链接消息实体
    /// </summary>
    public class LinkMessage : BaseMessage
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title;

        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description;

        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url;
    }
}
