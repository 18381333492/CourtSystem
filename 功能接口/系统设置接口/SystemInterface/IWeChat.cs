using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels;
using EFModels.MyModels;

namespace SystemInterface
{
    public abstract class IWeChat : BaseBll
    {
        #region 查询


        /// <summary>
        /// 获取微信公众号信息
        /// </summary>
        /// <returns></returns>
        public abstract ES_WeChat GetWeChat();


        #endregion


        #region 操作

        /// <summary>
        /// 设置公众号信息
        /// </summary>
        /// <param name="wechat"></param>
        /// <returns></returns>
        public abstract int SetWeChat(ES_WeChat wechat);


        #endregion



    }
}
