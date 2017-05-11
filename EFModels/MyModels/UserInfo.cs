using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFModels.MyModels
{

    /// <summary>
    /// 后台session保存的数据
    /// <summary>
    [Serializable]
    public  class UserInfo
    {
        public Guid ID
        {
            get;
            set;
        }

        /// <summary>
        /// 后台用户状态标识 0-冻结 1-正常
        /// </summary>
        public int iState
        {
            get;
            set;
        }

        public string sUserName
        {
            get;
            set;
        }

        public Guid sRoleId
        {
            get;
            set;
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string sRoleName
        {
            get;
            set;
        }

        /// <summary>
        /// Ip地址
        /// </summary>
        public string Ip
        {
            get;
            set;
        }
    }
}
