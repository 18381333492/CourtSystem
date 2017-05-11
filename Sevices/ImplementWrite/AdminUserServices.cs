using EFModels;
using Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Sevices
{
    public partial class AdminUserServices: IAdminUser
    {
        /// <summary>
        /// 添加后台用户
        /// </summary>
        /// <param name="adminUser"></param>
        /// <returns></returns>
        public override int Insert(CDELINK_AdminUser adminUser)
        {
            adminUser.sPassWord = C_Security.MD5(adminUser.sPassWord);
            adminUser.iState = 1;
            adminUser.bIsSuper = false;
            adminUser.bIsDeleted = false;
            adminUser.dInsertTime = DateTime.Now;
            excute.Add<CDELINK_AdminUser>(adminUser);
            return excute.SaveChange();
        }

        /// <summary>
        /// 编辑后台用户
        /// </summary>
        /// <param name="adminUser"></param>
        /// <returns></returns>
        public override int Update(CDELINK_AdminUser adminUser)
        {
            excute.Edit<CDELINK_AdminUser>(adminUser);
            return excute.SaveChange();
        }

    }
}
