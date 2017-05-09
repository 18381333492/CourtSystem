using EFModel.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFModel
{
    public partial class User
    {
        public Result result
        {
            get;
            set;
        }
    }

    [Serializable]
    public partial class Button
    {

    }

    [Serializable]
    public partial class Menus
    {

    }
}
