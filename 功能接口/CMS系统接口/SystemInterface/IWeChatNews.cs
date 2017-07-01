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
    /// 微信图文信息素材接口
    /// </summary>
    public abstract class IWeChatNews : BaseBll
    {
        #region 查询


        #endregion


        #region 操作

        /// <summary>
        /// 添加图文消息
        /// </summary>
        /// <param name="newsName"></param>
        /// <param name="newsData"></param>
        /// <returns></returns>
        public abstract int Insert(CDELINK_WeChatNewsName newsName, List<CDELINK_WeChatNews> newsData);

        #endregion

    }
}
