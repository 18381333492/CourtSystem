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
    /// 网站的写操作相关
    /// </summary>
    public partial class WeChatServices : IWeChat
    {

        /// <summary>
        /// 设置公众号信息
        /// </summary>
        /// <param name="wechat"></param>
        /// <returns></returns>
        public override int SetWeChat(CDELINK_WeChat wechat)
        {
            var tempWeChat = excute.Context.CDELINK_WeChat.AsNoTracking().FirstOrDefault();
            if (tempWeChat == null)
            {
                wechat.ID =Guid.NewGuid();
                excute.Add<CDELINK_WeChat>(wechat);
            }
            else
                excute.Edit<CDELINK_WeChat>(wechat);
            return excute.SaveChange();
        }

    }
}
