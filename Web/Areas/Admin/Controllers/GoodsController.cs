using EFModel;
using EFModel.MyModels;
using Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start;

namespace Web.Areas.Admin.Controllers
{

    /// <summary>
    /// 商品控制器
    /// </summary>
    public class GoodsController : AdminBaseController<GoodsService>
    {
        //
        // GET: /Admin/Goods/

        /// <summary>
        /// 商品列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加商品视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 商品编辑视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(Guid sGoodsId)
        {
            return View(_server.Get(sGoodsId));
        }


        /// <summary>
        /// 分页获取商品数据列表
        /// </summary>
        /// <param name="Info"></param>
        /// <param name="Sta"></param>
        /// <param name="End"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo Info, string Sta, string End, string searchText)
        {
            return Content(_server.GetList(Info, Sta, End, searchText));
        }


        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public void Insert(Goods goods)
        {
            if (_server.Add(goods) > 0)
            {
                result.success = true;
                result.info = MessageTip.添加成功.ToString();
            }
            else
                result.info = MessageTip.添加失败.ToString();
        }

        /// <summary>
        /// 编辑商品
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public void Update(Goods goods)
        {
            if (_server.Edit(goods) > 0)
            {
                result.success = true;
                result.info = MessageTip.编辑成功.ToString();
            }
            else
                result.info = MessageTip.编辑失败.ToString();
        }


        /// <summary>
        /// 上/下架商品
        /// </summary>
        /// <param name="Ids"></param>
        public void AlterShelves(string Ids)
        {
            List<Guid> idList = Ids.Split(',').Select(m => new Guid(m)).ToList();
            if (_server.UnderCarriage(idList) > 0)
            {
                result.success = true;
                result.info = MessageTip.操作成功.ToString();
            }
            else
                result.info = MessageTip.操作失败.ToString();
        }

    }
}
