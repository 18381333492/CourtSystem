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
    /// 商品规格的读操作
    /// </summary>
    public partial class GoodsStandardServices: IGoodsStandard
    {
        /// <summary>
        /// 获取所有的商品规格
        /// </summary>
        /// <param name="sGoodsId"></param>
        /// <returns></returns>
        public override List<ES_GoodsStandard> GetList(string sGoodsId)
        {
            var list = query.QueryList<ES_GoodsStandard>("select * from ES_GoodsStandard where sGoodsId=@sGoodsId order by dInsertTime asc", new { sGoodsId = sGoodsId });
            return list.ToList();
        }
    }
}
