using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels;
using EFModels.MyModels;

namespace SystemInterface
{
    public abstract class ISuperMan : BaseBll
    {
        #region 查询

        /// <summary>
        /// 验证超级管理员的验证.
        /// </summary>
        /// <param name="sLoginAccout"></param>
        /// <param name="sPassWord"></param>
        public abstract ES_AdminUser CheckLogin(string sLoginAccout, string sPassWord);

        ///// <summary>
        ///// 网站是否能继续运行
        ///// </summary>
        ///// <returns></returns>
        //public abstract bool IsStartUp();
        #endregion




    }
}
