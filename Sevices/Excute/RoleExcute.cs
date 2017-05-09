using EFModel;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sevices
{
    public partial class RoleService : ServiceBase
    {
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        [Log("角色",Operate.添加)]
        public int Add(Role item)
        {
            item.ID =Guid.NewGuid();
            item.sInsertTime = DateTime.Now;
            item.sRolePower = string.Empty;
            excute.Add<Role>(item);
            return excute.SaveChange(this,"Add");      
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [Log("角色", Operate.编辑)]
        public int Edit(Role item)
        {
            var temp = query.db.Role.Find(item.ID);
            temp.sRoleName = item.sRoleName;
            excute.Edit<Role>(temp);
            return excute.SaveChange(this, "Edit");
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Ids"></param>
        [Log("角色", Operate.删除)]
        public int Cancel(string Ids)
        {
            List<string> list = Ids.Split(',').Select(m => m.Trim('\'')).ToList();
            var entry = query.db.User.Where(m => list.Contains(m.sRoleID.ToString().ToLower()));
            if (entry.Count() == 0)
            {
                return excute.Cancel<Role>(Ids, this, "Cancel");
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="sPower"></param>
        /// <returns></returns>
        [Log("角色权限", Operate.修改)]
        public int SetPower(Guid ID,string sPower)
        {
            var role = query.db.Role.Find(ID);
            role.sRolePower = sPower;
            excute.Edit<Role>(role);
            return  excute.SaveChange(this, "SetPower");
        }
    }
}
