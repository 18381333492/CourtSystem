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
    /// 会员收货地址的写操作
    /// </summary>
    public partial class ShopAddressServices: IShopAddress
    {

        /// <summary>
        /// 添加收货地址
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public override int Insert(ES_ShopAddress address)
        {
            address.ID = Guid.NewGuid();
            excute.Add<ES_ShopAddress>(address);
            if (address.bIsDefault)
            {
                var addressList = excute.Context.ES_ShopAddress.AsNoTracking().Where(m => m.sOpenId == address.sOpenId);
                foreach(var addr in addressList)
                {
                    addr.bIsDefault = false;
                    excute.Edit<ES_ShopAddress>(addr);
                }
            }
            return excute.SaveChange();
        }


        /// <summary>
        /// 编辑收货地址
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public override int Update(ES_ShopAddress address)
        {
            excute.Edit<ES_ShopAddress>(address);
            if (address.bIsDefault)
            {
                var addressList = excute.Context.ES_ShopAddress.AsNoTracking().Where(m => m.sOpenId == address.sOpenId&&m.ID!=address.ID);
                foreach (var addr in addressList)
                {
                    addr.bIsDefault = false;
                    excute.Edit<ES_ShopAddress>(addr);
                }
            }
            return excute.SaveChange();
        }


        /// <summary>
        /// 根据主键ID删除收货地址
        /// </summary>
        /// <param name="sShopAddressId"></param>
        /// <returns></returns>
        public override int Delete(string sShopAddressId)
        {
           return excute.Delete<ES_ShopAddress>(sShopAddressId);
        }
    }
}
