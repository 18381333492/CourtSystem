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
        /// 添加图文信息
        /// </summary>
        /// <param name="newsName"></param>
        /// <param name="sJsonDataChatNews"></param>
        public void Insert(CDELINK_WeChatNewsName newsName,string sJsonDataChatNews)
        {
            var newsData = JsonConvert.DeserializeObject<List<CDELINK_WeChatNews>>(sJsonDataChatNews);
            if(manage.Insert(newsName, newsData) > 0)
            {
                result.success = true;
            }
        }
    }
}
