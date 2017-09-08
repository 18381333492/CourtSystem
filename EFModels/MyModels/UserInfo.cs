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
        /// 后台用户状态标识 0-审核中 1-正常
        /// </summary>
        public int iState
        {
            get;
            set;
        }

        /// <summary>
        /// 微信用户名称
        /// </summary>
        public string sUserName
        {
            get;
            set;
        }

        /// <summary>
        /// 角色ID
        /// </summary>
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

        /// <summary>
        /// 微信头像
        /// </summary>
        public string sHeadPic
        {
            get;
            set;
        }

        public bool bIsSuperMan
        {
            get;
            set;
        }
    }
}
