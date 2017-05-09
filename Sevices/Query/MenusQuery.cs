using EFModel;
using EFModel.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Sevices
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    public partial class MenusService: ServiceBase
    {
        public class MenuData
        {
            public string sMenuName;
            public List<Data> Menus=new List<Data>();
        }

        public class Data
        {
            public string sMenuName;
            public string sMenuLink;
        }

        /// <summary>
        /// 根据主键ID获取菜单实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Menus GetById(Guid ID)
        {
            return query.db.Menus.Find(ID);
        }

        /// <summary>
        /// 根据用户权限获取获取菜单列表数据
        /// </summary>
        /// <returns></returns>
        public string GetMenusList(Guid sRoleId)
        {
            var MainMenu = new List<MenuData>();
            //获取角色
            var childMenu = GetSecondMenus(sRoleId);//获取角色下面的所有二级菜单

            List<Guid> sParentMenuId = childMenu.Select(m => new Guid(m.sParentMenuId)).ToList();
            var menu = query.db.Menus.
                Where(m => sParentMenuId.Contains(m.ID) && m.bIsDeleted == false).
                OrderBy(m => m.iOrder).ToList();

            //组装菜单数据
            foreach (var m in menu)
            {
                var Menu = new MenuData();
                Menu.sMenuName = m.sMenuName;
                foreach (var k in childMenu)
                {
                    if (k.sParentMenuId.ToLower() == m.ID.ToString())
                    {
                        Menu.Menus.Add(new Data()
                        {
                            sMenuName = k.sMenuName,
                            sMenuLink = k.sMenuUrl
                        });
                    }
                }
                MainMenu.Add(Menu);
            }
            return C_Json.toJson(MainMenu);
        }

        /// <summary>
        /// 获取一级菜单数据
        /// </summary>
        /// <returns></returns>
        public string GetFirstMenus()
        {
            var entry = from m in query.db.Menus
                        where m.sParentMenuId == string.Empty && m.bIsDeleted == false
                        orderby m.iOrder
                        select new
                        {
                            id = m.ID,
                            text = m.sMenuName
                        };
            JArray array = C_Json.Array(C_Json.toJson(entry));
            JObject job = new JObject();
            job.Add(new JProperty("id", string.Empty));
            job.Add(new JProperty("text", "一级菜单"));
            array.AddFirst(job);
            return array.ToString();
        }

        /// <summary>
        /// 根据角色获取用户的二级菜单
        /// </summary>
        /// <param name="sRoleId"></param>
        /// <returns></returns>
        public List<Menus> GetSecondMenus(Guid sRoleId)
        {
            //获取角色
            var role = query.db.Role.Find(sRoleId);
            string sSql = string.Empty;
            var MenuIdAndButtonId = role.sRolePower.Split('|');
            string[] menuId = MenuIdAndButtonId[0].Split(',');
            if (!string.IsNullOrEmpty(menuId.First()))
            {
                menuId = menuId.Select(m => "'" + m + "'").ToArray();
                sSql = string.Format(@"SELECT * FROM [Menus] WHERE ID IN({0}) AND bIsDeleted=0 ORDER BY iOrder ", string.Join(",", menuId));
            }
            if (MenuIdAndButtonId.Length > 1)
            {
                string[] buttonId = MenuIdAndButtonId[1].Split(',');
                if (!string.IsNullOrEmpty(buttonId.First()))
                {
                    buttonId = buttonId.Select(m => "'" + m + "'").ToArray();
                    sSql = string.Format(@"SELECT * FROM [Menus]
                                           WHERE ID IN(SELECT sToMenuId FROM Button 
                                           WHERE ID IN({0}))
                                           OR ID IN({1}) AND bIsDeleted=0  ORDER BY iOrder ", string.Join(",", buttonId), string.Join(",", menuId));
                }
            }
            return query.Query<Menus>(sSql);//获取角色下面的所有二级菜单
        }
        /// <summary>
        /// 获取所有的菜单列表数据
        /// </summary>
        /// <returns></returns>
        public string GetList()
        {
            var entry = from m in query.db.Menus
                        where m.bIsDeleted == false && m.sParentMenuId == string.Empty
                        orderby m.iOrder
                        select m;

            var child = from m in query.db.Menus
                        where m.bIsDeleted == false && m.sParentMenuId != string.Empty
                        orderby m.iOrder
                        select m;

            JArray Main = new JArray();
            foreach (var m in entry)
            {
                var item = C_Json.Object(C_Json.toJson(m));
                JArray array = new JArray();
                foreach (var n in child)
                {
                    if (m.ID.ToString() == n.sParentMenuId)
                    {
                        var temp = C_Json.Object(C_Json.toJson(n));
                        array.Add(temp);
                    }
                }
                item.Add(new JProperty("children", array));
                Main.Add(item);
            }
            return Main.ToString();
        }


        /// <summary>
        /// 检查菜单名称是否重复或者路径重复
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="IsEdit">是否编辑的时候检查(默认值false)</param>
        /// <returns></returns>
        public bool Check(Menus menu, bool IsEdit = false)
        {
            List<Menus> list = null;
            if (string.IsNullOrEmpty(menu.sParentMenuId))
            {
                list = query.db.Menus
                   .Where(m => m.bIsDeleted == false)
                   .Where(m => m.sMenuName.Equals(menu.sMenuName))
                   .ToList();
            }
            else
            {
                list = query.db.Menus
                   .Where(m => m.bIsDeleted == false)
                   .Where(m => m.sMenuName.Equals(menu.sMenuName) || m.sMenuUrl.ToLower().Equals(menu.sMenuUrl.ToLower()))
                   .ToList();
            }
            if (IsEdit)
                return list.Count > 1 ? true : false;
            else
                return list.Count > 0 ? true : false;
        }
    }
}
