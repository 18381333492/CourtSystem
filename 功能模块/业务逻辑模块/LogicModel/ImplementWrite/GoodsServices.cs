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

        public override int Shelves(string sGoodsId)
        {
            throw new NotImplementedException();
        }


        public override int SetIntegral(string sGoodsId, int Integral, bool IsRateAdd)
        {
            throw new NotImplementedException();
        }


        public override int SetDisPrices(string sGoodsId, decimal DisPrices, bool IsRateGiving)
        {
            throw new NotImplementedException();
        }
    }
}
