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
    /// 商品规格的写操作
    /// </summary>
    public partial class GoodsStandardServices: IGoodsStandard
    {
        /// <summary>
        /// 设置商品规格
        /// </summary>
        /// <param name="list"></param>
        public override int SetStandard(List<ES_GoodsStandard> list)
        {
            var sGoodsId = list[0].sGoodsId;
            var oldlist = excute.Context.ES_GoodsStandard.AsNoTracking().Where(m => m.sGoodsId== sGoodsId).ToList();
            foreach(var m in oldlist)
            {
                excute.Delete<ES_GoodsStandard>(m);
            }
            foreach (var item in list)
            {
                item.ID = Guid.NewGuid();
                item.dInsertTime = DateTime.Now;
                excute.Add<ES_GoodsStandard>(item);
            }
            return excute.SaveChange();

        }
    }
}
