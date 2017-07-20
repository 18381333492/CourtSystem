
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start.BaseController;
using EFModels.MyModels;
using LogicHandlerInterface;
using EFModels;

namespace Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 订单控制器
    /// </summary>
    public class OrdersController :AdminBase<IOrders>
    {
        //
        // GET: /Admin/Orders/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 详情查看
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(string sOrderId)
        {
            return View(manage.Get(sOrderId));
        }

        /// <summary>
        /// 添加物流视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Logistics(string sOrderId)
        {
            return View(manage.Get(sOrderId));
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo pageInfo, string searchText,string dStaTime,string dEndTime, int iState=-1,int iOrderType=-1,int iChannel=-1)
        {
            return Content(manage.List(pageInfo, searchText, dStaTime, dEndTime, iState, iOrderType, iChannel));
        }

      
        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="sOrdersId">订单主键ID或主键ID集合</param>
        public void Confrim(string sOrdersId)
        {
            if (manage.Confrim(sOrdersId) > 0)
                result.success = true;
        }

        /// <summary>
        /// 设置物流
        /// </summary>
        /// <param name=""></param>
        public void SetLogistics(ES_Orders orders)
        {
            if (manage.SetLogistics(orders) > 0)
                result.success = true;
        }
    }
}
