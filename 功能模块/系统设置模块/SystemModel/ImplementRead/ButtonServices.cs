
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels;
using SystemInterface;

namespace SystemModel
{
    public partial class ButtonServices: IButton
    {
        /// <summary>
        /// 根据按钮主键ID获取按钮
        /// </summary>
        /// <param name="sButtonId"></param>
        /// <returns></returns>
        public override CDELINK_Button GetById(string sButtonId)
        {
            var button = query.SingleQuery<CDELINK_Button>(@"SELECT * FROM CDELINK_Button WHERE ID=@ID", new { ID = sButtonId });
            return button;
        }
    }
}
