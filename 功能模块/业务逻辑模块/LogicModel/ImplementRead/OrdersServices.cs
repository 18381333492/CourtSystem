using EFModels;
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
        public override string List(PageInfo pageInfo, string searchText, string dStaTime, string dEndTime, int iState, int iOrderType, int iChannel)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append("select * from ES_Orders where 1=1");
            if (iState > -1)
                sSql.AppendFormat(" and iState={0}", iState);
            if (iOrderType > -1)
                sSql.AppendFormat(" and iOrderType={0}", iOrderType);
            if (iChannel > -1)
                sSql.AppendFormat(" and iChannel={0}", iChannel);
            if(!string.IsNullOrEmpty(dStaTime))
                sSql.AppendFormat(" and dBookTime>='{0}'", dStaTime);
            if (!string.IsNullOrEmpty(dEndTime))
                sSql.AppendFormat(" and dBookTime<='{0}'", dEndTime);
            if(!string.IsNullOrEmpty(searchText))
                sSql.AppendFormat(" and (sReceiver like '%{0}%' or sPhone like '%{0}%' or sGoodsNo like '%{0}%' or sOrderNo like '%{0}%')", searchText);
            var res = query.PageQuery<Dictionary<string, object>>(sSql.ToString(), pageInfo, searchText);
            return res.toJson();
        }


        /// <summary>
        /// 根据主键ID获取订单对象
        /// </summary>
        /// <param name="sOrderId"></param>
        /// <returns></returns>
        public override ES_Orders Get(string sOrderId)
        {
            return query.Find<ES_Orders>(sOrderId);
        }
    }
}
