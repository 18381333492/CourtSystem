﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start.BaseController;
using EFModels;
using EFModels.MyModels;
using Web.App_Start;
using SystemInterface;
using Common;
using WeiXin.Tool;

namespace Web.Areas.Admin.Controllers
{

    public class AdminUserController : AdminBase<IAdminUser>
    {
        //
        // GET: /Admin/AdminUser/

        /// <summary>
        /// 后台管理员首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 个人中心页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PersonCenter()
        {
            var adminUser = new ES_AdminUser();
            var sessionUser = SessionAdminUser();
            adminUser = manage.GetById(sessionUser.ID.ToString());
            //根据角色ID获取角色名称
            var manageRole = Resolve<IAdminRole>();
            ViewBag.sRoleName = manageRole.GetById(adminUser.sRoleId).sRoleName;
            return View(adminUser);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="Info">分页参数</param>
        /// <param name="searchText">搜索的文本</param>
        /// <param name="iState">会员状态(默认全部)</param>
        /// <returns></returns>
        public ActionResult List(PageInfo pageInfo, string searchText, int iState = -1)
        {
            return Content(manage.PageList(pageInfo, searchText, iState));
        }

        /// <summary>
        /// 设置角色的页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SetRole(string ID)
        {
            ViewBag.ID = ID;
            return View(manage.GetAllRoleNameList());
        }

        /// <summary>
        /// 为后台用户设置角色
        /// </summary>
        /// <param name="ID">用户主键ID</param>
        /// <param name="sRoleId">角色Value</param>
        public void SetRoleValue(Guid ID,string sRoleId)
        {
            if (manage.SetRole(ID, sRoleId) > 0)
                result.success = true;
        }

        /// <summary>
        /// 根据主键ID集合删除后台用户
        /// </summary>
        /// <param name="Ids"></param>
        public void Cancel(string Ids)
        {
            if (manage.Cancel(Ids) > 0)
                result.success = true;
        }

        /// <summary>
        /// 根据主键ID冻结/解冻用户
        /// </summary>
        /// <param name="ID"></param>
        public void Freeze(string ID)
        {
            if (manage.Freeze(ID) > 0)
                result.success = true;
        }


        /// <summary>
        /// 微信扫码登录
        /// </summary>
        /// <returns></returns>
        [NoLogin]
        public ActionResult WeChatLogin(string sSendPriKey)
        {
            if (!Request.IsAjaxRequest())
            {
                var weChat = Resolve<IWeChat>().GetWeChat();
                string sUrl = string.Empty;
                var WeChatUserInfo = WeChatUserHelper.GetUserByAuthorize(weChat.sAppId, weChat.sAppSecret, out sUrl);
                if (!WeChatUserInfo.isSuccess)
                {
                    if (!string.IsNullOrEmpty(sUrl))
                        return Redirect(sUrl);
                    else
                        return View("~/Areas/Admin/Views/AdminUser/WeChatError.cshtml");
                }
                ViewBag.sSendPriKey = sSendPriKey;//需要发送消息WebSocket链接标识
                ViewBag.headimgurl = WeChatUserInfo.headimgurl;
                ViewBag.nickname = WeChatUserInfo.nickname;
                ViewBag.PORT = HttpContext.Application["WebScoket_Port"];
                ViewBag.DOMAIN = C_Config.ReadAppSetting("domain");
                Session["WeChatUserInfo_Login"] = WeChatUserInfo;
                return View();
            }
            else
            {//登录确认
                var WeChatUserInfo= Session["WeChatUserInfo_Login"] as WeChatUser;
                result.data = WeChatUserInfo.openid;
                result.success = true;
                return Content(result.toJson());
            }
        }

        /// <summary>
        /// 微信注册后台用户
        /// </summary>
        /// <returns></returns>
        [NoLogin]
        public ActionResult WeChatRegister()
        {
            if (!Request.IsAjaxRequest())
            {
                var weChat = Resolve<IWeChat>().GetWeChat();
                string sUrl = string.Empty;
                var WeChatUserInfo = WeChatUserHelper.GetUserByAuthorize(weChat.sAppId, weChat.sAppSecret, out sUrl);
                if (!WeChatUserInfo.isSuccess)
                {
                    if (!string.IsNullOrEmpty(sUrl))
                        return Redirect(sUrl);
                    else
                        return View("~/Areas/Admin/Views/AdminUser/WeChatError.cshtml");
                }
                if (!manage.IsBingWeChat(WeChatUserInfo.openid))
                {
                    Session["WeChatUserInfo_Register"] = WeChatUserInfo;
                    ViewBag.headimgurl = WeChatUserInfo.headimgurl;
                    ViewBag.nickname = WeChatUserInfo.nickname;
                    return View();
                }
                else
                {
                    return View("~/Areas/Admin/Views/AdminUser/WeChatError.cshtml",1);
                }
            }
            else
            {//POST 请求注册后台管理用户
                string sAcccount = Request.Form["sAcccount"];
                string sName = Request.Form["sName"];
                string sPhone = Request.Form["sPhone"];
                string sPassWord = Request.Form["sPassWord"];
                var WeChatUserInfo = Session["WeChatUserInfo_Register"] as WeChatUser;
                if (manage.Insert(new ES_AdminUser()
                {
                    sName = sName,//真实姓名
                    sPhone = sPhone,//手机号
                    iState = 0,//默认禁用
                    sLoginAccout = string.IsNullOrEmpty(sAcccount) ? string.Empty : sAcccount,
                    sPassWord = string.IsNullOrEmpty(sPassWord) ? string.Empty : C_Security.MD5(sPassWord),
                    sOpenId = WeChatUserInfo.openid,
                    sNick = WeChatUserInfo.nickname,
                    sHeadPicture = WeChatUserInfo.headimgurl,
                    sRoleId = string.Empty,//默认没有角色
                    dInsertTime = DateTime.Now,
                    bIsDeleted = false
                }) > 0)
                    result.success = true;
                else
                    result.info = "绑定失败!";
            }
            return Content(result.toJson());
        }

        /// <summary>
        /// 账号登录
        /// </summary>
        /// <returns></returns>
        [NoLogin]
        public ActionResult Login()
        {
            var manageWebSite = Resolve<IWebSite>();
            var webSite = manageWebSite.GetWebSite();
            if (webSite != null)
                ViewBag.ICON = webSite.sWebSiteIcon;
            ViewBag.PORT = HttpContext.Application["WebScoket_Port"];
            ViewBag.DOMAIN = C_Config.ReadAppSetting("domain");
            return View();
        }

        /// <summary>
        /// 后台用户账户登录
        /// </summary>
        /// <param name="sLoginAccout"></param>
        /// <param name="sPassWord"></param>
        [NoLogin]
        public void LoginCheck(string sLoginAccout, string sPassWord, string sCode)
        {
            result = LoginServices.AccountLogin(sLoginAccout, sPassWord, sCode);
        }


        /// <summary>
        /// 扫码验证登录
        /// </summary>
        /// <param name="key">扫码用户的openId</param>
        [NoLogin]
        public void ScanValiateLogin(string key)
        {
            result = LoginServices.ScanValiateLogin(key);
        }


        /// <summary>
        /// 生成图片验证码
        /// </summary>
        /// <returns></returns>
        [NoLogin]
        public FileResult MakePictureCode()
        {
            string sCode = C_ImgCode.CreateValidateCode(5);
            var code = C_ImgCode.CreateValidateGraphic(sCode);
            Session[SESSION.ImgCode] = sCode;
            return File(code, "@image/jpeg");
        }

        /// <summary>
        /// 安全退出
        /// </summary>
        public void Quit()
        {
            Session.Remove(SESSION.AdminUser);
            Session.Remove(SESSION.Menu);
            Session.Remove(SESSION.Button);
            result.success = true;
        }
        
    }
}
