using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Common;
using System.Threading;
using EFModel;

namespace Web
{
	// 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
	// 请访问 http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

            //定义全局变量统计在线人数
            Application.Add("Online",0);
            Application.Add("Visits", 0);//访问量

            Timer timer = new Timer(new TimerCallback(VisitsCount),
                                     null,//一个包含回调方法要使用的信息的对象，或者为空引用
                                     0,//调用 callback 之前延迟的时间量（以毫秒为单位）。(即第一次调用该委托的时间间隔)
                                     //指定 Timeout.Infinite 以防止计时器开始计时。指定零 (0) 以立即启动计时器。
                                     1*24*60*60*1000);//调用 callback 的时间间隔（以毫秒为单位）
        }
        
        /// <summary>
        /// 截取应用程序的错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender,EventArgs e)
        {       
            //Exception ex = Server.GetLastError();
            //if(ex is HttpException)
            //{
            //    if((ex as HttpException).GetHttpCode() == (int)HttpStatusCode.NotFound)
            //    {
            //        ;
            //    }
            //}
        }


        /// <summary>
        /// 会话结束
        /// </summary>
        protected void Session_End()
        {
            Application["Online"] = Application["Online"].toInt32() - 1;
            
        }

        /// <summary>
        /// 会话开始
        /// </summary>
        protected void Session_Start()
        {
            Application["Online"] = Application["Online"].toInt32() + 1;//统计在线人数
            Application["Visits"] = Application["Visits"].toInt32() + 1;//统计访问量
        }


        /// <summary>
        /// 统计访问量写入数据库
        /// </summary>
        private void VisitsCount(object a)
        {
            using (var db=new Entities())
            {
               
            }
        }
    }
}