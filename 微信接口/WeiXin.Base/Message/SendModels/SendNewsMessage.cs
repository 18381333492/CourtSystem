using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXin.Base.Message.SendModels
{
    /// <summary>
    /// 图文消息实体
    /// </summary>
    public class SendNewsMessage: SendBaseMessage
    {
        /// <summary>
        /// 图文消息个数，限制为8条以内
        /// </summary>
        public int ArticleCount;

        /// <summary>
        /// 多条图文消息信息，默认第一个item为大图,注意，如果图文数超过8，则将会无响应
        /// </summary>
        [CDATA]
        public List<item> Articles;

    }


    /// <summary>
    /// 单个图文消息实体
    /// </summary>
    public class item
    {
        /// <summary>
        /// 图文消息标题
        /// </summary>
        public string Title;

        /// <summary>
        /// 图文消息描述
        /// </summary>
        public string Description;

        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200
        /// </summary>
        public string PicUrl;

        /// <summary>
        /// 点击图文消息跳转链接
        /// </summary>
        public string Url;
    }
}
