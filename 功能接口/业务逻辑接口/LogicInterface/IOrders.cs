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

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="sOrdersId">订单主键ID或主键ID集合</param>
        public abstract int Confrim(string sOrdersId);


        /// <summary>
        /// 添加物流信息
        /// </summary>
        /// <param name="sOrdersId">订单主键ID</param>
        /// <param name="sExpressName">快递名称</param>
        /// <param name="sExpressNo">快递编号</param>
        /// <returns></returns>
        public abstract int AddLogistics(string sOrdersId, string sExpressName, string sExpressNo);

        #endregion
    }
}
