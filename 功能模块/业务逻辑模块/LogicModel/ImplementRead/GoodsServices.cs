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
        public override string List(PageInfo pageInfo, string searchText)
        {
            var res = query.PageQuery<Dictionary<string, object>>("select * from ES_Goods", pageInfo, searchText);
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
    }
}
