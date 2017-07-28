using LogicHandlerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels.MyModels;

namespace LogicHandlerModel
{
    /// <summary>
    /// 商品评价的读操作
    /// </summary>
    public partial class GoodsCommentServices: IGoodsComment
    {
        /// <summary>
        /// 分页获取商品评价数据
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public override string List(PageInfo pageInfo, string searchText)
        {
            var list = query.PageQuery<Dictionary<string, object>>(@"select a.*,b.sHeadPicture,b.sNickName from ES_GoodsComment as a 
                                                                               left join ES_Client as b on a.sOpenId=b.sOpenId", pageInfo);
            return list.toJson();
        }
    }
}
