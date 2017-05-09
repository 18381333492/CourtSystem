using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Web.App_Start.ActionFilter
{
    /// <summary>
    /// 验证前台会员登录的Filter
    /// </summary>
    public class AuthorizeClientLogin:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Session["User"] == null)
            {
                //session为空,跳转登录
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {//ajax请求
                    ContentResult content = new ContentResult();
                    //string sResult = C_Json.SetFalse("登录已过期，请重新登录！");
                    //content.Content = sResult;
                    filterContext.Result = content;
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Admin/User/Login");
                }

            }
        }
    }
}