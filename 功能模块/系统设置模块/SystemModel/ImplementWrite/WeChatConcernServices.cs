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
    /// 微信关注回复的写操作相关
    /// </summary>
    public partial class WeChatConcernServices : IWeChatConcern
    {
        /// <summary>
        /// 保存关注回复设置
        /// </summary>
        /// <param name="Concern"></param>
        public override int KeepWeChatConcern(ES_WeChatConcern Concern)
        {
            var item = excute.Context.ES_WeChatConcern.AsNoTracking().FirstOrDefault(m=>m.ID==Concern.ID);
            if (item == null)
            {//新增
                Concern.ID = Guid.NewGuid();
                excute.Add<ES_WeChatConcern>(Concern);
            }else
            {//编辑
                excute.Edit<ES_WeChatConcern>(Concern);
            }
            return excute.SaveChange();

        }
    }
}
