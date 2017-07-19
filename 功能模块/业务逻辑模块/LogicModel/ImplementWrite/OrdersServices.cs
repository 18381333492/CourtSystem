using EFModels;
using LogicHandlerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicHandlerModel
{
    /// <summary>
    /// 订单的写操作
    /// </summary>
    public partial class OrdersServices :IOrders
    {

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="sOrdersId"></param>
        /// <returns></returns>
        public override int Confrim(string sOrdersId)
        {
            var idList = sOrdersId.Split(',').ToList();
            var orderlist = excute.Context.ES_Orders.AsNoTracking().Where(m => idList.Contains(m.ID.ToString()));
            foreach(var item in orderlist)
            {
                if (item.iState == 2)
                {//状态为代收货
                    item.iState = 3;
                    excute.Edit<ES_Orders>(item);
                }

            }
            return excute.SaveChange();
        }


        /// <summary>
        /// 添加物流信息
        /// </summary>
        /// <param name="sOrdersId">订单主键ID</param>
        /// <param name="sExpressName">快递名称</param>
        /// <param name="sExpressNo">快递编号</param>
        /// <returns></returns>
        public override int AddLogistics(string sOrdersId, string sExpressName, string sExpressNo)
        {
            var order = excute.Context.ES_Orders.Find(sOrdersId);
            return excute.SaveChange();
        }


    }
}
