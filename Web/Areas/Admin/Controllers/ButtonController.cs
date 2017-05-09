using EFModel;
using Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Web.App_Start;

namespace Web.Areas.Admin.Controllers
{
    public class ButtonController : AdminBaseController<ButtonService>
    {
        //
        // GET: /Admin/Button/

        #region 菜单按钮视图

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(Guid ID)
        {
            var button = _server.Get(ID) != null ? _server.Get(ID) : new Button();
            return View(button);
        }

        #endregion

        /// <summary>
        /// 获取菜单按钮数据
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            string res = _server.GetList();
            return Content(res);
        }

        /// <summary>
        /// 添加菜单下面的按钮
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public void Insert(Button button)
        {
            int res = _server.Add(button);
            if (res > 0)
                result.success = true;
        }


        /// <summary>
        /// 编辑菜单下面的按钮
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public void Update(Button button)
        {
            int res = _server.Edit(button);
            if (res > 0)
            {
                result.success = true;
            }
        }

        /// <summary>
        /// 删除菜单下面的按钮
        /// </summary>
        /// <param name="Ids"></param>
        public void  Cancel(string Ids)
        {
            int res = _server.Cancel(Ids);
            if (res > 0)
            {
                result.success = true;
            }
        }

    }
}
