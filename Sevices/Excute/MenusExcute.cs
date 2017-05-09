using EFModel;
using EFModel.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Logs;

namespace Sevices
{
    /// <summary>
    /// 菜单的服务
    /// </summary>
    public partial class MenusService: ServiceBase
    {

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [Log("菜单",Operate.添加)]
        public int Add(Menus menu)
        {
            menu.ID = Guid.NewGuid();
            menu.sMenuIcon = string.Empty;
            menu.dInsertTime = DateTime.Now;
            if (string.IsNullOrEmpty(menu.sParentMenuId))
                menu.sParentMenuId = string.Empty;
            excute.Add<Menus>(menu);
            return excute.SaveChange(this,"Add");
        }

        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [Log("菜单", Operate.编辑)]
        public int Edit(Menus menu)
        {
            if (string.IsNullOrEmpty(menu.sParentMenuId))
                menu.sParentMenuId = string.Empty;
            menu.sMenuIcon = string.Empty;
            excute.Edit<Menus>(menu);
            return excute.SaveChange(this, "Edit");
        }


        /// <summary>
        /// 删除菜单数据
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [Log("菜单", Operate.删除)]
        public int Cancel(string Ids)
        {
            return  excute.Cancel<Menus>(Ids, this, "Cancel");
        }

    }
}
