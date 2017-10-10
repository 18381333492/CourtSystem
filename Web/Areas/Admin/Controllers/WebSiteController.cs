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
    public class WebSiteController : AdminBase<IWebSite>
    {
        //
        // GET: /Admin/WebSite/

        public ActionResult Index()
        {
            var webSite = manage.GetWebSite();
            webSite = webSite == null ? new ES_WebSite() : webSite;
            return View(webSite);
        }


        /// <summary>
        /// 设置网站信息
        /// </summary>
        /// <param name="website"></param>
        /// <returns></returns>
        public void SetWebSite(ES_WebSite website)
        {
            if (manage.SetWebSite(website) > 0)
                result.success = true;
        }
    }
}
