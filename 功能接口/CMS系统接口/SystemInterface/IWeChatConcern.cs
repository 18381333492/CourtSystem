using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels;
using EFModels.MyModels;

namespace SystemInterface
{

    /// <summary>
    /// 微信关注接口
    /// </summary>
    public abstract class IWeChatConcern : BaseBll
    {
        #region 查询

        /// <summary>
        /// 获取关注回复设置
        /// </summary>
        /// <param name="sConcernId"></param>
        /// <returns></returns>
        public abstract CDELINK_WeChatConcern Get();

        #endregion


        #region 操作

        /// <summary>
        /// 保存关注回复设置
        /// </summary>
        /// <param name="Concern"></param>
        public abstract int KeepWeChatConcern(CDELINK_WeChatConcern Concern);

        #endregion

    }
}
