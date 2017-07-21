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
    /// 商品的读操作
    /// </summary>
    public partial class GoodsServices:IGoods
    {
      
        /// <summary>
        /// 分页获取商品数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public override string List(PageInfo pageInfo, string searchText, int bIsShelves, int iGoodsType)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append("select * from ES_Goods where 1=1");
            if (bIsShelves > -1)
            {//上下架查询
                sSql.AppendFormat(" and bIsShelves={0}", bIsShelves);
            }
            if (iGoodsType > -1)
            {//商品类型查询
                sSql.AppendFormat(" and iGoodsType={0}", iGoodsType);
            }
            if (!string.IsNullOrEmpty(searchText))
            {//模糊查询
                sSql.AppendFormat(" and (sGoodsName like '%{0}%' or sGoodsNo like '%{0}%')", searchText);
            }
            var res = query.PageQuery<Dictionary<string, object>>(sSql.ToString(), pageInfo, searchText);
            return res.toJson();
        }

        /// <summary>
        /// 根据商品主键ID获取商品
        /// </summary>
        /// <param name="sGoodsId"></param>
        /// <returns></returns>
        public override ES_Goods Get(string sGoodsId)
        {
            return query.Find<ES_Goods>(sGoodsId);
        }


        /// <summary>
        /// 根据商品编号获取商品
        /// </summary>
        /// <param name="sGoodsNo"></param>
        /// <returns></returns>
        public override ES_Goods GetByGoodsNo(string sGoodsNo)
        {
            return query.SingleQuery<ES_Goods>("select * from ES_Goods where sGoodsNo=@sGoodsNo", new { sGoodsNo = sGoodsNo });
        }


        /// <summary>
        /// 根据商品编号获取商品的信息
        /// </summary>
        /// <param name="sGoodsNo"></param>
        /// <returns></returns>
        public override ES_Goods GetGoodsInfoByGoodsNo(string sGoodsNo)
        {
            return query.SingleQuery<ES_Goods>("select sGoodsPrices,bIsShelves,bIsActivity,iCount from ES_Goods where sGoodsNo=@sGoodsNo", new { sGoodsNo = sGoodsNo });
        }


    }
}
