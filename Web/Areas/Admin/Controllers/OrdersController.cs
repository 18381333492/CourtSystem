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
    public class OrdersController : AdminBaseController<OrdersService>
    {
        //
        // GET: /Admin/Orders/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 订单物流信息视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Wuliu(Guid sOrderId)
        {
            return View(_server.Get(sOrderId));
        }

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="sOrderId"></param>
        /// <returns></returns>
        public ActionResult Detail(Guid sOrderId)
        {
            return View(_server.Get(sOrderId));
        }

        /// <summary>
        /// 获取订单数据列表
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo Info,string Sta,string End,string searchText,int iState=-1)
        {
            return Content(_server.GetList(Info, Sta, End, searchText, iState));
        }

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="ID"></param>
        public void Alter(Guid ID)
        {
            if (_server.Alter(ID) > 0)
            {
                result.success = true;
                result.info = "修改成功";
            }
            else result.info = "操作失败!";
        }


        /// <summary>
        /// 添加物流信息
        /// </summary>
        /// <param name="sOrderId"></param>
        /// <param name="sWuliuCompany">物流公司</param>
        /// <param name="sWuliuNumber">物流单号</param>
        public void AddWuliu(Guid sOrderId,string sWuliuCompany,string sWuliuNumber)
        {
            if(_server.AddWuliu(sOrderId, sWuliuCompany, sWuliuNumber)>0)
            {
                result.success = true;
                result.info = "操作成功";
            }
            else result.info = "操作失败!";
        }

    }
}
