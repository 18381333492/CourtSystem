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
            sSql.Append("SELECT * FROM CDELINK_AdminRole WHERE 1=1 ");
            //条件查询
            if (!string.IsNullOrEmpty(searchText))
            {
                sSql.AppendFormat("AND (sPhone LIKE '%{0}%' OR sName  LIKE '%{0}%')", searchText);
            }

            var userList = query.PageQuery<CDELINK_AdminRole>(sSql.ToString(), pageInfo);
            return userList.toJson();
        }


        /// <summary>
        /// 根据角色主键ID获取信息
        /// </summary>
        /// <param name="sRoleId"></param>
        /// <returns></returns>
        public override CDELINK_AdminRole GetById(string sRoleId)
        {
            return query.SingleQuery<CDELINK_AdminRole>("SELECT * FROM CDELINK_AdminRole WHERE ID=@ID", new { ID = sRoleId });
        }
    }
}
