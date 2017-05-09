using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.App_Start.ActionFilter
{
    /// <summary>
    /// 验证后台用户登录的Filter
    /// </summary>
    public class AuthorizeUserLogin: ActionFilterAttribute
    {
        ///// <summary>
        ///// 获取登录者信息
        ///// </summary>
        //protected LoginUser loginUser
        //{
        //    get
        //    {
        //        return User_Session.GetLoginUser();
        //    }
        //}
        /// <summary>
        /// 判断是否登录
        /// </summary>
        /// <param name="filterContext"></param>
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    if (filterContext.HttpContext.Session["User"] == null)
        //    {
        //        //先判断session
        //        if (null == loginUser)
        //        {//session 过期
        //            if (!filterContext.HttpContext.Request.IsAjaxRequest())
        //            {// 请求跳转到Tip页面
        //                filterContext.Result = new RedirectResult("/Admin/User/Login");
        //            }
        //            else
        //            {//ajax请求 返回json格式提示
        //                if (filterContext.HttpContext.Request.HttpMethod == "GET")
        //                {
        //                    filterContext.Result = new RedirectResult("/Admin/User/Login");
        //                }
        //                else
        //                {
        //                    ContentResult content = new ContentResult();
        //                    string sResult = "";// C_Json.SetFalse("登录已过期，请重新登录！");
        //                    content.Content = sResult;
        //                    filterContext.Result = content;
        //                }
        //            }
        //        }
        //    }
        //}
    }
}