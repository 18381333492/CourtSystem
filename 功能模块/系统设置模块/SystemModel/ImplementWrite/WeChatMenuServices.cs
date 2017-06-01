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
    /// 网站的写操作相关
    /// </summary>
    public partial class WeChatMenuServices : IWeChatMenu
    {
        /// <summary>
        /// 添加微信自定义菜单
        /// </summary>
        /// <param name="wechatMenu"></param>
        /// <returns></returns>
        public override int Insert(CDELINK_WeChatMenu wechatMenu)
        {
            wechatMenu.ID = Guid.NewGuid();
            excute.Add<CDELINK_WeChatMenu>(wechatMenu);
            return excute.SaveChange();
        }

        /// <summary>
        /// 编辑微信自定义菜单
        /// </summary>
        /// <param name="wechatMenu"></param>
        /// <returns></returns>
        public override int Update(CDELINK_WeChatMenu wechatMenu)
        {
            excute.Edit<CDELINK_WeChatMenu>(wechatMenu);
            return excute.SaveChange();
        }


        /// <summary>
        /// 根据主键ID集合删除自定义菜单
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public override int Cancel(string Ids)
        {
            var res = excute.Cancel<CDELINK_WeChatMenu>(Ids);
            return res;
        }
    }
}
