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
        public abstract CDELINK_AdminUser ValidateLogin(string sUserName, string sPassWord);


        /// <summary>
        /// 根据管理员主键ID获取信息
        /// </summary>
        /// <param name="sUserId"></param>
        /// <returns></returns>
        public abstract CDELINK_AdminUser GetById(string sUserId);

        #endregion


        #region 操作

        /// <summary>
        /// 添加后台用户
        /// </summary>
        /// <param name="adminUser"></param>
        /// <returns></returns>
        public abstract int Insert(CDELINK_AdminUser adminUser);

        /// <summary>
        /// 编辑后台用户
        /// </summary>
        /// <param name="adminUser"></param>
        /// <returns></returns>
        public abstract int Update(CDELINK_AdminUser adminUser);


        #endregion

    }
}
