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
    public class MobileBaseController<T>: Controller, IExceptionFilter
    { 
        //
        // GET: /AdminBase/

        public Result result;//返回结果集

        protected T _server;

        public MobileBaseController()
        {
            result = new Result();
            _server = Resolve<T>();
        }

        protected M Resolve<M>()
        {
            return (M)Activator.CreateInstance(typeof(M));
        }

        /// <summary>
        /// 在Action执行完之后操作
        /// </summary>
        /// <param name = "filterContext" ></ param >
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var actionMethod = filterContext.Controller
              .GetType()
              .GetMethod(filterContext.ActionDescriptor.ActionName);//获取访问方法
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
