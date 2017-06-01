using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels;
using EFModels.MyModels;

namespace SystemInterface
{

    /// <summary>
    /// 微信自定义菜单接口
    /// </summary>
    public abstract class IWeChatMenu : BaseBll
    {
        #region 查询

        /// <summary>
        /// 分页获取微信自定义菜单数据列表
        /// </summary>
        /// <param name="info"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public abstract string PageList(PageInfo info, string searchText);

        /// <summary>
        /// 获取微信一级菜单数据列表
        /// </summary>
        /// <returns></returns>
        public abstract List<Dictionary<string, object>> GetMainMenuList();


        /// <summary>
        /// 根据主键ID获取微信自定义菜单
        /// </summary>
        /// <param name="sWeChatMenuId"></param>
        /// <returns></returns>
        public abstract CDELINK_WeChatMenu GetMenuById(string sWeChatMenuId);

        /// <summary>
        /// 检查微信自定义一级菜单不能超过3个
        /// </summary>
        /// <returns></returns>
        public abstract bool CheckMianMenuThree(string sWeChatMenuId=null);

        /// <summary>
        /// 检查微信自定义二级菜单不能超过5个
        /// </summary>
        /// <returns></returns>
        public abstract bool CheckChildMenuFive(string sWeChatMenuId = null);

        #endregion


        #region 操作

        /// <summary>
        /// 添加微信自定义菜单
        /// </summary>
        /// <param name="wechatMenu"></param>
        /// <returns></returns>
        public abstract int Insert(CDELINK_WeChatMenu wechatMenu);

        /// <summary>
        /// 编辑微信自定义菜单
        /// </summary>
        /// <param name="wechatMenu"></param>
        /// <returns></returns>
        public abstract int Update(CDELINK_WeChatMenu wechatMenu);


        /// <summary>
        /// 根据主键ID集合删除自定义菜单
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public abstract int Cancel(string Ids);

        #endregion



    }
}
