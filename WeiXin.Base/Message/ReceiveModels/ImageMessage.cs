using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXin.Base.Message.ReceiveModels
{
    
    /// <summary>
    /// 图片消息实体
    /// </summary>
    public class ImageMessage : BaseMessage
    {
        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl;

        /// <summary>
        /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据.
        /// </summary>
        public string MediaId;
    }
}
