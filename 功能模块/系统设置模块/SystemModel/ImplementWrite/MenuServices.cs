using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels;
using SystemInterface;

namespace SystemModel
{
    public partial class MenuServices: IMenu
    {
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public override int Insert(ES_Menu menu)
        {
            menu.ID = Guid.NewGuid();
            menu.dInsertTime = DateTime.Now;
            excute.Add<ES_Menu>(menu);
            return excute.SaveChange();
        }


        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public override int Update(ES_Menu menu)
        {
            excute.Edit<ES_Menu>(menu);
            return excute.SaveChange();
        }


        /// <summary>
        ///  根据菜单主键ID集合删除菜单
        /// </summary>
        /// <param name="sButtonId"></param>
        /// <returns></returns>
        public override int Cancel(string Ids)
        {
            var res = excute.Cancel<ES_Menu>(Ids);
            return res;
        }
    }
}
