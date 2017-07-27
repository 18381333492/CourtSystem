using EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicHandlerInterface
{
    /// <summary>
    /// 商品规格相关接口
    /// </summary>
    public abstract class IGoodsStandard:BaseBll
    {
        #region 查询

        /// <summary>
        /// 获取所有的商品规格
        /// </summary>
        /// <param name="sGoodsId"></param>
        /// <returns></returns>
        public abstract List<ES_GoodsStandard> GetList(string sGoodsId);

        #endregion


        #region 操作


        /// <summary>
        /// 设置商品规格
        /// </summary>
        /// <param name="list"></param>
        public abstract int SetStandard(List<ES_GoodsStandard> list);

        #endregion
    }
}
