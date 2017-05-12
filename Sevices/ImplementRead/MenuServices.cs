using Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels;
using Newtonsoft.Json.Linq;
using Common;

namespace Sevices
{
    public partial class MenuServices : IMenu
    {
        /// <summary>
        /// 根据菜单主键ID获取菜单
        /// </summary>
        /// <param name="sButtonId"></param>
        /// <returns></returns>
        public override CDELINK_Menu GetById(string sMenuId)
        {
            var menu = query.SingleQuery<CDELINK_Menu>(@"SELECT * FROM CDELINK_Menu WHERE ID=@ID", new { ID = sMenuId });
            return menu;
        }

        /// <summary>
        /// 获取所有的菜单列表
        /// </summary>
        /// <returns></returns>
        public override string List()
        {
            var menuList = query.QueryList<CDELINK_Menu>(@"SELECT * FROM CDELINK_Menu WHERE bIsDeleted=0 ORDER BY iOrder ASC");

            var parentList = menuList.Where(m =>string.IsNullOrEmpty(m.sParentMenuId) == true); //一级菜单
            var childList = menuList.Where(m => string.IsNullOrEmpty(m.sParentMenuId) == false);//二级菜单

            //遍历菜单组装数据
            JArray Main = new JArray();
            foreach (var m in parentList)
            {
                var item = C_Json.Object(C_Json.toJson(m));
                JArray array = new JArray();
                foreach (var n in childList)
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
    }
}
