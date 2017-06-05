using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Web.App_Start;
using EFModels.MyModels;
using System.IO;
using Web.App_Start.BaseController;
using SystemInterface;
using EFModels;

namespace Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBase<IAdminUser>
    {
        //
        // GET: /Admin/Home/

        #region 后台首页相关视图


        public ActionResult Index()
        {
            var manageWebSite = Resolve<IWebSite>();
            var webSite = manageWebSite.GetWebSite();
            if (webSite == null)
            {
                ViewBag.ICON = string.Empty;
                ViewBag.sWebSiteName = string.Empty;
            }
            else
            {
                ViewBag.ICON = webSite.sWebSiteIcon;
                ViewBag.sWebSiteName = webSite.sWebSiteName;
            }
            return View(Session[SESSION.AdminUser]);
        }


        /// <summary>
        /// 获取缓存菜单数据列表
        /// </summary>
        public void MenuList()
        {
            var childList = Session[SESSION.Menu] as List<CDELINK_Menu>;//获取缓存的二级菜单
            var Ids = childList.Select(m => { return "'" + m.sParentMenuId.ToString() + "'"; });
            var manageMenu = Resolve<IMenu>();
            var mainList = manageMenu.GetMainMenuByIds(string.Join(",", Ids));
            result.data = new
            {
                mainList = mainList,//一级菜单
                childList = childList//二级菜单
            };
            result.success = true;
        }

        #endregion
     
    }
}
