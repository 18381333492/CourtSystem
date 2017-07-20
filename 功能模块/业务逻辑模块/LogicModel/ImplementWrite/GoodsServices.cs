using EFModels;
using LogicHandlerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace LogicHandlerModel
{
    /// <summary>
    /// 商品的写操作
    /// </summary>
    public partial class GoodsServices:IGoods
    {
        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public override int Insert(ES_Goods goods)
        {
            goods.ID = Guid.NewGuid();
            goods.sGoodsNo ="ES"+C_String.RandomCodeNum(6);
            goods.dInsertTime = DateTime.Now;
            goods.bIsShelves = true;
            excute.Add<ES_Goods>(goods);
            return excute.SaveChange();
        }

        /// <summary>
        /// 编辑商品
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public override int Update(ES_Goods goods)
        {
            var oldGoods = excute.Context.ES_Goods.Find(goods.ID);
            oldGoods.sGoodsName = goods.sGoodsName;
            oldGoods.sGoodsPrices = goods.sGoodsPrices;
            oldGoods.iGoodsType = goods.iGoodsType;
            oldGoods.iCount = goods.iCount;
            return excute.SaveChange();
        }

        /// <summary>
        /// 上下架商品
        /// </summary>
        /// <param name="sGoodsId"></param>
        /// <returns></returns>
        public override int Shelves(string sGoodsId)
        {
            var idList = sGoodsId.Split(',').ToList();
            var sGoodList = excute.Context.ES_Goods.AsNoTracking().Where(m => idList.Contains(m.ID.ToString()));
            foreach(var goods in sGoodList)
            {
                goods.bIsShelves = goods.bIsShelves ? false : true;
                excute.Edit<ES_Goods>(goods);
            }
            return excute.SaveChange();
        }


        /// <summary>
        /// 商品活动的开启与禁用
        /// </summary>
        /// <param name="sGoodsId"></param>
        /// <returns></returns>
        public override int Activity(string sGoodsId)
        {
            var goods = excute.Context.ES_Goods.Find(new Guid(sGoodsId));
            goods.bIsActivity = goods.bIsActivity ? false : true;
            return excute.SaveChange();
        }


        /// <summary>
        /// 设置商品积分
        /// </summary>
        /// <param name="sGoodsId"></param>
        /// <param name="Integral"></param>
        /// <param name="IsRateAdd"></param>
        /// <returns></returns>
        public override int SetIntegral(string sGoodsId, int Integral, bool IsRateAdd)
        {
            var goods = excute.Context.ES_Goods.Find(new Guid(sGoodsId));
            goods.iIntegral = Integral;
            goods.IsRateAdd = IsRateAdd;
            return excute.SaveChange();
        }


        /// <summary>
        /// 设置商品的优惠价格
        /// </summary>
        /// <param name="sGoodsId"></param>
        /// <param name="DisPrices"></param>
        /// <param name="IsRateGiving"></param>
        /// <returns></returns>
        public override int SetDisPrices(string sGoodsId, decimal DisPrices, bool IsRateGiving)
        {
            var goods = excute.Context.ES_Goods.Find(new Guid(sGoodsId));
            goods.sDisPrices = DisPrices;
            goods.IsRateGiving = IsRateGiving;
            return excute.SaveChange();
        }
    }
}
