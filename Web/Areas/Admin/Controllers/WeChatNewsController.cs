using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFModels.MyModels;
using Web.App_Start.BaseController;
using SystemInterface;
using EFModels;
using WeiXin.Tool;

namespace Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 微信图文信息素材管理
    /// </summary>
    public class WeChatNewsController : AdminBase<IWeChatNews>
    {
        //
        // GET: /Admin/WeChatNews/
        public ActionResult Index()
        {
            return View();
        }
    }
}
