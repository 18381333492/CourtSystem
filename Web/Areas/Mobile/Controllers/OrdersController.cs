using EFModels;
using LogicHandlerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start.BaseController;

namespace Web.Areas.Mobile.Controllers
{
    /// <summary>
    /// 前端页面的订单相关的控制器
    /// </summary>
    public class OrdersController : MobileBase<IOrders>
    {
        //
        // GET: /Mobile/Orders/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 预订下单
        /// </summary>
        /// <param name="orders"></param>
        public void BookOrder(ES_Orders orders)
        {
            orders.sIp = Request.UserHostAddress;
            if (manage.BookOrder(orders) > 0)
            {//下单成功跳转支付

            }
            result.success = true;
        }



    }
}
