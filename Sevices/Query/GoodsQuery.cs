using EFModel;
using EFModel.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Sevices
{
    public partial class GoodsService : ServiceBase
    {

        /// <summary>
        /// 分页获取商品数据
        /// </summary>
        /// <param name="Info">分页参数</param>
        /// <param name="Sta">开始时间</param>
        /// <param name="End">结束时间</param>
        /// <param name="searchText"></param>
        /// <param name="iState"></param>
        /// <returns></returns>
        public string GetList(PageInfo Info, string Sta, string End, string searchText)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append("SELECT * FROM [Goods] WHERE 1=1");

            //条件查询
            if (!string.IsNullOrEmpty(searchText))
            {
                sSql.AppendFormat(" AND (sGoodsName LIKE '%{0}%' OR sGoodNo LIKE '%{0}%')", searchText);
            }
            if (!string.IsNullOrEmpty(Sta))
            {
                sSql.AppendFormat(" AND dInsertTime>='{0}'", Sta.toDateString(0));
            }
            if (!string.IsNullOrEmpty(End))
            {
                sSql.AppendFormat(" AND dInsertTime<='{0}'", End.toDateString(1));
            }
            Info.sort = "dInsertTime";
            Info.order = OrderType.DESC;
            return query.QueryPage(sSql.ToString(), Info, null);
        }


        /// <summary>
        /// 根据商品主键ID获取商品实体
        /// </summary>
        /// <param name="sGoodsId"></param>
        /// <returns></returns>
        public Goods Get(Guid sGoodsId)
        {
            return query.db.Goods.Find(sGoodsId);
        }

        /// <summary>
        /// 根据商品编号查询是否该商品是否存在
        /// </summary>
        /// <returns></returns>
        public bool IsExistGoods(string sGoodNo)
        {
            return query.db.Goods.Any(m => m.sGoodNo == sGoodNo);
        }
    }
}
