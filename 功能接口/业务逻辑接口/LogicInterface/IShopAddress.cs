using EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicHandlerInterface
{
   
    /// <summary>
    /// 收货地址接口
    /// </summary>
    public abstract class IShopAddress:BaseBll
    {

        /// <summary>
        /// 根据sOpenId获取会员的收货地址数据列表
        /// </summary>
        /// <param name="sOpenId"></param>
        /// <returns></returns>
        public abstract List<ES_ShopAddress> GetListBysOpenId(string sOpenId); 

        #region 操作

        /// <summary>
        /// 添加收货地址
        /// </summary>
        /// <param name="address"></param>
        public abstract int Insert(ES_ShopAddress address);


        /// <summary>
        /// 编辑收货地址
        /// </summary>
        /// <param name="address"></param>
        public abstract int Update(ES_ShopAddress address);


        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="sShopAddressId"></param>
        /// <returns></returns>
        public abstract int Delete(string sShopAddressId);

        #endregion

    }
}
