using EFModel;
using EFModel.MyModels;
using Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start;

namespace Web.Areas.Mobile.Controllers
{

    /// <summary>
    /// 商品评论相关
    /// </summary>
    public class GoodsCommentController : MobileBaseController<GoodsCommentService>
    {
        //
        // GET: /Mobile/Order/



        /// <summary>
        /// 分页获取商品评论数据
        /// </summary>
        /// <param name="Info"></param>
        /// <param name="sGoodNo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetList(PageInfo Info, string sGoodNo)
        {
            return Content(_server.GetListBysGoodNo(Info,sGoodNo));
        }

        /// <summary>
        /// 添加商品评论
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void AddGoodsComment(GoodsComment item, Guid  sOrderId)
        {
            if (_server.ClientAddComment(item, sOrderId) > 0)
            {
                result.success = true;
                result.info = "评论成功!";
            }
            else
                result.info = "操作失败!";
        }
    }
}
