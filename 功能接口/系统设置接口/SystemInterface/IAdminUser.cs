using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels;
using EFModels.MyModels;

namespace SystemInterface
{
    public abstract class IAdminUser: BaseBll
    {
        #region 查询


        /// <summary>
        /// 分页获取后台用户数据列表
        /// </summary>
        /// <param name="Info">分页参数</param>
        /// <param name="searchText">搜索的文本</param>
        /// <param name="iState">会员状态(默认全部)</param>
        /// <returns></returns>
        public abstract string PageList(PageInfo Info, string searchText, int iState);


        /// <summary>
        /// 验证公司后台管理员登录
        /// </summary>
        /// <param name="sUserName">用户名</param>
        /// <param name="sPassWord">登录密码</param>
        /// <returns></returns>
        public abstract ES_AdminUser ValidateLogin(string sUserName, string sPassWord);


        /// <summary>
        ///  微信扫码登录后台
        /// </summary>
        /// <param name="sOpenId"></param>
        /// <returns></returns>
        public abstract ES_AdminUser ScanLogin(string sOpenId);


        /// <summary>
        /// 是否已经绑定微信
        /// </summary>
        /// <param name="sOpenId"></param>
        /// <returns></returns>
        public abstract bool IsBingWeChat(string sOpenId);


        /// <summary>
        /// 根据管理员主键ID获取信息
        /// </summary>
        /// <param name="sUserId"></param>
        /// <returns></returns>
        public abstract ES_AdminUser GetById(string sUserId);

        /// <summary>
        /// 获取所有的角色名称
        /// </summary>
        /// <returns></returns>
        public abstract List<Dictionary<string, object>> GetAllRoleNameList();


        /// <summary>
        /// 检查登录账号是否重名
        /// </summary>
        /// <param name="sLoginAccout"></param>
        /// <returns></returns>
        public abstract bool CheckLoginAccout(string sLoginAccout);

        /// <summary>
        /// 根据用户角色获取相应的菜单和按钮
        /// </summary>
        /// <param name="sRoleId"></param>
        /// <returns></returns>
        public abstract MenuAndButton GetMenuAndButtonByRoleId(string sRoleId);
     

        #endregion


        #region 操作

        /// <summary>
        /// 添加后台用户
        /// </summary>
        /// <param name="adminUser"></param>
        /// <returns></returns>
        public abstract int Insert(ES_AdminUser adminUser);

        /// <summary>
        /// 设置后台用户角色
        /// </summary>
        /// <param name="adminUser"></param>
        /// <returns></returns>
        public abstract int SetRole(Guid ID,string sRoleId);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public abstract int AlertPwd(Guid ID, string sPassword);

        /// <summary>
        /// 设置账户信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="sAccount">账户信息</param>
        /// <param name="sPassword">密码</param>
        /// <returns></returns>
        public abstract int SetAccount(Guid ID,string sAccount,string sPassword);

        /// <summary>
        /// 扫码登录同步用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public abstract int SyncUserInfo(ES_AdminUser user);

        /// <summary>
        /// 根据主键ID集合删除后台用户
        /// </summary>
        /// <param name="Ids"></param>
        public abstract int Cancel(string Ids);


        /// <summary>
        /// 根据主键ID冻结/解冻后台用户
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public abstract int Freeze(string ID);

        #endregion

    }
}
