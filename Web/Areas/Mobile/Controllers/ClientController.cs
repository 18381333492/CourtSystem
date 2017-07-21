using LogicHandlerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start.BaseController;

namespace Web.Areas.Mobile.Controllers
{
    public class ClientController :MobileBase<IClient>
    {
        //
        // GET: /Mobile/Client/

        /// <summary>
        /// 个人中心页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Center()
        {
            return View();
        }

    }
}
