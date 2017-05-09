using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.WeiXin.Controllers
{
    /// <summary>
    /// 公众号自定义菜单控制器
    /// </summary>
    public class AutoMenuController : Controller
    {
        //
        // GET: /WeiXin/AutoMenu/

        public ActionResult Index()
        {
            return View();
        }

    }
}
