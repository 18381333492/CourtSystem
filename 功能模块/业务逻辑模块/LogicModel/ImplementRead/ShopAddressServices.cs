using LogicHandlerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels;

namespace LogicHandlerModel
{
    /// <summary>
    /// 会员收货地址的读操作
    /// </summary>
    public partial class ShopAddressServices: IShopAddress
    {

        /// <summary>
        /// 根据sOpenId获取会员的收货地址数据列表
        /// </summary>
        /// <param name="sOpenId"></param>
        /// <returns></returns>
        public override List<ES_ShopAddress> GetListBysOpenId(string sOpenId)
        {
            var list = query.QueryList<ES_ShopAddress>("select * from ES_ShopAddress where sOpenId=@sOpenId", 
                                    new { sOpenId = sOpenId }).ToList();
            return list;
        }
    }
}
