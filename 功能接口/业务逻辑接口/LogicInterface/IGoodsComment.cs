using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicHandlerInterface
{
    /// <summary>
    /// 商品评价相关接口
    /// </summary>
    public abstract class IGoodsComment:BaseBll
    {
        #region 查询

        /// <summary>
        /// 分页获取商品评价数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public abstract string List(PageInfo pageInfo, string searchText);

        #endregion


        #region 操作


        #endregion
    }
}
