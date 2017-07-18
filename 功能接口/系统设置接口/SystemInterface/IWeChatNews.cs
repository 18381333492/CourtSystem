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
    /// 微信图文信息素材接口
    /// </summary>
    public abstract class IWeChatNews : BaseBll
    {
        #region 查询

        /// <summary>
        /// 获取所有的图文素材列表
        /// </summary>
        /// <returns></returns>
        public abstract object GetNewsList(string searchText);


        /// <summary>
        /// 获取单条图文信息
        /// </summary>
        /// <param name="sNewsId"></param>
        public abstract object GetNews(string sNewsId);

        #endregion


        #region 操作

        /// <summary>
        /// 添加图文消息
        /// </summary>
        /// <param name="newsName"></param>
        /// <param name="newsData"></param>
        /// <returns></returns>
        public abstract int Insert(CDELINK_WeChatNewsName newsName, List<CDELINK_WeChatNews> newsData);


        /// <summary>
        /// 编辑图文消息
        /// </summary>
        /// <param name="newsName"></param>
        /// <param name="newsData"></param>
        /// <returns></returns>
        public abstract int Update(CDELINK_WeChatNewsName newsName, List<CDELINK_WeChatNews> newsData);


        /// <summary>
        /// 根据ID删除图文信息
        /// </summary>
        /// <param name="sNewsId"></param>
        /// <returns></returns>
        public abstract int Cancel(string sNewsId);

        #endregion

    }
}
