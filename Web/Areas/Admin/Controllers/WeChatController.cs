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
    ///微信公众号控制器
    /// </summary>
    public class WeChatController : AdminBase<IWeChat>
    {
        //
        // GET: /Admin/WeChat/

        public ActionResult Index()
        {
            var wechat = manage.GetWeChat();
            wechat = wechat == null ? new ES_WeChat() : wechat;
            return View(wechat);
        }


        /// <summary>
        /// 设置公众号信息
        /// </summary>
        /// <param name="wechat"></param>
        /// <returns></returns>
        public void SetWeChat(ES_WeChat wechat)
        {
            if (manage.SetWeChat(wechat) > 0)
                result.success = true;
        }
    }
}
