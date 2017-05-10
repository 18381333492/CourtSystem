using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels;

namespace Sevices.Interface
{
    public abstract class IAdminUser: BaseBll
    {
        #region 查询

        /// <summary>
        /// 验证公司后台管理员登录
        /// </summary>
        /// <param name="sUserName">用户名</param>
        /// <param name="sPassWord">登录密码</param>
        /// <returns></returns>
        public abstract CDELINK_AdminUser ValidateLogin(string sUserName, string sPassWord);


        /// <summary>
        /// 根据管理员主键ID获取信息
        /// </summary>
        /// <param name="sUserId"></param>
        /// <returns></returns>
        public abstract CDELINK_AdminUser GetById(string sUserId);

        #endregion






        #region 操作

        #endregion

    }
}
