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
        public List<ES_Menu> menuList = new List<ES_Menu>();
        public List<ES_Button> buttonList = new List<ES_Button>();
    }
}
