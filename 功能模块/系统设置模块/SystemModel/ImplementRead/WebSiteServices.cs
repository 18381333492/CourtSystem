using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterface;
using EFModels;
using Common;

namespace SystemModel
{
    /// <summary>
    /// 网站的读操作相关
    /// </summary>
    public partial class WebSiteServices : IWebSite
    {

        /// <summary>
        /// 获取网站信息
        /// </summary>
        /// <returns></returns>
        public override ES_WebSite GetWebSite()
        {
            return query.SingleQuery<ES_WebSite>(@"SELECT * FROM ES_WebSite");
        }
    }
}
