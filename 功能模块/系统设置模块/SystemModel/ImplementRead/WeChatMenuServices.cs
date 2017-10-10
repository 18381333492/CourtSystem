using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterface;
using EFModels;
using Common;

namespace SystemModel
{
    /// <summary>
    /// 网站的读操作相关
    /// </summary>
    public partial class WeChatMenuServices : IWeChatMenu
    {
        /// <summary>
        /// 分页获取微信自定义菜单数据列表
        /// </summary>
        /// <param name="info"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public override string PageList(PageInfo pageInfo, string searchText)
        {
            pageInfo.order = OrderType.DESC;
            pageInfo.sort = "iOrder";
            StringBuilder sSql = new StringBuilder();
            sSql.Append(@"SELECT A.*,B.sMenuName AS ParentMenuName FROM ES_WeChatMenu AS A 
                                     LEFT JOIN  ES_WeChatMenu AS B ON A.sParentMenuId=B.ID WHERE A.bIsDeleted=0");

            //条件查询
            if (!string.IsNullOrEmpty(searchText))
            {
                sSql.AppendFormat("AND (A.sMenuName  LIKE '%{0}%')", searchText);
            }
            var userList = query.PageQuery(sSql.ToString(), pageInfo);
            return userList.toJson();
        }


        /// <summary>
        /// 获取微信一级菜单数据列表
        /// </summary>
        /// <returns></returns>
        public override List<Dictionary<string, object>> GetMainMenuList()
        {
            var menulist = query.QueryList<Dictionary<string, object>>(@"SELECT ID,sMenuName 
                                                                   FROM ES_WeChatMenu WHERE bIsDeleted=0").ToList();
            var temp = new Dictionary<string, object>();
            temp["ID"] = string.Empty;
            temp["sMenuName"] = "一级菜单";
            menulist.Add(temp);
            return menulist;
        }


        /// <summary>
        /// 根据主键ID获取微信自定义菜单
        /// </summary>
        /// <param name="sWeChatMenuId"></param>
        /// <returns></returns>
        public override ES_WeChatMenu GetMenuById(string sWeChatMenuId)
        {
            return query.Find<ES_WeChatMenu>(sWeChatMenuId);
        }


        /// <summary>
        /// 检查微信自定义一级菜单不能超过3个
        /// </summary>
        /// <returns></returns>
        public override bool CheckMianMenuThree(string sWeChatMenuId = null)
        {
            var res = false;
            if (string.IsNullOrEmpty(sWeChatMenuId))
            {
                var list = query.QueryList<ES_WeChatMenu>(@"SELECT * FROM ES_WeChatMenu WHERE sParentMenuId IS NULL AND bIsDeleted=0");
                if (list.Count >= 3)
                    res = true;
            }
            else
            {
                var list = query.QueryList<ES_WeChatMenu>(@"SELECT * FROM ES_WeChatMenu 
                                                                    WHERE sParentMenuId IS NULL AND bIsDeleted=0 AND ID!='"+ sWeChatMenuId + "' ");
                if (list.Count >= 3)
                    res = true;
            }
            return res;
        }


        /// <summary>
        /// 检查微信自定义二级菜单不能超过5个
        /// </summary>
        /// <returns></returns>
        public override bool CheckChildMenuFive(string sWeChatMenuId = null)
        {
            var res = false;
            if (string.IsNullOrEmpty(sWeChatMenuId))
            {
                var list = query.QueryList<ES_WeChatMenu>(@"SELECT * FROM ES_WeChatMenu WHERE sParentMenuId IS NOT NULL AND bIsDeleted=0");
                if (list.Count >= 5)
                    res = true;
            }
            else
            {
                var list = query.QueryList<ES_WeChatMenu>(@"SELECT * FROM ES_WeChatMenu 
                                                                             WHERE sParentMenuId IS NOT NULL AND bIsDeleted=0 AND ID!='"+ sWeChatMenuId + "'");
                if (list.Count >= 5)
                    res = true;
            }
            return res;
        }


        /// <summary>
        /// 获取所有的微信自定义菜单
        /// </summary>
        /// <returns></returns>
        public override List<ES_WeChatMenu> GetAllList()
        {
            return query.QueryList<ES_WeChatMenu>("SELECT * FROM ES_WeChatMenu WHERE bIsDeleted=0").ToList();
        }
    }
}
