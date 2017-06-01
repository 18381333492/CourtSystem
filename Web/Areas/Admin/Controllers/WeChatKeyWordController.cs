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
    ///微信关键字控制器
    /// </summary>
    public class WeChatKeyWordController : AdminBase<IWeChat>
    {
        //
        // GET: /Admin/WeChatKeyWord/

        public ActionResult Index()
        {
            return View();
        }




    }
}
