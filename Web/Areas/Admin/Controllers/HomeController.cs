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
using System.IO;

namespace Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController<UserService>
    {
        //
        // GET: /Admin/Home/

        public ActionResult Index()
        {
            ViewBag.sUserName = SessionUser().sUserName;
            ViewBag.sRoleName = SessionUser().sRoleName;
            ViewBag.Online =HttpContext.Application["Online"];
            ViewBag.Visits = HttpContext.Application["Visits"];
            return View();
        }
        public ActionResult My404()
        {
            return View();
        }

        /// <summary>
        /// 登录过期提示页面
        /// </summary>
        /// <returns></returns>
        [NoLogin]
        public ActionResult Tip()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [NoLogin]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 后台用户登录
        /// </summary>
        /// <param name="sUserName"></param>
        /// <param name="sPassWord"></param>
        /// <returns></returns>
        [NoLogin]
        public void CheckLogin(string sUserName, string sPassWord,string sImgCode)
        {
            if (sImgCode == Session[SESSION.ImgCode].ToString())
            {
                string sRoleName;
                var user = _server.Login(sUserName, sPassWord, out sRoleName);
                if (user != null)
                {
                    if (user.bState)
                    {
                        Session[SESSION.User] = new UserInfo()
                        {
                            ID = user.ID,
                            sUserName = user.sUserName,
                            sRoleId = user.sRoleID,
                            sRoleName = sRoleName,
                            Ip = Request.UserHostAddress
                        };

                        //缓存用户的二级菜单和按钮
                        var menu = Resolve<MenusService>();
                        var button = Resolve<ButtonService>();
                        Session[SESSION.Menu] = menu.GetSecondMenus(user.sRoleID);
                        Session[SESSION.Button] = button.GetButton(user.sRoleID);
                        result.success = true;
                    }
                    else result.info = "该用户已被冻结,请联系管理员!";
                }
                else
                    result.info = "用户名或密码错误!";
            }
            else
            {
                result.info = "验证码错误!";
            }
        }


        /// <summary>
        /// 安全退出
        /// </summary>
        /// <returns></returns>
        public void Quit()
        {
            Session.RemoveAll();
            Session.Abandon();//清除全部Session
            result.success = true;
        }

        /// <summary>
        /// 获取图形验证码
        /// </summary>
        /// <returns></returns>
        [NoLogin]
        public ActionResult GetImgCode()
        {
            string sCode = C_ImgCode.CreateValidateCode(5);
            var code=C_ImgCode.CreateValidateGraphic(sCode);
            Session[SESSION.ImgCode] = sCode;
            return File(code, "@image/jpeg");
        }


        public FileResult file()
        {
            FileStream fs = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + "Content\\57521.mp4", FileMode.Open);

            return File(fs,"video/mpeg4");
        }
    }
}
