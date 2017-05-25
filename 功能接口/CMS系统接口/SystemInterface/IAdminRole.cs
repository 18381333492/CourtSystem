using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels;
using EFModels.MyModels;

namespace SystemInterface
{
    public abstract class IAdminRole : BaseBll
    {
        #region 查询


        /// <summary>
        /// 分页获取后台角色数据列表
        /// </summary>
        /// <param name="Info">分页参数</param>
        /// <param name="searchText">搜索的文本</param>
        /// <returns></returns>
        public abstract string PageList(PageInfo Info, string searchText);


        /// <summary>
        /// 根据角色主键ID获取信息
        /// </summary>
        /// <param name="sRoleId"></param>
        /// <returns></returns>
        public abstract CDELINK_AdminRole GetById(string sRoleId);

        /// <summary>
        /// 检查是否存在相同的角色名称
        /// </summary>
        /// <param name="sRoleName">角色名称</param>
        /// <param name="sRoleId">角色主键ID</param>
        /// <returns></returns>
        public abstract bool CheckRoleName(string sRoleName, string sRoleId=null);

        #endregion


        #region 操作

        /// <summary>
        /// 添加后台角色
        /// </summary>
        /// <param name="adminRole"></param>
        /// <returns></returns>
        public abstract int Insert(CDELINK_AdminRole adminRole);

        /// <summary>
        /// 编辑后台角色
        /// </summary>
        /// <param name="adminRole"></param>
        /// <returns></returns>
        public abstract int Update(CDELINK_AdminRole adminRole);


        /// <summary>
        /// 根据主键ID集合删除角色
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public abstract int Cancel(string Ids);
        #endregion

    }
}
