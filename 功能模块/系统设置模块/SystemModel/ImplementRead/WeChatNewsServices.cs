using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterface;
using EFModels;
using Common;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SystemModel
{
    /// <summary>
    /// 微信图文素材的读操作相关
    /// </summary>
    public partial class WeChatNewsServices : IWeChatNews
    {
        /// <summary>
        /// 获取所有的图文素材列表
        /// </summary>
        /// <returns></returns>
        public override object GetNewsList(string searchText)
        {

            StringBuilder sSql = new StringBuilder();
            if(!string.IsNullOrEmpty(searchText))
            sSql.AppendFormat(@"select * from CDELINK_WeChatNewsName where bIsDeleted=0
                                                               and  sWeChatNewsName like '%{0}%'
                                                                 order by dUpdateTime desc", searchText);
            else
            sSql.Append(@"select * from CDELINK_WeChatNewsName where bIsDeleted=0 order by dUpdateTime desc");

            var nameList = query.QueryList<CDELINK_WeChatNewsName>(sSql.ToString()).ToList();
   
            var newsList = query.QueryList<CDELINK_WeChatNews>(@"select ID,sTitle,sDescribe,sPictureUrl,iOrder,sToId from CDELINK_WeChatNews order by iOrder");

            JArray array = new JArray();

            foreach (var item in nameList)
            {
                JObject job = new JObject();
                job.Add(new JProperty("ID", item.ID));
                job.Add(new JProperty("sWeChatNewsName", item.sWeChatNewsName));
                job.Add(new JProperty("dUpdateTime", item.dUpdateTime));
                job.Add(new JProperty("newsList",JsonConvert.SerializeObject(newsList.Where(m => m.sToId.ToString().ToLower() == item.ID.ToString().ToLower()))));
                array.Add(job);
            }
            return array;
        }



        /// <summary>
        /// 获取单条图文信息
        /// </summary>
        /// <param name="sNewsId"></param>
        public override object GetNews(string sNewsId)
        {
            //获取微信图文信息
            var weChatNewsName = query.Find<CDELINK_WeChatNewsName>(sNewsId);
            //获取微信详细图文数据列表
            var newsList = query.QueryList<CDELINK_WeChatNews>(@"select * from CDELINK_WeChatNews where sToId=@sToId order by iOrder",new { sToId= sNewsId });

            JObject job = new JObject();
            job.Add(new JProperty("ID", weChatNewsName.ID));
            job.Add(new JProperty("sWeChatNewsName", weChatNewsName.sWeChatNewsName));
            job.Add(new JProperty("newsList", JsonConvert.SerializeObject(newsList)));
            return job;
        }
    }
}
