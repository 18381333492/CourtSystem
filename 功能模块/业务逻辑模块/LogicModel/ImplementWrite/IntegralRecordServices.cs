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
    /// 积分记录的写操作
    /// </summary>
    public partial class IntegralRecordServices: IIntegralRecord
    {

        /// <summary>
        /// 添加积分记录
        /// </summary>
        /// <param name="integralRecord"></param>
        /// <returns></returns>
        public override int Insert(ES_IntegralRecord integralRecord)
        {
            integralRecord.ID = Guid.NewGuid();
            integralRecord.dInsertTime = DateTime.Now;
            excute.Add<ES_IntegralRecord>(integralRecord);
            return excute.SaveChange();
        }
    }
}
