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
using Common;

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
            var manageWebSite = Resolve<IWebSite>();
            var webSite = manageWebSite.GetWebSite();
            ViewBag.PORT = HttpContext.Application["WebScoket_Port"];
            ViewBag.DOMAIN = C_Config.ReadAppSetting("domain");
            return View(webSite);
        }

        /// <summary>
        /// 超级管理员验证登陆
        /// </summary>
        /// <param name="sLoginAccout"></param>
        /// <param name="sPassWord"></param>
        [NoLogin]
        public void CheckLogin(string key)
        {
            var adminUser = manage.CheckLogin(key);
            if (adminUser==null)
            {
                result.info = "你还不是管理员!";
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
                    sUserName = adminUser["sName"].ToString(),
                    iState = 1,//正常
                    sHeadPic = adminUser["sHeadPicture"].ToString(),
                    ID =new Guid(adminUser["ID"].ToString()),
                    bIsSuperMan = true //超级管理员
                };
                result.success = true;
            }
        }
    }
}
