using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXin.Base.Message.ReceiveModels
{
    
    /// <summary>
    /// 位置消息实体
    /// </summary>
    public class LocationMessage : BaseMessage
    {
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Location_X;

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Location_Y;

        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public string Scale;

        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label;

    }
}
