using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXin.Base.Message.ReceiveModels
{
    
    /// <summary>
    /// 语音消息实体
    /// </summary>
    public class VoiceMessage : BaseMessage
    {
        /// <summary>
        ///  MediaId 语音消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId;

        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format;
    }
}
