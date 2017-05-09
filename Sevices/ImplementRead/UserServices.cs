using DapperHelper;
using EFModel;
using Sevices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Sevices
{
    /// <summary>
    /// 后台管理员的读操作相关
    /// </summary>
    public partial class UserServices : IUser
    {
        /// <summary>
        /// 验证公司后台管理员登录
        /// </summary>
        /// <param name="sUserName">用户名</param>
        /// <param name="sPassWord">登录密码</param>
        /// <returns></returns>
        public override User ValidateLogin(string sUserName, string sPassWord)
        {
            var user =query.SingleQuery<User>("SELECT TOP 1 * FROM [User]");
            return user;
        }

        /// <summary>
        /// 根据管理员主键ID获取信息
        /// </summary>
        /// <param name="sUserId"></param>
        /// <returns></returns>
        public override User GetById(string sUserId)
        {
            return query.SingleQuery<User>("SELECT TOP 1 * FROM [User] WHERE ID=@ID",new { ID= sUserId });
        }
    }
}
