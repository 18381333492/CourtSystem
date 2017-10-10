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
    /// 网站的写操作相关
    /// </summary>
    public partial class WebSiteServices : IWebSite
    {

        /// <summary>
        /// 设置网站信息
        /// </summary>
        /// <param name="website"></param>
        /// <returns></returns>
        public override int SetWebSite(ES_WebSite website)
        {
            var tempSite = excute.Context.ES_WebSite.AsNoTracking().FirstOrDefault();
            if (tempSite == null)
            {
                website.ID =Guid.NewGuid();
                excute.Add<ES_WebSite>(website);
            }
            else
                excute.Edit<ES_WebSite>(website);
            return excute.SaveChange();
        }

    }
}
