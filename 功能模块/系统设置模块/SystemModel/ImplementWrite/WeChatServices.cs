using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterface;
using EFModels;
using Common;

namespace SystemModel
{
    /// <summary>
    /// 微信公众号的写操作相关
    /// </summary>
    public partial class WeChatServices : IWeChat
    {

        /// <summary>
        /// 设置公众号信息
        /// </summary>
        /// <param name="wechat"></param>
        /// <returns></returns>
        public override int SetWeChat(ES_WeChat wechat)
        {
            var tempWeChat = excute.Context.ES_WeChat.AsNoTracking().FirstOrDefault();
            if (tempWeChat == null)
            {
                wechat.ID =Guid.NewGuid();
                excute.Add<ES_WeChat>(wechat);
            }
            else
                excute.Edit<ES_WeChat>(wechat);
            return excute.SaveChange();
        }

    }
}
