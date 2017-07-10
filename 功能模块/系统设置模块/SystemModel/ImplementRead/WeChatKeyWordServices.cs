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
    /// 微信关键字的读操作相关
    /// </summary>
    public partial class WeChatKeyWordServices : IWeChatKeyWord
    {
        /// <summary>
        /// 分页获取微信关键字数据列表
        /// </summary>
        /// <param name="info"></param>
        /// <param name="searchText"></param>
        public override string PageList(PageInfo pageInfo, string searchText)
        {
            pageInfo.order = OrderType.DESC;
            pageInfo.sort = "dUpdateTime";
            StringBuilder sSql = new StringBuilder();
            sSql.Append(@"SELECT * FROM CDELINK_WeChatKeyWord WHERE 1=1");

            //条件查询
            if (!string.IsNullOrEmpty(searchText))
            {
                sSql.AppendFormat("AND searchText LIKE '%{0}%'", searchText);
            }
            var userList = query.PageQuery<Dictionary<string,object>>(sSql.ToString(), pageInfo);
            return userList.toJson();
        }


        /// <summary>
        /// 根据主键ID获取微信关键字
        /// </summary>
        /// <param name="sWeChatKeyWordId"></param>
        /// <returns></returns>
        public override CDELINK_WeChatKeyWord GetById(string sWeChatKeyWordId)
        {
            return  query.Find<CDELINK_WeChatKeyWord>(sWeChatKeyWordId);
        }


        /// <summary>
        /// 根据关键字查找关键字
        /// </summary>
        /// <param name="sWeChatKeyWord"></param>
        /// <returns></returns>
        public override CDELINK_WeChatKeyWord GetByKeyWord(string sWeChatKeyWord)
        {
            return query.SingleQuery<CDELINK_WeChatKeyWord>("select * from CDELINK_WeChatKeyWord where sKeyWordName=@sKeyWordName", new
            {
                sKeyWordName = sWeChatKeyWord
            });
        }
    }
}
