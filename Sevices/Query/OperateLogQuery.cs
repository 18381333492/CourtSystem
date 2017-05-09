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
    public partial class OperateLogService : ServiceBase
    {

        /// <summary>
        /// 分页获取操作日志数据
        /// </summary>
        /// <param name="Info">分页参数</param>
        /// <param name="Params">查询参数</param>
        /// <returns></returns>
        public string GetList(PageInfo Info,Dictionary<string,object> Params)
        {
            Info.sort = "dInsertTime";
            return query.QueryPage(@"select * from [OperateLog]", Info, null);
        }
    }
}
