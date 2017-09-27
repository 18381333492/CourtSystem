
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels;
using Newtonsoft.Json.Linq;
using Common;
using SystemInterface;

namespace SystemModel
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
                var item = C_Json.ParseObject(C_Json.toJson(m));
                JArray array = new JArray();
                foreach (var n in childList)
                {
                    if (m.ID.ToString() == n.sParentMenuId)
                    {
                        var temp = C_Json.ParseObject(C_Json.toJson(n));
                        array.Add(temp);
                    }
                }
                item.Add(new JProperty("children", array));
                Main.Add(item);
            }
            return Main.ToString();
        }

        /// <summary>
        /// 获取所有的菜单数据和按钮数据
        /// </summary>
        /// <returns></returns>
        public override object GetAllMenuAndButtonList()
        {
            var menuList = query.QueryList<CDELINK_Menu>(@"SELECT * FROM CDELINK_Menu WHERE bIsDeleted=0 ORDER BY iOrder");
            var buttonList = query.QueryList<CDELINK_Button>(@"SELECT * FROM CDELINK_Button ORDER BY iOrder");
            return new
            {
                menu = menuList,
                button= buttonList
            };
        }


        /// <summary>
        /// 获取所有的一级菜单
        /// </summary>
        /// <returns></returns>
        public override List<Dictionary<string, object>> GetMianMenuList()
        {
            var list = query.QueryList<Dictionary<string, object>>(@"SELECT ID,sMenuName 
                                                            FROM CDELINK_Menu WHERE bIsDeleted=0 AND sParentMenuId IS NULL ORDER BY iOrder ASC").ToList();
            var temp = new Dictionary<string, object>();
            temp["ID"] = string.Empty;
            temp["sMenuName"] = "一级菜单";
            list.Insert(0, temp);
            return list;
        }


        /// <summary>
        ///  根据菜单名称检查是否有重名的菜单
        /// </summary>
        /// <param name="sMenuName">菜单名称</param>
        /// <param name="sMenuId">菜单的主键ID</param>
        /// <returns></returns>
        public override bool CheckMenuName(string sMenuName, string sMenuId = null)
        {
            if (string.IsNullOrEmpty(sMenuId))
                return query.Any(@"SELECT * FROM CDELINK_Menu WHERE bIsDeleted=0 AND sMenuName=@sMenuName", new { sMenuName = sMenuName });
            else
                return query.Any(@"SELECT * FROM CDELINK_Menu WHERE bIsDeleted=0 AND sMenuName=@sMenuName AND ID!=@ID", new { sMenuName = sMenuName, ID = sMenuId });
        }


        /// <summary>
        /// 根据菜单主键IDs集合获取菜单数据
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public override List<CDELINK_Menu> GetMainMenuByIds(string Ids)
        {
            if (!string.IsNullOrEmpty(Ids))
                return query.QueryList<CDELINK_Menu>(string.Format(@"SELECT * FROM CDELINK_Menu WHERE ID IN({0}) AND bIsDeleted=0 ORDER BY iOrder", Ids)).ToList();
            else
                return null;
        }


        /// <summary>
        /// 获取所有的二级菜单（SuperMan专用通道）
        /// </summary>
        /// <returns></returns>
        public override List<CDELINK_Menu> GetAllChildMenu()
        {
            return query.QueryList<CDELINK_Menu>(@"SELECT * FROM CDELINK_Menu WHERE bIsDeleted=0 AND sParentMenuId IS NOT NULL ORDER BY iOrder").ToList();
        }
    }
}
