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
    public partial class ButtonService: ServiceBase
    {

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [Log("按钮", Operate.添加)]
        public int Add(Button button)
        {
            button.ID = Guid.NewGuid();
            excute.Add<Button>(button);
            return excute.SaveChange(this,"Add");
        }

        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [Log("按钮", Operate.编辑)]
        public int Edit(Button button)
        {
            excute.Edit<Button>(button);
            return excute.SaveChange(this, "Edit");
        }

        /// <summary>
        /// 删除菜单下面的按钮
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [Log("按钮", Operate.删除)]
        public int Cancel(string Ids)
        {
            return  excute.Excute(string.Format( @"DELETE Button WHERE ID={0}", Ids),this, "Cancel");
        }

    }
}
