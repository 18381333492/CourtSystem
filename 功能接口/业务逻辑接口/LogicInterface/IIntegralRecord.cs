using EFModels;
using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicHandlerInterface
{
    /// <summary>
    /// 积分记录接口
    /// </summary>
    public abstract class IIntegralRecord:BaseBll
    {
        #region 查询

        /// <summary>
        /// 分页获取会员积分记录
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="sOpenId"></param>
        /// <returns></returns>
        public abstract string List(PageInfo pageInfo, string sOpenId);

        #endregion


        #region 操作

        /// <summary>
        /// 添加积分记录
        /// </summary>
        /// <param name="integralRecord"></param>
        /// <returns></returns>
        public abstract int Insert(ES_IntegralRecord integralRecord);

        #endregion
    }
}
