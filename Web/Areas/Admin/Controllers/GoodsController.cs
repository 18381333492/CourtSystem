using EFModels;
using EFModels.MyModels;
using LogicHandlerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start.BaseController;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
        /// 设置积分视图
        /// </summary>
        /// <param name="sGoodsId"></param>
        /// <returns></returns>
        public ActionResult Integral(string sGoodsId)
        {
            return View(manage.Get(sGoodsId));
        }

        /// <summary>
        /// 设置商品规格视图
        /// </summary>
        /// <param name="sGoodsId"></param>
        /// <returns></returns>
        public ActionResult Standard(string sGoodsId)
        {
            ViewBag.sGoodsId = sGoodsId;
            var manageStandard = Resolve<IGoodsStandard>();
            return View(manageStandard.GetList(sGoodsId));
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo pageInfo,string searchText,int bIsShelves=-1,int iGoodsType=-1)
        {
            return Content(manage.List(pageInfo, searchText, bIsShelves, iGoodsType));
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

        /// <summary>
        /// 设置积分
        /// </summary>
        /// <param name="goods"></param>
        public void SetIntegral(ES_Goods goods)
        {
            if (manage.SetIntegral(goods.ID.ToString(), goods.iIntegral, goods.IsRateAdd) > 0)
                result.success = true;
        }

        /// <summary>
        /// 设置商品规格
        /// </summary>
        /// <param name="list"></param>
        public void SetStandard(string list)
        {
            var List = JsonConvert.DeserializeObject<List<ES_GoodsStandard>>(list);
            var manageStandard = Resolve<IGoodsStandard>();
            if (manageStandard.SetStandard(List) > 0)
                result.success = true;
        }

        /// <summary>
        /// 商品的上下架
        /// </summary>
        /// <param name="sGoodsId"></param>
        public void Shelves(string sGoodsId)
        {
            if (manage.Shelves(sGoodsId)>0)
                 result.success = true;
        }

        /// <summary>
        /// 商品活动的开启与禁用
        /// </summary>
        /// <param name="sGoodsId"></param>
        public void Activity(string sGoodsId)
        {
            if (manage.Activity(sGoodsId) > 0)
                result.success = true;
        }

    }
}
