using EFModel;
using EFModel.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json.Linq;

namespace Sevices
{
    public partial class RoleService : ServiceBase
    {

        /// <summary>
        /// 分页获取角色数据列表
        /// </summary>
        /// <param name="Info">分页参数</param>
        /// <param name="Params">查询参数</param>
        /// <returns></returns>
        public string GetList(PageInfo Info,Dictionary<string,object> Params)
        {
            return query.QueryPage(@"select * from [Role] where bIsDeleted=0",Info, null);
        }

        /// <summary>
        /// 获取角色数据
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public Role Get(Guid ID)
        {
            return query.db.Role.Find(ID);
        }

        /// <summary>
        /// 获取所有的角色名字列表
        /// </summary>
        /// <returns></returns>
        public string GetRoleNameList()
        {
            var entry = from m in query.db.Role
                        where m.bIsDeleted == false && m.IsDeveloper==false
                        select new
                        {
                            m.ID,
                            m.sRoleName
                        };
            return C_Json.toJson(entry);
        }

        /// <summary>
        /// 获取所有的菜单和菜单按钮
        /// </summary>
        /// <param name="ID">角色ID</param>
        /// <returns></returns>
        public string GetAllMenuAndButton(Guid ID)
        {

            /*
            * 获取二级菜单数据*
            */
            var childMenu = from m in query.db.Menus
                            where m.bIsDeleted == false && m.sParentMenuId != string.Empty
                            orderby m.iOrder ascending
                            select new
                            {
                                m.ID,
                                m.iOrder,
                                m.sMenuName,
                                m.sParentMenuId,
                            };

            List<string> ChildIds = childMenu.Select(m => m.sParentMenuId).ToList();

            /*
             * 获取一级菜单数据*
             */
            var menus = from m in query.db.Menus
                        where ChildIds.Contains(m.ID.ToString()) && m.bIsDeleted == false
                        orderby m.iOrder ascending
                        select new
                        {
                            m.ID,
                            m.iOrder,
                            m.sMenuName,
                            m.sParentMenuId,
                        };

            /*
             * 获取菜单按钮数据*
             */
            var button = from n in query.db.Button
                         orderby n.iOrder ascending
                         select new
                         {
                             n.ID,
                             n.sButtonName,
                             n.sToMenuId,
                         };

            /*
             * 根据角色ID获取角色权限*
             */

            var role = query.db.Role.Find(ID);

            JObject job = new JObject();
            job.Add(new JProperty("menu", C_Json.Array(C_Json.toJson(menus))));
            job.Add(new JProperty("childMenu", C_Json.Array(C_Json.toJson(childMenu))));
            job.Add(new JProperty("button", C_Json.Array(C_Json.toJson(button))));
            job.Add(new JProperty("power", role.sRolePower));
            return job.ToString();
        }
    }
}
