using EFModel;
using EFModel.MyModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.App_Start
{
    public class AdminBaseController<T> : Controller, IExceptionFilter where T:class,new ()
    {
        //
        // GET: /AdminBase/

        public Result result;//返回结果集

        protected T _server;

        public AdminBaseController()
        {
            result = new Result();
            _server = Resolve<T>();
        }

        protected M Resolve<M>()
        {
            return (M)Activator.CreateInstance(typeof(M));
        }

        /// <summary>
        /// 缓存的session的相关信息
        /// </summary>
        public class SESSION
        { 
            public static string User = "@_User";
            public static string Menu = "@_Menu";
            public static string Button = "@_Button";
            public static string ImgCode = "@_ImgCode";
        }

        /// <summary>
        /// 获取SessionUser
        /// </summary>
        /// <returns></returns>
        protected UserInfo SessionUser()
        {
            return Session[SESSION.User] as UserInfo;
        }

        /// <summary>
        /// 重写RequestContext的对象数据的初始化
        /// tip: /*反射缓存设置用户的相关信息*/
        /// </summary>
        /// <param name = "requestContext" ></ param >
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session[SESSION.User] != null)
            {
                UserInfo info = SessionUser();
                /*反射缓存设置用户的相关信息*/
                Type t = _server.GetType(); //获得该类的Type
                PropertyInfo sUserName = t.GetProperty("sUserName");
                PropertyInfo sIpAddress = t.GetProperty("sIpAddress");

                sUserName.SetValue(_server, info.sUserName, null);
                sIpAddress.SetValue(_server, info.Ip, null);
            }
        }

        /// <summary>
        /// 在Action之前调用
        /// tip:主要来验证用户登录
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!(filterContext.ActionDescriptor.GetCustomAttributes(typeof(NoLogin), true).Length == 1))
            {//有NoLogin属性;不判断登录
                if (Session[SESSION.User] == null)
                {
                    /*登录过时,session过期*/
                    if (filterContext.HttpContext.Request.HttpMethod.ToUpper() == "GET")
                    {
                        /*跳转到登录过期提示页面*/
                        filterContext.Result = new RedirectResult("/Admin/Home/Login");
                    }
                    else
                    {
                        result.over = true;//登录过时
                        ContentResult res = new ContentResult();
                        res.Content = result.toJson();
                        filterContext.Result = res;
                    }
                }
            }
        }

        /// <summary>
        /// 在Action执行完之后操作
        /// tip:主要根据角色获取权限相应的按钮
        /// </summary>
        /// <param name = "filterContext" ></ param >
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var actionMethod = filterContext.Controller
              .GetType()
              .GetMethod(filterContext.ActionDescriptor.ActionName);//获取访问方法
            if (Session[SESSION.User] != null)
            {
                if (request.HttpMethod.ToUpper() == "GET")
                {//请求的方式为Get
                    var user = SessionUser();
                    //请求的路径
                    var sPath = filterContext.RequestContext.HttpContext.Request.Url.AbsolutePath.ToLower();
                    if (sPath.Contains("index") && !sPath.Contains("home"))
                    {
                        var menu = (Session[SESSION.Menu] as List<Menus>).Where(m => m.sMenuUrl.ToLower() == sPath).FirstOrDefault();
                        var buttonList = (Session[SESSION.Button] as List<Button>).Where(m => m.sToMenuId == menu.ID).OrderBy(m=>m.iOrder);
                        filterContext.Controller.ViewData["Button"] = buttonList.Count() > 0?buttonList.ToList():new List<Button>();
                    }
                }
            }
            if (actionMethod.ReturnType.Name.ToString() == "Void"&& request.IsAjaxRequest()&& request.HttpMethod.ToUpper()=="POST")
            {
                filterContext.Result = Content(result.toJson()); /**统一处理ajax的返回结果**/
            }

        }

        /// <summary>
        /// 异常捕捉
        /// tip：捕捉代码异常,写代码错误日志.
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;
            Logs.LogHelper.ErrorLog(e);
            if (filterContext.HttpContext.Request.HttpMethod.ToUpper() == "POST")
            {
                result.success = false;
                result.info = "数据丢失,请重新操作!";
                filterContext.Result = Content(result.toJson());
            }
        }
    }
}
