using LogicHandlerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start.BaseController;

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

    }
}
