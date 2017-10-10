using EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterface;

namespace SystemModel
{
    public partial class ButtonServices: IButton
    {
        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public override int Insert(ES_Button button)
        {
            button.ID = Guid.NewGuid();
            excute.Add<ES_Button>(button);
            return excute.SaveChange();
        }


        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public override int Update(ES_Button button)
        {
            excute.Edit<ES_Button>(button);
            return excute.SaveChange();
        }


        /// <summary>
        ///  根据按钮主键ID删除按钮
        /// </summary>
        /// <param name="sButtonId"></param>
        /// <returns></returns>
        public override int Delete(string sButtonId)
        {
            return excute.Delete<ES_Button>(sButtonId);
        }
    }
}
