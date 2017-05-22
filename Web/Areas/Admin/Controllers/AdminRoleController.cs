using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFModels.MyModels;
using Web.App_Start.BaseController;
using SystemInterface;

namespace Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 角色管理控制器
    /// </summary>
    public class AdminRoleController : AdminBase<IAdminRole>
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
        /// 分页获取角色数据列表
        /// </summary>
        /// <param name="info"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo info,string searchText)
        {
            return Content(manage.PageList(info, searchText));
        }
    }
}
