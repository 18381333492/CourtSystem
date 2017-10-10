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
        public override int Insert(ES_WeChatNewsName newsName, List<ES_WeChatNews> newsData)
        {
            newsName.ID = Guid.NewGuid();
            newsName.dInsertTime = DateTime.Now;
            newsName.dUpdateTime = DateTime.Now;
            newsName.bIsDeleted =false;
            excute.Add<ES_WeChatNewsName>(newsName);
            foreach(var item in newsData)
            {
                item.ID = Guid.NewGuid();
                item.sToId = newsName.ID;
                excute.Add<ES_WeChatNews>(item);
            }
            return excute.SaveChange();
        }


        /// <summary>
        /// 编辑图文消息
        /// </summary>
        /// <param name="newsName"></param>
        /// <param name="newsData"></param>
        /// <returns></returns>
        public override int Update(ES_WeChatNewsName newsName, List<ES_WeChatNews> newsData)
        {
            //查询出下面的图文数据
            var old_newsData = excute.Context.ES_WeChatNews.Where(m => m.sToId.ToString() == newsName.ID.ToString()).ToList();
            var newsName_item = excute.Context.ES_WeChatNewsName.AsNoTracking().FirstOrDefault(m => m.ID.ToString() == newsName.ID.ToString());

            foreach(var m in old_newsData)
            {   //删除以前的图文数据
                excute.Delete<ES_WeChatNews>(m);
            }
            newsName_item.sWeChatNewsName = newsName.sWeChatNewsName;
            newsName_item.dUpdateTime = DateTime.Now;
            excute.Edit<ES_WeChatNewsName>(newsName_item);
            foreach(var item in newsData)
            {
                item.ID = Guid.NewGuid();
                item.sToId = newsName_item.ID;
                excute.Add<ES_WeChatNews>(item);          
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
            var item= excute.Context.ES_WeChatNewsName.Find(new Guid(sNewsId));
            excute.Delete<ES_WeChatNewsName>(item);
            var newList = excute.Context.ES_WeChatNews.Where(m => m.sToId.ToString().ToLower() == sNewsId.ToLower()).ToList();
            foreach(var m in newList)
            {
                excute.Delete<ES_WeChatNews>(m);
            }
            var res= excute.SaveChange();
            return res;          
        }
    }
}
