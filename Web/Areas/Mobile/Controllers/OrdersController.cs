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
    /// 订单相关
    /// </summary>
    public class OrdersController : MobileBaseController<OrdersService>
    {
        //
        // GET: /Mobile/Order/
  
        /// <summary>
        /// 会员页面查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 根据订单ID查看订单详情视图
        /// </summary>
        /// <param name="sOrderId"></param>
        /// <returns></returns>
        public ActionResult Detail(Guid sOrderId)
        {
            return View(_server.Get(sOrderId));
        }

        /// <summary>
        /// 商品评论视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Comment(Guid sOrderId)
        {
            return View(_server.Get(sOrderId));
        }

        /// <summary>
        /// 根据手机号码查询用户订单
        /// </summary>
        /// <param name="Info"></param>
        /// <param name="Sta"></param>
        /// <param name="End"></param>
        /// <param name="searchText"></param>
        /// <param name="iState"></param>
        /// <returns></returns>

        public ActionResult List(PageInfo Info, string sPhone, int iState = -1)
        {
            return Content(_server.GetListByPhone(Info, sPhone, iState));
        }

        /// <summary>
        /// 下订单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void BookOrder(Orders order)
        {
            var domin = Resolve<GoodsService>();
            if (domin.IsExistGoods(order.sGoodNo))
            {
                if (_server.Add(order) > 0)
                {
                    //订单生成之后发起支付所需要的额参数
                    string url = PayHelper.sGetPayUrl(order.sOrderNo, order.sGoodName, order.dPrices.ToString(), "怡佳之城订单");
                    result.data = url;
                    result.success = true;
                    result.info = "下单成功!";
                }
                else
                    result.info = "操作失败!";
            }
            else
                result.info = "亲,该商品信息有误,无法购买!";
        }


        /// <summary>
        /// 订单支付
        /// </summary>
        /// <param name="sOrderId"></param>
        public void Pay(Guid sOrderId)
        {
            var order = new Orders();
            if (_server.CheckOrderState(sOrderId,out order))
            {
                string url = PayHelper.sGetPayUrl(order.sOrderNo, order.sGoodName, order.dPrices.ToString(), "怡佳之城订单");
                result.data = url;
                result.success = true;
            }
            else
                result.info = "该订单已支付过,不能再次发起支付!";
        }



        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="sOrderId"></param>
        public void CancelOrder(Guid sOrderId)
        {
            if (_server.CancelOrder(sOrderId)>0)
                result.success = true;
            else
            {
                result.info = "操作失败!";
            }
        }

        /// <summary>
        /// 买家提醒发货
        /// </summary>
        /// <param name="sOrderId"></param>
        public void OrderTip(Guid sOrderId)
        {
            var res = _server.TipOrder(sOrderId);
            if (res > 0)
            {
                result.success = true;
                result.info = "提醒成功!";
                if (res == 2)
                    result.info = "一天只能提醒卖家发货一次!";
            }
        }


        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="sOrderId"></param>
        public void Confirm(Guid sOrderId)
        {
            if (_server.Alter(sOrderId) > 0)
                result.success = true;
            else
                result.info = "操作失败!";
        }


        /// <summary>
        /// 会员评价商品
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="sOrderId"></param>
        public void GoodsComment(GoodsComment comment,Guid sOrderId)
        {
            var domin = Resolve<GoodsCommentService>();
            var res = domin.ClientAddComment(comment, sOrderId);
            if (res > 0)
            {
                result.success = true;
            }
            else
            {
                if (res == -2) result.info = "该商品已评价过!";
                result.info = "评价失败!";
            }

        }
         
    }
}
