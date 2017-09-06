using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace EFModels.MyModels
{
    /// <summary>
    /// WebSockket消息model
    /// </summary>
    public class SocketMode
    {
        /// <summary>
        /// 是否是首次链接
        /// </summary>
        public bool IsFirstConnect
        {
            get;
            set;
        } = false;

        /// <summary>
        /// Socket链接的唯一标识
        /// </summary>
        public string sPriKey
        {
            get;
            set;
        }

        /// <summary>
        /// 需要发送消息的socket的标识
        /// </summary>
        public string sSendPriKey
        {
            get;
            set;
        }

        /// <summary>
        /// 发送/接受的数据
        /// </summary>
        public object data
        {
            get;
            set;
        }


        public string toJson()
        {
            return C_Json.toJson(this);
        }
    }
}
