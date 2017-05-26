using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels;

namespace SystemInterface
{
    /// <summary>
    /// 按钮接口
    /// </summary>
    public abstract class IButton: BaseBll
    {
        #region 查询


        /// <summary>
        /// 获取菜单按钮数据列表
        /// </summary>
        /// <returns></returns>
        public abstract string GetList();

        /// <summary>
        /// 根据按钮主键ID获取按钮
        /// </summary>
        /// <param name="sButtonId"></param>
        /// <returns></returns>
        public abstract CDELINK_Button GetById(string sButtonId);


        #endregion


        #region 操作

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public abstract int Insert(CDELINK_Button button);


        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public abstract int Update(CDELINK_Button button);


        /// <summary>
        ///  根据按钮主键ID删除按钮
        /// </summary>
        /// <param name="sButtonId"></param>
        /// <returns></returns>
        public abstract int Delete(string sButtonId);
       
        #endregion
    }
}
