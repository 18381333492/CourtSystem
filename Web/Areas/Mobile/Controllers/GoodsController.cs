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
    /// 前段页面的商品控制器
    /// </summary>
    public class GoodsController : MobileBase<IGoods>
    {
        //
        // GET: /Mobile/Goods/

        /// <summary>
        /// 根据商品编号获取商品信息
        /// </summary>
        /// <param name="sGoodsNo"></param>
        [HttpPost]
        public void GetGoodsInfo(string sGoodsNo)
        {
            var goods= manage.GetGoodsInfoByGoodsNo(sGoodsNo);
            if (goods != null)
            {
                result.data = goods;
                result.success = true;
            }
            else
                result.info = "亲,该商品已失效不能购买!";
        }      
    }
}
