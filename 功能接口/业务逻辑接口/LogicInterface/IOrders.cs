using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicHandlerInterface
{
    /// <summary>
    /// 订单业务逻辑接口
    /// </summary>
    public abstract class IOrders : BaseBll
    {
        #region 查询

        /// <summary>
        /// 分页获取订单数据列表
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
