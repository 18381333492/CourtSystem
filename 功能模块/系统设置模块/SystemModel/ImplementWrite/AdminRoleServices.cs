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
    public partial class AdminRoleServices : IAdminRole
    {
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="adminRole"></param>
        /// <returns></returns>
        public override int Insert(CDELINK_AdminRole adminRole)
        {
            adminRole.dInsertTime = DateTime.Now;
            excute.Add<CDELINK_AdminRole>(adminRole);
            return excute.SaveChange();
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="adminRole"></param>
        /// <returns></returns>
        public override int Update(CDELINK_AdminRole adminRole)
        {
            excute.Edit<CDELINK_AdminRole>(adminRole);
            return excute.SaveChange();
        }

    }
}
