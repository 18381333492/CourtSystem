
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start.BaseController;
using EFModels.MyModels;
using LogicHandlerInterface;

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
        /// 分页获取数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo pageInfo, string searchText)
        {
            return Content(manage.List(pageInfo, searchText));
        }
    }
}
