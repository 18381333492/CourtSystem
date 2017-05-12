using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels;

namespace Sevices
{
    public partial class MenuServices: IMenu
    {
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public override int Insert(CDELINK_Menu menu)
        {
            excute.Add<CDELINK_Menu>(menu);
            return excute.SaveChange();
        }


        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public override int Update(CDELINK_Menu menu)
        {
            excute.Edit<CDELINK_Menu>(menu);
            return excute.SaveChange();
        }


        /// <summary>
        ///  根据菜单主键ID删除菜单
        /// </summary>
        /// <param name="sButtonId"></param>
        /// <returns></returns>
        public override int Delete(string sMenuId)
        {
            return 0;
        }
    }
}
