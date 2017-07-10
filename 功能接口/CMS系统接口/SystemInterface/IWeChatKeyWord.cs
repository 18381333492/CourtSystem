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
    /// 微信关键字接口
    /// </summary>
    public abstract class IWeChatKeyWord : BaseBll
    {
        #region 查询

        /// <summary>
        /// 分页获取微信关键字数据列表
        /// </summary>
        /// <param name="info"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public abstract string PageList(PageInfo info, string searchText);

        /// <summary>
        /// 根据主键ID获取微信关键字
        /// </summary>
        /// <param name="sWeChatKeyWordId"></param>
        /// <returns></returns>
        public abstract CDELINK_WeChatKeyWord GetById(string sWeChatKeyWordId);

        /// <summary>
        /// 根据关键字查找关键字
        /// </summary>
        /// <param name="sWeChatKeyWord"></param>
        /// <returns></returns>
        public abstract CDELINK_WeChatKeyWord GetByKeyWord(string sWeChatKeyWord);

        #endregion


        #region 操作

        /// <summary>
        /// 添加微信关键字
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public abstract int Insert(CDELINK_WeChatKeyWord keyWord);


        /// <summary>
        /// 编辑微信关键字
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public abstract int Update(CDELINK_WeChatKeyWord keyWord);

        #endregion



    }
}
