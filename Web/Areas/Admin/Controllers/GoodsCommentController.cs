using EFModels.MyModels;
using LogicHandlerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start.BaseController;

namespace Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 商品评价控制器
    /// </summary>
    public class GoodsCommentController : AdminBase<IGoodsComment>
    {
        //
        // GET: /Admin/GoodsComment/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 分页获取商品评论数据
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo pageInfo,string searchText)
        {
            return Content(manage.List(pageInfo, searchText));
        }
    }
}
