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
    /// 积分记录的读操作
    /// </summary>
    public partial class IntegralRecordServices: IIntegralRecord
    {

        /// <summary>
        /// 分页获取会员积分记录
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="sOpenId"></param>
        /// <returns></returns>
        public override string List(PageInfo pageInfo, string sOpenId)
        {
            var res = query.PageQuery<ES_IntegralRecord>(@"select * from ES_IntegralRecord where sOpenId=@sOpenId", pageInfo, new { sOpenId = sOpenId });
            return res.toJson();
        }
    }
}
