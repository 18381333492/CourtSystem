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
        /// 锁对象
        /// </summary>
        private static object _lock_object = new object();

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
                    item.dDoneTime = DateTime.Now;
                    excute.Edit<ES_Orders>(item);
                }

            }
            return excute.SaveChange();
        }


        /// <summary>
        /// 设置物流信息
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public override int SetLogistics(ES_Orders orders)
        {
            var oldOrder = excute.Context.ES_Orders.Find(orders.ID);
            oldOrder.sLogisticsCompany = orders.sLogisticsCompany;
            oldOrder.sLogisticsNo = orders.sLogisticsNo;
            return excute.SaveChange();
        }


        /// <summary>
        /// 预订下单
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public override int BookOrder(ES_Orders orders)
        {
            lock (_lock_object)
            {//防止商品库存的数据写脏
                var goods = excute.Context.ES_Goods.AsNoTracking().FirstOrDefault(m=>m.sGoodsNo==orders.sGoodsNo);
                if(goods.iCount>= orders.iCount)
                {//库存足够
                    orders.ID = Guid.NewGuid();
                    orders.dBookTime = DateTime.Now;
                    orders.sOrderNo = orders.dBookTime.ToString("yyyyMMddHHmmssfff");
                    orders.iState = 0;
                    orders.iOrderType = goods.iGoodsType;
                    orders.iChannel = 0;//订单的渠道 默认值0
                    orders.dAllPrices = goods.sGoodsPrices * orders.iCount;//订单总价
                    if (goods.bIsActivity)
                    {//有活动
                        if (goods.IsRateGiving)//按比例增长
                            orders.dDiscountPrices = goods.sDisPrices * orders.iCount;//优惠价格
                        else orders.dDiscountPrices = goods.sDisPrices;
                    }
                    else
                        orders.sActivity = string.Empty;
                    orders.iIntegral = goods.iIntegral;//积分
                    if (goods.IsRateAdd)
                    {//积分是否按比例增长
                        orders.iIntegral = orders.iIntegral * orders.iCount;
                    }
                    excute.Add<ES_Orders>(orders);
                    //修改商品库存
                    goods.iCount = goods.iCount - orders.iCount;
                    excute.Edit<ES_Goods>(goods);
                    return excute.SaveChange();
                }
                else
                {
                    throw new ApplicationException("商品库存不足!");
                }
            }
        }



    }
}
