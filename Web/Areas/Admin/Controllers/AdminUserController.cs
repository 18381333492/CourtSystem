using Sevices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start.BaseController;

namespace Web.Areas.Admin.Controllers
{

    public class AdminUserController : AdminBase<IAdminUser>
    {
        //
        // GET: /Admin/AdminUser/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 后台用户登录
        /// </summary>
        /// <param name="sLoginAccout"></param>
        /// <param name="sPassWord"></param>
        public void  Login(string sLoginAccout,string sPassWord)
        {
            var adminUser=manage.ValidateLogin(sLoginAccout, sPassWord);
            if (adminUser.iState == 0)
            {//该用户被冻结
                result.info = "该账户已被冻结,请联系管理员";
            }
            else
            {
                result.success = true;
            }
        }


    }
}
