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
    public partial class GoodsCommentService : ServiceBase
    {

        /// <summary>
        /// 分页获取商品评论数据
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
            sSql.Append("SELECT A.*,B.sGoodsName FROM [GoodsComment] AS A LEFT JOIN [Goods] AS B ON A.sGoodNo=B.sGoodNo WHERE 1=1");

            //条件查询
            if (!string.IsNullOrEmpty(searchText))
            {
                sSql.AppendFormat(" AND (A.sGoodNo LIKE '%{0}%' OR A.sCommentPerson LIKE '%{0}%' OR B.sGoodsName LIKE '%{0}%')", searchText);
            }
            if (!string.IsNullOrEmpty(Sta))
            {
                sSql.AppendFormat(" AND A.dCommentTime>='{0}'", Sta.toDateString(0));
            }
            if (!string.IsNullOrEmpty(End))
            {
                sSql.AppendFormat(" AND A.dCommentTime<='{0}'", End.toDateString(1));
            }
            Info.sort = "dCommentTime";
            Info.order = OrderType.DESC;
            return query.QueryPage(sSql.ToString(), Info, null);
        }


        /// <summary>
        /// 根据商品编号获取商品评论
        /// </summary>
        /// <param name="Info"></param>
        /// <param name="sGoodNo"></param>
        /// <returns></returns>
        public string GetListBysGoodNo(PageInfo Info, string sGoodNo)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.AppendFormat("SELECT * FROM [GoodsComment] WHERE sGoodNo='{0}'",sGoodNo);
            Info.sort = "dCommentTime";
            Info.order = OrderType.DESC;
            return query.QueryPage(sSql.ToString(), Info, null);
        }


        /// <summary>
        /// 根据商品评论主键ID获取商品评论
        /// </summary>
        /// <param name="sGoodsId"></param>
        /// <returns></returns>
        public GoodsComment Get(Guid sGoodsCommentId)
        {
            return query.db.GoodsComment.Find(sGoodsCommentId);
        }
    }
}
