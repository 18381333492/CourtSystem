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
    /// 角色的读操作相关
    /// </summary>
    public partial class AdminRoleServices : IAdminRole
    {
        /// <summary>
        /// 分页获取后台角色数据列表
        /// </summary>
        /// <param name="Info">分页参数</param>
        /// <param name="searchText">搜索的文本</param>
        /// <returns></returns>
        public override string PageList(PageInfo pageInfo, string searchText)
        {
            pageInfo.order = OrderType.DESC;
            pageInfo.sort = "dInsertTime";
            StringBuilder sSql = new StringBuilder();
            sSql.Append("SELECT * FROM CDELINK_AdminRole WHERE 1=1");

            //条件查询
            if (!string.IsNullOrEmpty(searchText))
            {
                sSql.AppendFormat("AND (sRoleName  LIKE '%{0}%')", searchText);
            }

            var userList = query.PageQuery(sSql.ToString(), pageInfo);
            return userList.toJson();
        }


        /// <summary>
        /// 根据角色主键ID获取信息
        /// </summary>
        /// <param name="sRoleId"></param>
        /// <returns></returns>
        public override CDELINK_AdminRole GetById(string sRoleId)
        {
            return query.Find<CDELINK_AdminRole>(sRoleId);
        }


        /// <summary>
        /// 检查是否存在相同的角色名称
        /// </summary>
        /// <param name="sRoleName">角色名称</param>
        /// <param name="sRoleId">角色主键ID</param>
        /// <returns></returns>
        public override bool CheckRoleName(string sRoleName, string sRoleId = null)
        {
            var res = false;
            if (string.IsNullOrEmpty(sRoleId))
                res = query.Any("SELECT * FROM CDELINK_AdminRole WHERE sRoleName=@sRoleName", new { sRoleName = sRoleName });
            else
                res = query.Any("SELECT * FROM CDELINK_AdminRole WHERE sRoleName=@sRoleName AND ID!=@ID", new { sRoleName = sRoleName, ID = sRoleId });
            return res;
        }


        /// <summary>
        /// 根据角色主键Ids是否存在对应的后台用户
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public override bool IsExitAdminUserByRoleId(string Ids)
        {
            var res = query.Any(string.Format(@"SELECT * FROM CDELINK_AdminUser WHERE sRoleId IN({0}) AND bIsDeleted=0", Ids));
            return res;
        }

    }
}
