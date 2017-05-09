using EFModel;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Sevices
{
    public partial class UserService: ServiceBase
    {

        /// <summary>
        /// 添加后台用户
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        [Log("用户",Operate.添加)]
        public int Add(User item)
        {
            item.ID = Guid.NewGuid();
            item.dInsertTime = DateTime.Now;
            item.bState = true;
            item.sPassWord = C_Security.MD5(item.sPassWord);
            item.sPhone = string.Empty;
            excute.Add<User>(item);
            return excute.SaveChange(this,"Add");      
        }

        /// <summary>
        /// 编辑后台用户
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [Log("用户", Operate.编辑)]
        public int Edit(User item)
        {
            excute.Edit<User>(item);
             return excute.SaveChange(this, "Edit");
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Ids"></param>
        [Log("用户", Operate.删除)]
        public int Cancel(string Ids)
        {
            return excute.Cancel<User>(Ids,this, "Cancel");
        }

        /// <summary>
        /// 冻结用户
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [Log("用户", Operate.冻结解冻)]
        public int Freeze(string Ids,bool bState)
        {
            int type = 0;
            if (bState) type = 0;
            else type = 1;
            string sSql =string.Format("Update [User] Set bState={0} Where ID IN({1})", type, Ids);
            return excute.Excute(sSql, this, "Freeze");
        }
    }
}
