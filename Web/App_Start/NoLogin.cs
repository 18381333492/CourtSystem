using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.App_Start
{
    /// <summary>
    /// 使用验证时 [NoLogin] 标注不需要登录
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public class NoLogin:Attribute
    {
    }
}