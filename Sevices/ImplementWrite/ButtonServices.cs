using EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevices
{
    public partial class ButtonServices: IButton
    {
        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public override int Insert(CDELINK_Button button)
        {
            button.ID = Guid.NewGuid();
            excute.Add<CDELINK_Button>(button);
            return excute.SaveChange();
        }


        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public override int Update(CDELINK_Button button)
        {
            excute.Add<CDELINK_Button>(button);
            return excute.SaveChange();
        }


        /// <summary>
        ///  根据按钮主键ID删除按钮
        /// </summary>
        /// <param name="sButtonId"></param>
        /// <returns></returns>
        public override int Delete(string sButtonId)
        {
            return 0; /*excute.ExcuteBySql("DELETE CDELINK_Button WHERE ID=@ID", new { ID = sButtonId });*/
        }
    }
}
