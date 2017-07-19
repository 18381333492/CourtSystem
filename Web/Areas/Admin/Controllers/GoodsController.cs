using EFModels;
using EFModels.MyModels;
using LogicHandlerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start.BaseController;

namespace Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 商品控制器
    /// </summary>
    public class GoodsController : AdminBase<IGoods>
    {
        //
        // GET: /Admin/Goods/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(string sGoodsId)
        {
            return View(manage.Get(sGoodsId));
        }

        /// <summary>
        /// 设置优惠价格视图
        /// </summary>
        /// <param name="sGoodsId"></param>
        /// <returns></returns>
        public ActionResult SetPrices(string sGoodsId)
        {
            return View(manage.Get(sGoodsId));
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo pageInfo,string searchText)
        {
            return Content(manage.List(pageInfo, searchText));
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="goods"></param>
        public void Insert(ES_Goods goods)
        {
            if (manage.Insert(goods) > 0)
                result.success = true;
        }

        /// <summary>
        /// 编辑商品
        /// </summary>
        /// <param name="goods"></param>
        public void Update(ES_Goods goods)
        {
            if (manage.Update(goods) > 0)
                result.success = true;
        }

        /// <summary>
        /// 设置优惠价格
        /// </summary>
        /// <param name="goods"></param>
        public void SetDisPrices(ES_Goods goods)
        {
            if (manage.SetDisPrices(goods.ID.ToString(),goods.sDisPrices,goods.IsRateGiving) > 0)
                result.success = true;
        }
    }
}
