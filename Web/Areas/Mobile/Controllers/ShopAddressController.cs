using EFModels;
using LogicHandlerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start.BaseController;

namespace Web.Areas.Mobile.Controllers
{

    /// <summary>
    /// 会员收货地址控制器
    /// </summary>
    public class ShopAddressController :MobileBase<IShopAddress>
    {
        //
        // GET: /Mobile/ShopAddress/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 添加收货地址
        /// </summary>
        /// <param name="address"></param>
        public void Insert(ES_ShopAddress address)
        {
            if (manage.Insert(address) > 0)
                result.success = true;
        }

        /// <summary>
        /// 编辑收货地址
        /// </summary>
        /// <param name="address"></param>
        public void Update(ES_ShopAddress address)
        {
            if (manage.Update(address) > 0)
                result.success = true;
        }

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="sAddressId"></param>
        public void Delete(string sShopAddressId)
        {
            if (manage.Delete(sShopAddressId) > 0)
                result.success = true;
        }
    }
}
