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
    /// 角色管理控制器
    /// </summary>
    public class AdminRoleController : AdminBase<IAdminRole>
    {
        //
        // GET: /Admin/Role/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Add()
        {
            return View(SessionAdminUser());
        }

        public ActionResult Edit(string sAdminRoleId)
        {
            return View(manage.GetById(sAdminRoleId));
        }

        /// <summary>
        /// 权限分配页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Power(string sAdminRoleId)
        {
            return View(manage.GetById(sAdminRoleId));
        }

        /// <summary>
        /// 获取所有的菜单列表和按钮数据
        /// </summary>
        public void GetMenuList()
        {
            var userinfo = SessionAdminUser();
            if (userinfo.bIsSuperMan)
            {//超级管理员通道
                var manageMenu = Resolve<IMenu>();
                result.data = manageMenu.GetAllMenuList();
                result.success = true;
            }
            else
            {
                //var manageAdminUser = Resolve<IAdminUser>();
                //manageAdminUser.GetMenuAndButtonByRoleId(userinfo.);
                //result.success = true;
            }

        }


        /// <summary>
        /// 分页获取角色数据列表
        /// </summary>
        /// <param name="info"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo info,string searchText)
        {
            return Content(manage.PageList(info, searchText,SessionAdminUser().bIsSuperMan));
        }


        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="adminRole"></param>
        public void Insert(CDELINK_AdminRole adminRole)
        {
            if (!SessionAdminUser().bIsSuperMan) adminRole.IsShow = true;
            if (!manage.CheckRoleName(adminRole.sRoleName))
            {
                if (manage.Insert(adminRole) > 0)
                    result.success = true;
            }
            else result.info = string.Format("{0}角色名称已存在,请重新输入", adminRole.sRoleName);
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="adminRole"></param>
        public void Update(CDELINK_AdminRole adminRole)
        {
            if (!SessionAdminUser().bIsSuperMan) adminRole.IsShow = true;
            if (!manage.CheckRoleName(adminRole.sRoleName, adminRole.ID.ToString()))
            {
                if (manage.Update(adminRole) > 0)
                    result.success = true;
            }
            else result.info = string.Format("{0}角色名称已存在,请重新输入", adminRole.sRoleName);
        }

        /// <summary>
        /// 根据主键ID集合删除角色
        /// </summary>
        /// <param name="Ids"></param>
        public void Cancel(string Ids)
        {
            if(manage.Cancel(Ids)>0)
                result.success = true;
        }

        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <param name="sAdminRoleId"></param>
        /// <param name="sMenuIds"></param>
        /// <param name="sButtonIds"></param>
        public void SetPower(string sAdminRoleId,string sMenuIds,string sButtonIds)
        {
            if (manage.SetPower(sAdminRoleId, sMenuIds, sButtonIds) > 0)
                result.success = true;
        }
    }
}
