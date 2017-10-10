using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using EFModels;
using EFModels.MyModels;
using Common;

namespace Web.App_Start.BaseController
{
    public class AdminBase<T>: Controller
    {

         //返回结果集
         protected Result result = new Result();

        //接口
        protected T manage;

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        public AdminBase()
        {
            manage = Resolve<T>();
        }

        /// <summary>
        /// 映射实现接口
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <returns></returns>
        protected M Resolve<M>()
        {
            return DIEntity.Instance.GetImpl<M>();
        }

        /// <summary>
        /// 获取后台AdminUserSession
        /// </summary>
        /// <returns></returns>
        protected UserInfo SessionAdminUser()
        {
            return Session[SESSION.AdminUser] as UserInfo;
        }

        /// <summary>
        /// 在Action之前调用
        /// tip:主要来验证用户登录
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (bool.Parse(filterContext.HttpContext.Application["bIsStartUp"].ToString())==true)
            {//网站能继续运行
                if (!(filterContext.ActionDescriptor.GetCustomAttributes(typeof(NoLogin), true).Length == 1))
                {//有NoLogin属性;不判断登录
                    if (Session[SESSION.AdminUser] == null)
                    {
                        /*登录过时,session过期*/
                        if (filterContext.HttpContext.Request.HttpMethod.ToUpper() == "GET")
                        {
                            /*跳转到登录过期提示页面*/
                            var LoginPath = C_Config.ReadAppSetting("virtualPath");
                            filterContext.Result = new RedirectResult(LoginPath + "/Admin/AdminUser/Login");
                        }
                        else
                        {
                            result.over = true;//登录过时
                            ContentResult res = new ContentResult();
                            res.Content = result.toJson();
                            filterContext.Result = res;
                        }
                    }
                    else
                    {//session存在

                    }
                }
            }
            else
            {//网站过期
                if (filterContext.HttpContext.Request.HttpMethod.ToUpper() == "GET")
                {
                    /*跳转到网站到期提示页面*/
                    var LoginPath = C_Config.ReadAppSetting("virtualPath");
                    filterContext.Result = new RedirectResult("http://www.baidu.com");
                }
                else
                {
                    result.success = false;
                    result.info = "网站运行时间已到期,不能执行任何操作.请及时充值!";
                    ContentResult res = new ContentResult();
                    res.Content = result.toJson();
                    filterContext.Result = res;
                }
            }
        }

        /// <summary>
        /// 在Action之后调用
        /// 主要处理返回的数据
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var actionMethod = filterContext.Controller.GetType().GetMethods().
                    FirstOrDefault(m=>m.Name.ToLower()== filterContext.ActionDescriptor.ActionName.ToLower());//获取访问方法   
            if (Session[SESSION.AdminUser] != null)
            {
                if (request.HttpMethod.ToUpper() == "GET")
                {//请求的方式为Get
                    var user = SessionAdminUser();
                    //请求的路径
                    var sPath = filterContext.RequestContext.HttpContext.Request.Url.AbsolutePath.ToLower();
                    if (sPath.Contains("index") && !sPath.Contains("home"))
                    {
                        var menu = (Session[SESSION.Menu] as List<ES_Menu>).FirstOrDefault(m => m.sMenuUrl.ToLower() == sPath);
                        var buttonList = (Session[SESSION.Button] as List<ES_Button>).
                                        Where(m => m.sMenuId == menu.ID.ToString()).OrderBy(m => m.iOrder);
                        var Button_Toolbar = buttonList.Where(m => m.bIsToolbar == true);//导航栏上的按钮
                        var Button_NotToolbar = buttonList.Where(m => m.bIsToolbar == false);//数据栏上的按钮
                        filterContext.Controller.ViewData["Button_Toolbar"] = Button_Toolbar.Count() > 0 ? Button_Toolbar.ToList() : new List<ES_Button>();
                        filterContext.Controller.ViewData["Button_NotToolbar"] = buttonList.Count() > 0 ? C_Json.toJson(Button_NotToolbar.ToList()) : C_Json.toJson(new List<ES_Button>());
                    }
                }
            }
            if (actionMethod.ReturnType.Name.ToLower() == "void" && request.IsAjaxRequest() && request.HttpMethod.ToLower() == "post")
            {//POST的返回结果处理
                filterContext.Result = Content(result.toJson()); /**统一处理ajax的返回结果**/
            }
        }

        /// <summary>
        /// 捕捉异常
        /// 统一处理错误日志
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            ////表示异常已经处理 不在对IIS抛出异常
            filterContext.ExceptionHandled = true;

            //记录错误日志
            var logger = Logs.LogsHelper.Instance.GetLogger("web");
            logger.Fatal(filterContext.Exception.Message, filterContext.Exception);

            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                result.info = filterContext.Exception.Message;
                filterContext.Result = Content(result.toJson());
            }
            else
            {//跳转错误页面
                
            }   
        }
    }
}