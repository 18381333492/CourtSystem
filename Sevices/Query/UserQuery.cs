using EFModel;
using EFModel.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Sevices
{
    public partial class UserService:ServiceBase
    {

        /// <summary>
        /// 分页获取后台用户数据
        /// </summary>
        /// <param name="Info">分页参数</param>
        /// <param name="Params">查询参数</param>
        /// <returns></returns>
        public string GetList(PageInfo Info,Dictionary<string,object> Params)
        {
            return query.QueryPage(@"select A.*,B.sRoleName from [User] AS A 
                                            LEFT JOIN [Role] AS B  
                                            ON A.sRoleID=B.ID WHERE A.bIsDeleted=0 ", Info, null);
        }

        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public User Get(Guid ID)
        {
            return query.db.User.Find(ID);
        }

        /// <summary>
        /// 检查用户名是否已存在
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>
        public bool CheckUserName(string sUserName)
        {
            if (query.db.User.Where(m => m.sUserName == sUserName && m.bIsDeleted == false).Count() > 0)
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        ///  用户登录
        /// </summary>
        /// <param name="sUserName">用户</param>
        /// <param name="sPassWord">密码</param>
        /// <returns></returns>
        public User Login(string sUserName, string sPassWord,out string sRoleName)
        {
            sRoleName = string.Empty;
            sPassWord = C_Security.MD5(sPassWord);
            var user = query.db.User.Where(m => m.sUserName == sUserName && m.sPassWord == sPassWord&&m.bIsDeleted==false).SingleOrDefault();
            if (user != null)
            {
                sRoleName = query.db.Role.Find(user.sRoleID).sRoleName;
            }
            return user;
        }
    }
}
