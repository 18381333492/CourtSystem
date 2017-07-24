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
    /// 订单的读操作实现
    /// </summary>
    public partial class OrdersServices : IOrders
    {

        /// <summary>
        /// 分页获取订单数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public override string List(PageInfo pageInfo, string searchText, string dStaTime, string dEndTime, int iState, int iOrderType, int iChannel)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append("select * from ES_Orders where 1=1");
            if (iState > -1)
                sSql.AppendFormat(" and iState={0}", iState);
            if (iOrderType > -1)
                sSql.AppendFormat(" and iOrderType={0}", iOrderType);
            if (iChannel > -1)
                sSql.AppendFormat(" and iChannel={0}", iChannel);
            if(!string.IsNullOrEmpty(dStaTime))
                sSql.AppendFormat(" and dBookTime>='{0}'", dStaTime);
            if (!string.IsNullOrEmpty(dEndTime))
                sSql.AppendFormat(" and dBookTime<='{0}'", dEndTime);
            if(!string.IsNullOrEmpty(searchText))
                sSql.AppendFormat(" and (sReceiver like '%{0}%' or sPhone like '%{0}%' or sGoodsNo like '%{0}%' or sOrderNo like '%{0}%')", searchText);
            var res = query.PageQuery<Dictionary<string, object>>(sSql.ToString(), pageInfo, searchText);
            return res.toJson();
        }


        /// <summary>
        /// 根据主键ID获取订单对象
        /// </summary>
        /// <param name="sOrderId"></param>
        /// <returns></returns>
        public override ES_Orders Get(string sOrderId)
        {
            return query.Find<ES_Orders>(sOrderId);
        }


        /// <summary>
        /// 获取订单图表统计数据
        /// </summary>
        /// <param name="iType">比较类型</param>
        /// <param name="iCompareType">统计范畴</param>
        /// <param name="dBookTime">交易时间</param>
        /// <param name="iOrderType">订单类型</param>
        /// <param name="iChannel">渠道</param>
        public override List<Dictionary<string,object>> SearchCount(int iType, int iCompareType, string dBookTime,int iOrderType, int iChannel)
        {
            StringBuilder sSql = new StringBuilder();
            if (iType == 1)
            {//统计订单量
                switch (iCompareType)
                {
                    case 1:
                        sSql.Append(@"select count(*) as value,LEFT(CONVERT(varchar(100), dBookTime, 20),7) as date");
                        break;
                    case 2:
                        sSql.Append(@"select count(*) as value,LEFT(CONVERT(varchar(100), dBookTime, 20),10) as date");
                        break;
                    case 3:
                        sSql.Append(@"select count(*) as value,LEFT(CONVERT(varchar(100), dBookTime, 20),13) as date");
                        break;
                }
            }
            else
            {//订单金额的统计
                switch (iCompareType)
                {
                    case 1:
                        sSql.Append(@"select sum(dPrices) as value,LEFT(CONVERT(varchar(100), dBookTime, 20),7) as date");
                        break;
                    case 2:
                        sSql.Append(@"select sum(dPrices) as value,LEFT(CONVERT(varchar(100), dBookTime, 20),10) as date");
                        break;
                    case 3:
                        sSql.Append(@"select sum(dPrices) as value,LEFT(CONVERT(varchar(100), dBookTime, 20),13) as date");
                        break;
                }
            }
            sSql.Append(" from ES_Orders where iState>0");
            if (iOrderType > -1)
                sSql.AppendFormat(" and iOrderType={0}", iOrderType);
            if (iChannel > -1)
                sSql.AppendFormat(" and iChannel={0}", iChannel);
            if (iCompareType == 1)
                sSql.Append(" group by LEFT(CONVERT(varchar(100), dBookTime, 20),7)");
            if (iCompareType == 2)
                sSql.Append(" group by LEFT(CONVERT(varchar(100), dBookTime, 20),10)");
            if (iCompareType == 3)
                sSql.Append(" group by LEFT(CONVERT(varchar(100), dBookTime, 20),13)");
            if (!string.IsNullOrEmpty(dBookTime))
            {
                var date = DateTime.Parse(dBookTime);
                string dateSta = string.Empty;
                string dateEnd = string.Empty;
                switch (iCompareType)
                {
                    case 1:
                        dateSta = date.ToString("yyyy") + "-01-01";
                        dateEnd = date.ToString("yyyy") + "-12-31";
                        break;
                    case 2:
                        var days = DateTime.DaysInMonth(date.Year, date.Month);//获取某月的天数
                        dateSta = date.ToString("yyyy-MM") + "-01";
                        dateEnd =string.Format("{0}-{1}",date.ToString("yyyy-MM"), days);
                        break;
                    case 3:
                        dateSta = date.ToString("yyyy-MM-dd") + " 00:00:00";
                        dateSta = date.ToString("yyyy-MM-dd") + " 23:59:59";
                        break;
                }
                var res = query.QueryList<Dictionary<string, object>>(sSql.ToString()).ToList();
                return res;
            }
            else
            {
                throw new ApplicationException("交易时间不能为空!");
            }
        }
    }
}
