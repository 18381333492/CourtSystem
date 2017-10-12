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
        public override int Insert(ES_AdminUser adminUser)
        {
            adminUser.ID = Guid.NewGuid();
            excute.Add<ES_AdminUser>(adminUser);
            return excute.SaveChange();
        }

        /// <summary>
        /// 设置后台用户角色
        /// </summary>
        /// <param name="adminUser"></param>
        /// <returns></returns>
        public override int SetRole(Guid ID, string sRoleId)
        {
            var user = excute.Context.ES_AdminUser.AsNoTracking().FirstOrDefault(m => m.ID==ID);
            user.sRoleId = sRoleId;
            excute.Edit<ES_AdminUser>(user);
            return excute.SaveChange();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public override int AlertPwd(Guid ID, string sPassword)
        {
            var user = excute.Context.ES_AdminUser.AsNoTracking().FirstOrDefault(m => m.ID == ID);
            user.sPassWord = C_Security.MD5(sPassword);
            excute.Edit<ES_AdminUser>(user);
            return excute.SaveChange();
        }

        /// <summary>
        /// 设置账户信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="sAccount">账户信息</param>
        /// <param name="sPassword">密码</param>
        /// <returns></returns>
        public override int SetAccount(Guid ID, string sAccount, string sPassword)
        {
            var user = excute.Context.ES_AdminUser.AsNoTracking().FirstOrDefault(m => m.ID == ID);
            user.sLoginAccout = sAccount;
            user.sPassWord = C_Security.MD5(sPassword);
            excute.Edit<ES_AdminUser>(user);
            return excute.SaveChange();
        }


        /// <summary>
        /// 根据主键ID集合删除后台用户
        /// </summary>
        /// <param name="Ids"></param>
        public override int Cancel(string Ids)
        {
            var res = excute.Cancel<ES_AdminUser>(Ids);
            return res;
        }

        /// <summary>
        /// 根据主键ID冻结/解冻后台用户
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public override int Freeze(string ID)
        {
            var adminUser = excute.Context.ES_AdminUser.Find(new Guid(ID));
            adminUser.iState = adminUser.iState == 1 ? 0 : 1;
            var res= excute.SaveChange();
            return res;

        }
    }
}
