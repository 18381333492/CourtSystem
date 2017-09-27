using EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using SystemInterface;

namespace SystemModel
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
            adminUser.ID = Guid.NewGuid();
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


        /// <summary>
        /// 根据主键ID重置后台用户密码
        /// </summary>
        /// <param name="Ids"></param>
        public override int Reset(string ID)
        {
            var adminUser = excute.Context.CDELINK_AdminUser.Find(new Guid(ID));
            adminUser.sPassWord = C_Security.MD5("123456");
            return excute.SaveChange();
        }


        /// <summary>
        /// 根据主键ID集合删除后台用户
        /// </summary>
        /// <param name="Ids"></param>
        public override int Cancel(string Ids)
        {
            var res = excute.Cancel<CDELINK_AdminUser>(Ids);
            return res;
        }

        /// <summary>
        /// 根据主键ID冻结/解冻后台用户
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public override int Freeze(string ID)
        {
            var adminUser = excute.Context.CDELINK_AdminUser.Find(new Guid(ID));
            adminUser.iState = adminUser.iState == 1 ? 0 : 1;
            var res= excute.SaveChange();
            return res;

        }
    }
}
