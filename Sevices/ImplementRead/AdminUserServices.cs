using DapperHelper;
using Sevices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using EFModels;
using Common;

namespace Sevices
{
    /// <summary>
    /// 后台管理员的读操作相关
    /// </summary>
    public partial class AdminUserServices : IAdminUser
    {
        /// <summary>
        /// 验证公司后台管理员登录
        /// </summary>
        /// <param name="sUserName">用户名</param>
        /// <param name="sPassWord">登录密码</param>
        /// <returns></returns>
        public override CDELINK_AdminUser ValidateLogin(string sLoginAccout, string sPassWord)
        {
            var user = query.SingleQuery<CDELINK_AdminUser>(@"SELECT TOP 1 * FROM CDELINK_AdminUser 
                                                                WHERE sLoginAccout=@sLoginAccout AND sPassWord=@sPassWord", new
            {
                sLoginAccout = sLoginAccout,
                sPassWord = C_Security.MD5(sPassWord)
            });
            return user;
        }

        /// <summary>
        /// 根据管理员主键ID获取信息
        /// </summary>
        /// <param name="sUserId"></param>
        /// <returns></returns>
        public override CDELINK_AdminUser GetById(string sUserId)
        {
            return query.SingleQuery<CDELINK_AdminUser>("SELECT TOP 1 * FROM CDELINK_AdminUser WHERE ID=@ID", new { ID= sUserId });
        }
    }
}
