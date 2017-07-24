using LogicHandlerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start.BaseController;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 订单统计控制器
    /// </summary>
    public class OrdersCountController : AdminBase<IOrders>
    {
        //
        // GET: /Admin/OrdersCount/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 获取订单图表统计数据
        /// </summary>
        /// <param name="iType">比较类型</param>
        /// <param name="iCompareType">统计范畴</param>
        /// <param name="dBookTime">交易时间</param>
        /// <param name="dCompareTime">参考时间</param>
        /// <param name="iOrderType">订单类型</param>
        /// <param name="iChannel">渠道</param>
        public void SearchCount(int iType,int iCompareType,string dBookTime,string dCompareTime,int iOrderType,int iChannel)
        {

            if (string.IsNullOrEmpty(dCompareTime))
            {
                var res = manage.SearchCount(iType, iCompareType, dBookTime, iOrderType, iChannel);
                result.success = true;
                result.data = new
                {
                    iType= iType,
                    bookData = InstallData(res, iCompareType, dBookTime),
                    compData = new List<object>()
                };
            }
            else
            {
                var res = manage.SearchCount(iType, iCompareType, dBookTime, iOrderType, iChannel);
                var res_data = manage.SearchCount(iType, iCompareType, dCompareTime, iOrderType, iChannel);
                result.success = true;
                result.data = new
                {
                    iType = iType,
                    bookData = InstallData(res, iCompareType, dBookTime),
                    compData = InstallData(res, iCompareType, dBookTime)
                };
            }
        }

        /// <summary>
        /// 组装数据
        /// </summary>
        /// <returns></returns>
        private object InstallData(List<Dictionary<string, object>> res, int iCompareType,string dBookTime)
        {
            List<decimal> data_list = new List<decimal>();
            if (res != null)
            {
                var m_state = true;
                switch (iCompareType)
                {
                    case 1:
                        for (var i = 1; i <= 12; i++)
                        {
                            m_state = true;
                            for (var j = 1; j <= res.Count; j++)
                            {
                                var da_time = DateTime.Parse(res[j - 1]["date"].ToString());
                                if (da_time.Month == i)
                                {
                                    data_list.Add(decimal.Parse(res[j - 1]["value"].ToString()));
                                    m_state = false;
                                    break;
                                }
                            }
                            if (m_state)
                            {
                                data_list.Add(0);
                            }
                        }
                        break;
                    case 2:
                        var date = DateTime.Parse(dBookTime);
                        var days = DateTime.DaysInMonth(date.Year, date.Month);
                        for (var i = 1; i <= days; i++)
                        {
                            m_state = true;
                            for (var j = 1; j <= res.Count; j++)
                            {
                                var da_time = DateTime.Parse(res[j - 1]["date"].ToString());
                                if (da_time.Day == i)
                                {
                                    data_list.Add(decimal.Parse(res[j - 1]["value"].ToString()));
                                    m_state = false;
                                    break;
                                }
                            }
                            if (m_state)
                            {
                                data_list.Add(0);
                            }
                        }
                        break;
                    case 3:
                        for (var i = 0; i <= 23; i++)
                        {
                            m_state = true;
                            for (var j = 0; j < res.Count; j++)
                            {
                                var da_time = DateTime.Parse(res[j]["date"].ToString());
                                if (da_time.Hour == i)
                                {
                                    data_list.Add(decimal.Parse(res[j]["value"].ToString()));
                                    m_state = false;
                                    break;
                                }
                            }
                            if (m_state)
                            {
                                data_list.Add(0);
                            }
                        }
                        break;
                }
            }
            return data_list;
        }
            
    }
}
