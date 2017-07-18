using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXin.Base.Message.EventModels
{
    /// <summary>
    /// 取消关注事件的实体对象
    /// </summary>
    public class UnSubscribeEvent : BaseMessage
    {
        public string Event;
    }
}
