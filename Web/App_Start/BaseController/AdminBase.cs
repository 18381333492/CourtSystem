using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using EFModels;
using EFModels.MyModels;

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
            return DIEntity.GetInstance().GetImpl<M>();
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
                if (SESSION.AdminUser== null)
                {
                    /*登录过时,session过期*/
                    if (filterContext.HttpContext.Request.HttpMethod.ToUpper() == "GET")
                    {
                        /*跳转到登录过期提示页面*/
                        filterContext.Result = new RedirectResult("/Admin/AdminUser/Login");
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
    }
}