using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFModels.MyModels;

namespace Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 角色管理控制器
    /// </summary>
    public class RoleController : Controller
    {
        //
        // GET: /Admin/Role/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult List(PageInfo info,string searchText)
        {
            return Content(string.Empty);
        }
    }
}
