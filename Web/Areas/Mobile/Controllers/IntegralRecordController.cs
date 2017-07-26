using EFModels.MyModels;
using LogicHandlerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start.BaseController;

namespace Web.Areas.Mobile.Controllers
{
    /// <summary>
    /// 积分记录控制器
    /// </summary>
    public class IntegralRecordController : MobileBase<IIntegralRecord>
    {
        //
        // GET: /Mobile/IntegralRecord/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 分页获取会员积分记录
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="sOpenId"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo pageInfo,string sOpenId)
        {
            return Content(manage.List(pageInfo, sOpenId));
        }
    }
}
