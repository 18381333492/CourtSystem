using EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using SystemInterface;
using Web.App_Start;
using Web.App_Start.BaseController;

namespace Web.Areas.Admin.Controllers
{
    public class ButtonController : AdminBase<IButton>
    {
        //
        // GET: /Admin/Button/

        #region 按钮相关视图

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(string sButtonId)
        {
            return View(manage.GetById(sButtonId));
        }

        #endregion


        /// <summary>
        /// 获取按钮数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return Content(manage.GetList());
        }

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public void Insert(ES_Button button)
        {
            if (manage.Insert(button) > 0)
                result.success = true;
        }


        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public void Update(ES_Button button)
        {
            if (manage.Update(button) > 0)
                result.success = true;
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public void Cancel(string sButtonId)
        {
            if (manage.Delete(sButtonId) > 0)
                result.success = true;
        }
    }
}
