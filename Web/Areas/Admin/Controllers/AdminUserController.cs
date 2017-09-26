
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

namespace Web.Areas.Admin.Controllers
{

    public class AdminUserController : AdminBase<IAdminUser>
    {
        //
        // GET: /Admin/AdminUser/

        #region 后台用户相关视图
    
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 微信扫码登录
        /// </summary>
        /// <returns></returns>
        [NoLogin]
        public ActionResult WeChatLogin()
        {
            return View();
        }

        /// <summary>
        /// 微信注册后台用户
        /// </summary>
        /// <returns></returns>
        [NoLogin]
        public ActionResult WeChatRegister()
        {
            return View();
        }

        [NoLogin]
        public ActionResult Login()
        {
            var manageWebSite = Resolve<IWebSite>();
            var webSite = manageWebSite.GetWebSite();
            if (webSite == null) ViewBag.ICON = string.Empty;
            ViewBag.ICON = webSite.sWebSiteIcon;
            ViewBag.PORT =HttpContext.Application["WebScoket_Port"];
            return View();
        }

        /// <summary>
        /// 个人中心页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PersonCenter()
        {
            var adminUser = new CDELINK_AdminUser();
            var sessionUser = SessionAdminUser();
            adminUser = manage.GetById(sessionUser.ID.ToString());
            //根据角色ID获取角色名称
            var manageRole = Resolve<IAdminRole>();
            ViewBag.sRoleName=manageRole.GetById(adminUser.sRoleId).sRoleName;
            return View(adminUser);
        }


        #endregion

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="Info">分页参数</param>
        /// <param name="searchText">搜索的文本</param>
        /// <param name="iState">会员状态(默认全部)</param>
        /// <returns></returns>
        public ActionResult List(PageInfo pageInfo,string searchText,int iState=-1)
        {
            return Content(manage.PageList(pageInfo, searchText, iState));
        }

        /// <summary>
        /// 添加后台用户
        /// </summary>
        /// <param name="adminUser"></param>
        public void Insert(CDELINK_AdminUser adminUser)
        {
            if (!manage.CheckLoginAccout(adminUser.sLoginAccout))
            {
                if (manage.Insert(adminUser) > 0)
                    result.success = true;
            }
            else
                result.info =string.Format("账号：{0}已被注册,请重新输入", adminUser.sLoginAccout);
        }

        /// <summary>
        /// 编辑后台用户
        /// </summary>
        /// <param name="adminUser"></param>
        public void Update(CDELINK_AdminUser adminUser)
        {
            if (!manage.CheckLoginAccout(adminUser.sLoginAccout, adminUser.ID.ToString()))
            {
                if (manage.Update(adminUser) > 0)
                    result.success = true;
            }
            else
                result.info = string.Format("账号：{0}已被注册,请重新输入", adminUser.sLoginAccout);
        }

        /// <summary>
        ///  根据主键ID重置后台用户账户密码
        /// </summary>
        /// <param name="adminUser"></param>
        public void Reset(string ID)
        {
            if (manage.Reset(ID) > 0)
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
        /// 后台用户登录
        /// </summary>
        /// <param name="sLoginAccout"></param>
        /// <param name="sPassWord"></param>
        [NoLogin]
        public void LoginCheck(string sLoginAccout, string sPassWord,string sCode)
        {
            if (sCode == Session[SESSION.ImgCode].ToString())
            {
                var adminUser = manage.ValidateLogin(sLoginAccout, sPassWord);
                if (adminUser != null)
                {
                    if (adminUser.iState == 0)
                    {//该用户被冻结
                        result.info = "该账户已被冻结,请联系管理员";
                    }
                    else
                    {
                        var obj = manage.GetMenuAndButtonByRoleId(adminUser.sRoleId);
                        Session[SESSION.Menu] = obj.menuList;//缓存的二级菜单
                        Session[SESSION.Button] = obj.buttonList;//缓存的按钮
                        Session[SESSION.AdminUser] = new UserInfo()
                        {
                            sUserName = adminUser.sName,
                            iState = adminUser.iState,
                            ID = adminUser.ID,
                            sRoleId = new Guid(adminUser.sRoleId),
                            bIsSuperMan = false
                        };
                        result.success = true;
                    }
                }
                else
                {
                    result.info = "账号或者密码错误";
                }
            }
            else
            {
                result.info = "验证码错误或者失效";
            }
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
