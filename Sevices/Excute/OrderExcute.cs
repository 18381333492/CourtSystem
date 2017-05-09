using EFModel;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Sevices
{
    public partial class OrdersService: ServiceBase
    {

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public int Add(Orders item)
        {
            if (string.IsNullOrEmpty(item.sActivity))
                item.sActivity = string.Empty;
            if (string.IsNullOrEmpty(item.sOrderRemark))
                item.sOrderRemark = string.Empty;
            item.ID = Guid.NewGuid();
            item.sOrderNo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            item.dBookTime = DateTime.Now;
            item.dPayTime = DateTime.Now;
            item.dTipTime = DateTime.Now;//默认时间
            item.iState = 0;//待付款
            excute.Add<Orders>(item);
            return excute.SaveChange();
        }


        /// <summary>
        /// 检查订单状态是否能发起支付
        /// </summary>
        /// <param name="sOrderId"></param>
        /// <returns></returns>
        public bool CheckOrderState(Guid sOrderId,out Orders order)
        {
            order = excute.db.Orders.Find(sOrderId);
            return excute.db.Orders.Any(m => m.ID == sOrderId && m.iState == 0);
        }


        /// <summary>
        /// 根据ID修改订单状态
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int Alter(Guid ID)
        {
            var order= excute.db.Orders.Find(ID);
            order.iState = order.iState + 1;
            return  excute.SaveChange();
        }

        /// <summary>
        /// 根据订单ID取消订单
        /// </summary>
        /// <param name="sOrderId"></param>
        /// <returns></returns>
        public int CancelOrder(Guid sOrderId)
        {
            var order = excute.db.Orders.Find(sOrderId);
            order.bIsDeleted = true;
            return excute.SaveChange();
        }

        /// <summary>
        /// 提醒卖家发货
        /// </summary>
        /// <param name="sOrderId"></param>
        /// <returns></returns>
        public int TipOrder(Guid sOrderId)
        {
            var order = excute.db.Orders.Find(sOrderId);
            if (!order.bIsTip)
            {
                order.bIsTip = true;
                order.dTipTime = DateTime.Now;
                return excute.SaveChange();
            }
            else
            {//提醒过
                var Timesapn=DateTime.Now.Subtract(order.dTipTime);
                if (Timesapn.Days < 1)
                {
                    return 2;//一天只能提醒卖家发货一次
                }
                else
                {
                    order.dTipTime = DateTime.Now;
                    return excute.SaveChange();
                }
            }
        }

        
        /// <summary>
        /// 根据订单编号修改订单状态
        /// </summary>
        /// <param name="sOrderNO"></param>
        /// <returns></returns>
        public int AlterStateByOrderNo(string sOrderNO,out Orders order)
        {
             order = excute.db.Orders.FirstOrDefault(m=>m.sOrderNo== sOrderNO);
            if (order.iState == 0)
            {
                order.iState = 1;//修改为已支付
                order.dPayTime = DateTime.Now;
                return  excute.SaveChange();
            }
            else
            {
                return -2;
            }
        }


        /// <summary>
        /// 添加物流信息
        /// </summary>
        /// <param name="sOrderId"></param>
        /// <param name="sWuliuCompany">物流公司</param>
        /// <param name="sWuliuNumber">物流单号</param>
        public int AddWuliu(Guid sOrderId, string sWuliuCompany, string sWuliuNumber)
        {
            var order = excute.db.Orders.Find(sOrderId);
            order.sWuliuCompany = sWuliuCompany;
            order.sWuliuNumber = sWuliuNumber;
            return excute.SaveChange();
        }
    }
}
