using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFModels.MyModels
{
    [Serializable]
    public class MenuAndButton
    {
        public List<CDELINK_Menu> menuList = new List<CDELINK_Menu>();
        public List<CDELINK_Button> buttonList = new List<CDELINK_Button>();
    }
}
