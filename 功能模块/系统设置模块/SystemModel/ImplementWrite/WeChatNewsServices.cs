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


        /// <summary>
        /// 编辑图文消息
        /// </summary>
        /// <param name="newsName"></param>
        /// <param name="newsData"></param>
        /// <returns></returns>
        public override int Update(CDELINK_WeChatNewsName newsName, List<CDELINK_WeChatNews> newsData)
        {
            //查询出下面的图文数据
            var old_newsData = excute.Context.CDELINK_WeChatNews.Where(m => m.sToId.ToString() == newsName.ID.ToString()).ToList();
            var newsName_item = excute.Context.CDELINK_WeChatNewsName.AsNoTracking().FirstOrDefault(m => m.ID.ToString() == newsName.ID.ToString());

            foreach(var m in old_newsData)
            {   //删除以前的图文数据
                excute.Delete<CDELINK_WeChatNews>(m);
            }
            newsName_item.sWeChatNewsName = newsName.sWeChatNewsName;
            newsName_item.dUpdateTime = DateTime.Now;
            excute.Edit<CDELINK_WeChatNewsName>(newsName_item);
            foreach(var item in newsData)
            {
                item.ID = Guid.NewGuid();
                item.sToId = newsName_item.ID;
                excute.Add<CDELINK_WeChatNews>(item);          
            }
            var res = excute.SaveChange();
            return res;
        }

        /// <summary>
        /// 根据ID删除图文信息
        /// </summary>
        /// <param name="sNewsId"></param>
        /// <returns></returns>
        public override int Cancel(string sNewsId)
        {
            var item= excute.Context.CDELINK_WeChatNewsName.Find(new Guid(sNewsId));
            excute.Delete<CDELINK_WeChatNewsName>(item);
            var newList = excute.Context.CDELINK_WeChatNews.Where(m => m.sToId.ToString().ToLower() == sNewsId.ToLower()).ToList();
            foreach(var m in newList)
            {
                excute.Delete<CDELINK_WeChatNews>(m);
            }
            var res= excute.SaveChange();
            return res;          
        }
    }
}
