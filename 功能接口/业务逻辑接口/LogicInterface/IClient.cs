using EFModels;
using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicHandlerInterface
{
    /// <summary>
    /// 会员逻辑接口
    /// </summary>
    public abstract class IClient : BaseBll
    {
        #region 查询

        /// <summary>
        /// 分页获取会员数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public abstract string List(PageInfo pageInfo, string searchText);

        /// <summary>
        /// 根据OpenId判断该会员是否存在
        /// </summary>
        /// <param name="sOpenId"></param>
        /// <returns></returns>
        public abstract bool IsExistByOpenId(string sOpenId);

        #endregion


        #region 操作

        /// <summary>
        /// 关注微信公众号注册会员
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public abstract int AddClient(ES_Client client);

        /// <summary>
        /// 再次关注修改会员关注状态
        /// </summary>
        /// <param name="sOpenId"></param>
        /// <returns></returns>
        public abstract int SubscribeEditClient(string sOpenId);

        /// <summary>
        /// 取消关注修改会员关注状态
        /// </summary>
        /// <param name="sOpenId"></param>
        /// <returns></returns>
        public abstract int UnSubscribeEditClient(string sOpenId);

        #endregion
    }
}
