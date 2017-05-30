using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemInterface;
using Web.App_Start;
using Web.App_Start.BaseController;
using EFModels;

namespace Web.Areas.Admin.Controllers
{

    /// <summary>
    /// 超级管理员登陆入口
    /// </summary>
    public class SuperManController : AdminBase<ISuperMan>
    {
        //
        // GET: /Admin/SuperMan/

        /// <summary>
        /// 登陆入口
        /// </summary>
        /// <returns></returns>
        [NoLogin]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 超级管理员验证登陆
        /// </summary>
        /// <param name="sLoginAccout"></param>
        /// <param name="sPassWord"></param>
        [NoLogin]
        public void CheckLogin(string sLoginAccout, string sPassWord)
        {
            var adminUser = manage.CheckLogin(sLoginAccout, sPassWord);
            if (adminUser==null)
            {//该用户被冻结
                result.info = "用户名或密码错误";
            }
            else
            {
                //获取所有的二级菜单
                var manageMenu = Resolve<IMenu>();
                var manageButton = Resolve<IButton>();
                Session[SESSION.Menu] = manageMenu.GetAllChildMenu();//获取所有的二级菜单
                Session[SESSION.Button] = manageButton.GetAllButtonList();//获取所有的按钮
                Session[SESSION.AdminUser] = new UserInfo()
                {
                    sUserName = adminUser.sName,
                    iState = 1,//正常
                    ID = adminUser.ID,
                    bIsSuperMan = true //超级管理员
                };
                result.success = true;
            }
        }
    }
}
