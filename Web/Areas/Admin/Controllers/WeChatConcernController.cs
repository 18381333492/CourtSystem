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
using Newtonsoft.Json;

namespace Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 微信关注回复管理
    /// </summary>
    public class WeChatConcernController : AdminBase<IWeChatConcern>
    {
        //
        // GET: /Admin/WeChatConcern/
        public ActionResult Index()
        {
            var m=manage.Get();
            if (m == null) m = new CDELINK_WeChatConcern();
            return View(m);
        }     

        /// <summary>
        /// 修改关注回复设置
        /// </summary>
        /// <param name="Concern"></param>
        public void Keep(CDELINK_WeChatConcern Concern)
        {
            if (manage.KeepWeChatConcern(Concern) > 0)
            {
                result.success = true;
            }
        }
    }
}
