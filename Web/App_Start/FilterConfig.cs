using System.Web;
using System.Web.Mvc;

namespace Web
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
            //注释下面的语句,只要程序报错后出现异常都会执行 Application_Error
            // 只要添加对HandleErrorAttribute 的注册绑定出现代码错误都不会执行Application_Error
            filters.Add(new HandleErrorAttribute());
		}
	}
}