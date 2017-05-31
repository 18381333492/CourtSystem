using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFModels.MyModels;
using Web.App_Start.BaseController;
using SystemInterface;
using EFModels;

namespace Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 网站设置控制器
    /// </summary>
    public class WebSiteController : AdminBase<IAdminRole>
    {
        //
        // GET: /Admin/WebSite/

        public ActionResult Index()
        {
            return View();
        }
    }
}
