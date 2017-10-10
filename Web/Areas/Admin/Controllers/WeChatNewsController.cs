using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFModels.MyModels;
using Web.App_Start.BaseController;
using SystemInterface;
using EFModels;
using WeiXin.Tool;
using Newtonsoft.Json;

namespace Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 微信图文信息素材管理
    /// </summary>
    public class WeChatNewsController : AdminBase<IWeChatNews>
    {
        //
        // GET: /Admin/WeChatNews/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 编辑图文素材视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(string sNewsId)
        {
            ViewBag.sNewsId = sNewsId;
            return View();
        }

        /// <summary>
        /// 获取单条图文信息
        /// </summary>
        /// <param name="sNewsId"></param>
        public void GetNews(string sNewsId)
        {
            result.data = manage.GetNews(sNewsId);
            result.success = true;
        }

        /// <summary>
        /// 获取所有的图文信息列表
        /// </summary>
        public void GetNewsList(string searchText)
        {
            result.data = manage.GetNewsList(searchText);
            result.success = true;
        }

        /// <summary>
        /// 添加图文信息
        /// </summary>
        /// <param name="newsName"></param>
        /// <param name="sJsonDataChatNews"></param>
        [ValidateInput(false)]
        public void Insert(ES_WeChatNewsName newsName,string sJsonDataChatNews)
        {
            var newsData = JsonConvert.DeserializeObject<List<ES_WeChatNews>>(sJsonDataChatNews);
            if(manage.Insert(newsName, newsData) > 0)
            {
                result.success = true;
            }
        }


        /// <summary>
        /// 编辑图文信息
        /// </summary>
        /// <param name="newsName"></param>
        /// <param name="sJsonDataChatNews"></param>
        [ValidateInput(false)]
        public void Update(ES_WeChatNewsName newsName, string sJsonDataChatNews)
        {
            var newsData = JsonConvert.DeserializeObject<List<ES_WeChatNews>>(sJsonDataChatNews);
            if (manage.Update(newsName, newsData) > 0)
            {
                result.success = true;
            }
        }

        /// <summary>
        /// 删除图文信息
        /// </summary>
        /// <param name="sNewsId"></param>
        public void Cancel(string sNewsId)
        {
            if (manage.Cancel(sNewsId) > 0)
                result.success = true;
        }
    }
}
