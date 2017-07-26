using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Mobile.Controllers
{
    /// <summary>
    /// 商品评价控制器
    /// </summary>
    public class GoodsCommentController : Controller
    {
        //
        // GET: /Mobile/GoodsComment/

        public ActionResult Index()
        {
            return View();
        }

    }
}
