using EFModel;
using EFModel.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Logs;

namespace Sevices
{
    public partial class OperateLogService : ServiceBase
    {


        /// <summary>
        /// 删除操作日志
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [Log("操作日志", Operate.删除)]
        public int Cancel(string Ids)
        {
            return excute.Excute(string.Format(@"DELETE OperateLog WHERE ID IN({0})", Ids), this, "Cancel");
        }
    }
}
