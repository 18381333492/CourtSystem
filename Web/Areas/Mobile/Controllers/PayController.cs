using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeiXin.Tool;

namespace Web.Areas.Mobile.Controllers
{
    public class PayController : Controller
    {
        //
        // GET: /Mobile/Pay/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 发送模板消息
        /// </summary>
        public void Send()
        {
            string openid = "omHVhuM92qR97IBMffkx6smEoZjc";
            string template_id = "t0JrOTKFmcGIFKXy-6x3OQKkIU0J9vdQjbznRiZlGmc";
            JObject job = new JObject();

            job.Add(new JProperty("touser", openid));
            job.Add(new JProperty("template_id", template_id));
            job.Add(new JProperty("url", "http://www.baidu.com"));
            JObject childData = new JObject();
            childData.Add(new JProperty("date", new JObject(new JProperty("value", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")))));
            childData.Add(new JProperty("user", new JObject(new JProperty("value", "汤台"))));
            job.Add(new JProperty("data", childData));
            string Content = job.ToString();
            access_token token = new access_token("wx474f2fdbcbff2dde", "e2d3a16f0c00157e2c2d684759e41389");
            TemplateHelper.SendMessage(Content, token.Get());
        }
    }
}
