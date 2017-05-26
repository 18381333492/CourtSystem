using EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemInterface;
using Web.App_Start.BaseController;

namespace Web.Areas.Admin.Controllers
{
    public class MenuController : AdminBase<IMenu>
    {
        //
        // GET: /Admin/Menu/

        #region 菜单相关视图
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View(manage.GetMianMenuList());
        }

        public ActionResult Edit(string sMenuId)
        {
            ViewBag.MianMenuList = manage.GetMianMenuList();
            return View(manage.GetById(sMenuId));
        }
        #endregion 

        /// <summary>
        /// 获取菜单的所有数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return Content(manage.List());
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menu"></param>
        public void Insert(CDELINK_Menu menu)
        {
            if (!manage.CheckMenuName(menu.sMenuName))
            {
                if (manage.Insert(menu) > 0)
                    result.success = true;
            }
            else
                result.info = string.Format("{0}菜单名称已存在,请重新输入", menu.sMenuName);
        }


        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="menu"></param>
        public void Update(CDELINK_Menu menu)
        {
            if (!manage.CheckMenuName(menu.sMenuName,menu.ID.ToString()))
            {
                if (manage.Update(menu) > 0)
                result.success = true;
            }
            else
                result.info = string.Format("{0}菜单名称已存在,请重新输入", menu.sMenuName);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="Ids"></param>
        public void Cancel(string Ids)
        {
            if (manage.Cancel(Ids) > 0)
                result.success = true;
        }

    }
}
