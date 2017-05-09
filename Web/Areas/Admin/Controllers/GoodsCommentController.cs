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
    /// 商品评论控制器
    /// </summary>
    public class GoodsCommentController : AdminBaseController<GoodsCommentService>
    {
        //
        // GET: /Admin/GoodsComment/

        /// <summary>
        /// 商品评论数据列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加评论视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 编辑评论视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(Guid sGoodsCommentId)
        {
            return View(_server.Get(sGoodsCommentId));
        }

        /// <summary>
        /// 评论详情查看视图
        /// </summary>
        /// <param name="sGoodsCommentId"></param>
        /// <returns></returns>
        public ActionResult Detail(Guid sGoodsCommentId)
        {
            return View(_server.Get(sGoodsCommentId));
        }


        /// <summary>
        /// 分页获取商品评论数据列表
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
        /// 添加商品评论
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public void Insert(GoodsComment comment)
        {
            if (Resolve<GoodsService>().IsExistGoods(comment.sGoodNo))
            {
                if (_server.Add(comment) > 0)
                {
                    result.success = true;
                    result.info = MessageTip.添加成功.ToString();
                }
                else
                    result.info = MessageTip.添加失败.ToString();
            }
            else
                result.info = "该商品编号对应商品不存在";
        }


        /// <summary>
        /// 编辑商品评论
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public void Update(GoodsComment comment)
        {
            if (Resolve<GoodsService>().IsExistGoods(comment.sGoodNo))
            {
                if (_server.Edit(comment) > 0)
                {
                    result.success = true;
                    result.info = MessageTip.编辑成功.ToString();
                }
                else
                    result.info = MessageTip.编辑失败.ToString();
            }
            else
                result.info = "该商品编号对应商品不存在";
        }
    }
}
