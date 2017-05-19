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
    /// 后台管理员的读操作相关
    /// </summary>
    public partial class AdminUserServices : IAdminUser
    {
        /// <summary>
        /// 分页获取后台用户数据列表
        /// </summary>
        /// <param name="Info">分页参数</param>
        /// <param name="searchText">搜索的文本</param>
        /// <param name="iState">会员状态(默认全部)</param>
        /// <returns></returns>
        public override string PageList(PageInfo pageInfo, string searchText, int iState)
        {
            pageInfo.order = OrderType.DESC;
            pageInfo.sort = "dInsertTime";
            StringBuilder sSql = new StringBuilder();
            sSql.Append("SELECT * FROM CDELINK_AdminUser WHERE bIsDeleted=0 ");
            //条件查询
            if (iState > 0)
            {
                sSql.AppendFormat("AND iState={0}", iState);
            }
            if (!string.IsNullOrEmpty(searchText))
            {
                sSql.AppendFormat("AND (sPhone LIKE '%{0}%' OR sName  LIKE '%{0}%')", searchText);
            }

            var userList = query.PageQuery<CDELINK_AdminUser>(sSql.ToString(), pageInfo);
            return userList.toJson();
        }

        /// <summary>
        /// 验证公司后台管理员登录
        /// </summary>
        /// <param name="sUserName">用户名</param>
        /// <param name="sPassWord">登录密码</param>
        /// <returns></returns>
        public override CDELINK_AdminUser ValidateLogin(string sLoginAccout, string sPassWord)
        {
            var user = query.SingleQuery<CDELINK_AdminUser>(@"SELECT * FROM CDELINK_AdminUser 
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
            return query.SingleQuery<CDELINK_AdminUser>("SELECT * FROM CDELINK_AdminUser WHERE ID=@ID", new { ID = sUserId });
        }
    }
}
