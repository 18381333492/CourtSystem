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
    /// 后台管理员的读操作相关
    /// </summary>
    public partial class AdminUserServices : IAdminUser
    {
        /// <summary>
        /// 分页获取后台用户数据列表
        /// </summary>
        /// <param name="Info">分页参数</param>
        /// <param name="searchText">搜索的文本</param>
        /// <param name="iState">会员状态(默认全部)</param>
        /// <returns></returns>
        public override string PageList(PageInfo pageInfo, string searchText, int iState)
        {
            pageInfo.order = OrderType.DESC;
            pageInfo.sort = "dInsertTime";
            StringBuilder sSql = new StringBuilder();
            sSql.Append(@"SELECT A.*,B.sRoleName FROM ES_AdminUser AS A 
                                        LEFT JOIN ES_AdminRole AS B 
                                        ON A.sRoleId=B.ID WHERE A.bIsDeleted=0 ");
            if (!string.IsNullOrEmpty(searchText))
            {
                sSql.AppendFormat("AND (A.sPhone LIKE '%{0}%' OR A.sName  LIKE '%{0}%')", searchText);
            }

            var userList = query.PageQuery(sSql.ToString(), pageInfo);
            return userList.toJson();
        }

        /// <summary>
        /// 验证公司后台管理员登录
        /// </summary>
        /// <param name="sUserName">用户名</param>
        /// <param name="sPassWord">登录密码</param>
        /// <returns></returns>
        public override ES_AdminUser ValidateLogin(string sLoginAccout, string sPassWord)
        {
            var user = query.SingleQuery<ES_AdminUser>(@"SELECT * FROM ES_AdminUser 
                                                                WHERE sLoginAccout=@sLoginAccout AND sPassWord=@sPassWord AND bIsDeleted=0", new
            {
                sLoginAccout = sLoginAccout,
                sPassWord = C_Security.MD5(sPassWord)
            });
            return user;
        }

        /// <summary>
        /// 微信扫码验证登录
        /// </summary>
        /// <param name="sOpenId"></param>
        /// <returns></returns>
        public override ES_AdminUser ScanLogin(string sOpenId)
        {
            var user = query.SingleQuery<ES_AdminUser>(@"SELECT * FROM ES_AdminUser 
                                                                WHERE sOpenId=@sOpenId AND bIsDeleted=0", new
            {
                sOpenId = sOpenId
            });
            return user;
        }

        /// <summary>
        /// 根据管理员主键ID获取信息
        /// </summary>
        /// <param name="sUserId"></param>
        /// <returns></returns>
        public override ES_AdminUser GetById(string sUserId)
        {
            return query.SingleQuery<ES_AdminUser>("SELECT * FROM ES_AdminUser WHERE ID=@ID", new { ID = sUserId });
        }


        /// <summary>
        /// 获取所有的角色名称
        /// </summary>
        /// <returns></returns>
        public override List<Dictionary<string, object>> GetAllRoleNameList()
        {
            return query.QueryList<Dictionary<string, object>>(@"SELECT ID,sRoleName
                                                    FROM ES_AdminRole ORDER BY dInsertTime").ToList();
        }


        /// <summary>
        /// 检查登录账号是否重名
        /// </summary>
        /// <param name="sLoginAccout"></param>
        /// <returns></returns>
        public override bool CheckLoginAccout(string sLoginAccout,string sAdminUserId=null)
        {
            if (string.IsNullOrEmpty(sAdminUserId))
            {
                return query.Any(string.Format(@"SELECT * FROM ES_AdminUser WHERE bIsDeleted=0 AND sLoginAccout='{0}'", sLoginAccout));
            }
            else
            {
                return query.Any(string.Format(@"SELECT * FROM ES_AdminUser WHERE bIsDeleted=0 AND sLoginAccout='{0}' AND ID!='{1}'",
                    sLoginAccout, sAdminUserId));
            }
        }


        /// <summary>
        /// 根据用户角色获取相应的菜单和按钮
        /// </summary>
        /// <param name="sRoleId"></param>
        /// <returns></returns>
        public override MenuAndButton GetMenuAndButtonByRoleId(string sRoleId)
        {
            MenuAndButton data = new MenuAndButton();
            var adminRole = query.Find<ES_AdminRole>(sRoleId);
            if (!string.IsNullOrEmpty(adminRole.sPowerIds))
            {
                var menuIdsArray = adminRole.sPowerIds.Split('|')[0].Split(',').Select(m => { return "'" + m + "'"; });
                var buttonIdsArray = adminRole.sPowerIds.Split('|')[1].Split(',').Select(m => { return "'" + m + "'"; });

                string menuIds = string.Join(",", menuIdsArray);
                string buttonIds = string.Join(",", buttonIdsArray);

                data.menuList = query.QueryList<ES_Menu>(string.Format(@"SELECT * FROM ES_Menu WHERE bIsDeleted=0 AND ID IN({0})", menuIds)).ToList();
                data.buttonList = query.QueryList<ES_Button>(string.Format(@"SELECT * FROM ES_Button WHERE ID IN({0})", buttonIds)).ToList();                
            }
            return data;
        }
    }
}
