using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterface;
using EFModels;
using Common;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SystemModel
{
    /// <summary>
    /// 微信关注回复的读操作相关
    /// </summary>
    public partial class WeChatConcernServices : IWeChatConcern
    {
        /// <summary>
        /// 根据主键ID获取关注回复设置
        /// </summary>
        /// <param name="sConcernId"></param>
        /// <returns></returns>
        public override CDELINK_WeChatConcern Get()
        {
            var item = query.SingleQuery<CDELINK_WeChatConcern>("select * from CDELINK_WeChatConcern");
            return item;
        }

    }
}
