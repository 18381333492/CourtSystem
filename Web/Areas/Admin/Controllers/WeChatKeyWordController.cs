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
    public class WeChatKeyWordController : AdminBase<IWeChatKeyWord>
    {
        //
        // GET: /Admin/WeChatKeyWord/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(string sWeChatKeyWordId)
        {
            var model = manage.GetById(sWeChatKeyWordId);
            return View(model);
        }

        /// <summary>
        /// 分页获取微信关键字数据列表
        /// </summary>
        /// <param name="info"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo info, string searchText)
        {
            return Content(manage.PageList(info, searchText));
        }

        /// <summary>
        /// 添加微信关键字
        /// </summary>
        /// <param name="keyWord"></param>
        [ValidateInput(false)]

        public void Insert(CDELINK_WeChatKeyWord keyWord)
        {
            if (manage.Insert(keyWord) > 0)
                result.success = true;
        }

        /// <summary>
        /// 添加编辑关键字
        /// </summary>
        /// <param name="keyWord"></param>
        [ValidateInput(false)]

        public void Update(CDELINK_WeChatKeyWord keyWord)
        {
            if (manage.Update(keyWord) > 0)
                result.success = true;
        }
    }
}
