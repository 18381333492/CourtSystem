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
    /// 微信图文素材的写操作相关
    /// </summary>
    public partial class WeChatNewsServices : IWeChatNews
    {
        /// <summary>
        /// 添加图文消息
        /// </summary>
        /// <param name="newsName"></param>
        /// <param name="newsData"></param>
        /// <returns></returns>
        public override int Insert(CDELINK_WeChatNewsName newsName, List<CDELINK_WeChatNews> newsData)
        {
            newsName.ID = Guid.NewGuid();
            newsName.dInsertTime = DateTime.Now;
            newsName.dUpdateTime = DateTime.Now;
            newsName.bIsDeleted =false;
            excute.Add<CDELINK_WeChatNewsName>(newsName);
            foreach(var item in newsData)
            {
                item.ID = Guid.NewGuid();
                item.sToId = newsName.ID;
                excute.Add<CDELINK_WeChatNews>(item);
            }
            return excute.SaveChange();
        }
    }
}
