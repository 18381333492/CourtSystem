using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels;
using EFModels.MyModels;

namespace SystemInterface
{
    public abstract class IWebSite : BaseBll
    {
        #region 查询


        /// <summary>
        /// 获取网站信息
        /// </summary>
        /// <returns></returns>
        public abstract CDELINK_WebSite GetWebSite();


        #endregion


        #region 操作

        /// <summary>
        /// 设置网站信息
        /// </summary>
        /// <param name="website"></param>
        /// <returns></returns>
        public abstract int SetWebSite(CDELINK_WebSite website);

        #endregion



    }
}
