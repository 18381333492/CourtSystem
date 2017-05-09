using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Sevices;
using Web.App_Start;
using EFModel.MyModels;
using EFModel;

namespace Web.Areas.Admin.Controllers
{
    public class RoleController : AdminBaseController<RoleService>
    {
        //
        // GET: /Admin/Home/

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
            return View(_server.Get(ID));
        }
        /// <summary>
        /// 权限分配视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Power(Guid ID)
        {
            ViewBag.ID = ID;
            return View();
        }

        /// <summary>
        /// 分页获取角色数据列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo info)
        {
            return Content(_server.GetList(info,null));
        }


        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        public void GetPowerList(Guid ID)
        {
            result.data = _server.GetAllMenuAndButton(ID);
            result.success = true;
        }

        /// <summary>
        /// 获取所有的角色名称数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRoleNameList()
        {
            return Content(_server.GetRoleNameList());
        }


        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void Insert(Role role)
        {
            int res = _server.Add(role);
            if (res > 0)
            {
                result.success = true;
            }
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void Update(Role role)
        {
            int res = _server.Edit(role);
            if (res > 0)
            {
                result.success = true;
            }
        }

        /// <summary>
        /// 逻辑删除角色
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public void Cancel(string Ids)
        {
            int res = _server.Cancel(Ids);
            if (res > 0)
            {
                result.success = true;
            }
            if(res==-1)
            {
                result.info = "该角色下面绑定有用户,不能删除该角色!";
            }
        }

        
        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="sPower"></param>
        /// <returns></returns>
        public void SetPower(Guid ID,string sPower)
        {
            int res = _server.SetPower(ID, sPower);
            if (res > 0)
            {
                result.success = true;
            }
        }
             
    }
}
