using EFModels.MyModels;
using LogicHandlerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicHandlerModel
{
    /// <summary>
    /// 订单的读操作实现
    /// </summary>
    public partial class OrdersServices : IOrders
    {

        /// <summary>
        /// 分页获取订单数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public override string List(PageInfo pageInfo, string searchText)
        {
            var res = query.PageQuery<Dictionary<string, object>>("select * from ES_Orders", pageInfo, searchText);
            return res.toJson();
        }
    }
}
