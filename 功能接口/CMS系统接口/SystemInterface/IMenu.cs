using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels;

namespace SystemInterface
{
    /// <summary>
    /// 菜单接口
    /// </summary>
    public abstract class IMenu: BaseBll
    {
        #region 查询

        /// <summary>
        /// 根据菜单主键ID获取菜单
        /// </summary>
        /// <param name="sButtonId"></param>
        /// <returns></returns>
        public abstract CDELINK_Menu GetById(string sMenuId);


        /// <summary>
        /// 获取所有的菜单列表
        /// </summary>
        /// <returns></returns>
        public abstract string List();

        /// <summary>
        /// 获取所有的一级菜单
        /// </summary>
        /// <returns></returns>
        public abstract List<Dictionary<string, object>> GetMianMenuList();

        /// <summary>
        ///  根据菜单名称检查是否有重名的菜单
        /// </summary>
        /// <param name="sMenuName">菜单名称</param>
        /// <param name="sMenuId">菜单的主键ID</param>
        /// <returns></returns>
        public abstract bool CheckMenuName(string sMenuName, string sMenuId=null);


        #endregion


        #region 操作

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public abstract int Insert(CDELINK_Menu menu);


        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public abstract int Update(CDELINK_Menu menu);


        /// <summary>
        ///  根据菜单主键ID集合删除菜单
        /// </summary>
        /// <param name="sButtonId"></param>
        /// <returns></returns>
        public abstract int Cancel(string Ids);
       
        #endregion
    }
}
