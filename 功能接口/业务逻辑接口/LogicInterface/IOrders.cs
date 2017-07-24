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
    /// 订单业务逻辑接口
    /// </summary>
    public abstract class IOrders : BaseBll
    {
        #region 查询

        /// <summary>
        /// 分页获取订单数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="searchText"></param>
        /// <param name="dStaTime"></param>
        /// <param name="dEndTime"></param>
        /// <param name="iState"></param>
        /// <param name="iOrderType"></param>
        /// <param name="iChannel"></param>
        /// <returns></returns>
        public abstract string List(PageInfo pageInfo, string searchText, string dStaTime, string dEndTime, int iState, int iOrderType, int iChannel);

        /// <summary>
        /// 根据主键ID获取订单对象
        /// </summary>
        /// <param name="sOrderId"></param>
        /// <returns></returns>
        public abstract ES_Orders Get(string sOrderId);


        /// <summary>
        /// 获取订单图表统计数据
        /// </summary>
        /// <param name="iType">比较类型</param>
        /// <param name="iCompareType">统计范畴</param>
        /// <param name="dBookTime">交易时间</param>
        /// <param name="dCompareTime">参考时间</param>
        /// <param name="iOrderType">订单类型</param>
        /// <param name="iChannel">渠道</param>
        public abstract List<Dictionary<string, object>> SearchCount(int iType, int iCompareType, string dBookTime, int iOrderType, int iChannel);

        #endregion


        #region 操作

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="sOrdersId">订单主键ID或主键ID集合</param>
        public abstract int Confrim(string sOrdersId);


        /// <summary>
        /// 设置物流信息
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public abstract int SetLogistics(ES_Orders orders);


        /**********************************前端订单的相关操作***********************************/

        /// <summary>
        /// 预订下单
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public abstract int BookOrder(ES_Orders orders);

        #endregion
    }
}
