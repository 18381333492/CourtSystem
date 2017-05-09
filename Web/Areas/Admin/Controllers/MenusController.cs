using EFModel;
using Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start;

namespace Web.Areas.Admin.Controllers
{

    /// <summary>
    /// 菜单控制器
    /// </summary>
    public class MenusController : AdminBaseController<MenusService>
    {
        //
        // GET: /Admin/Menus/

        #region 菜单相关视图

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
            return View(_server.GetById(ID));
        }

        #endregion

        /// <summary>
        /// 获取菜单列表数据
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            string data = _server.GetList();
            return Content(data);
        }

        /// <summary>
        /// 获取菜单栏目
        /// </summary>
        /// <returns></returns>
        public void GetMenusList()
        {
            result.data = _server.GetMenusList(SessionUser().sRoleId);
            result.success = true;
        }

        /// <summary>
        /// 获取一级菜单栏目
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFirstMenus()
        {
            return Content(_server.GetFirstMenus());
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public void  Insert(Menus menu)
        {
            if (!_server.Check(menu))
            {
                int res = _server.Add(menu);
                if (res > 0)
                    result.success = true;
            }
            else
            {
                result.success = false;
                result.info = "菜单名称或者路径重复!";
            }
        }

        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public void Update(Menus menu)
        {
            if (!_server.Check(menu,true))
            {
                int res = _server.Edit(menu);
                if (res > 0)
                    result.success = true;
            }
            else
            {
                result.success = false;
                result.info = "菜单名称或者路径重复!";
            }
        }


        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public void Cancel(string Ids)
        {
            int res = _server.Cancel(Ids);
            if (res > 0)
                result.success = true;
        }
    }
}
