using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels;

namespace Sevices
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
        ///  根据菜单主键ID删除菜单
        /// </summary>
        /// <param name="sButtonId"></param>
        /// <returns></returns>
        public abstract int Delete(string sMenuId);
       
        #endregion
    }
}
