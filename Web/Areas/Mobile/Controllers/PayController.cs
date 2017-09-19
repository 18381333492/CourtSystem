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
            string openid = "omHVhuM92qR97IBMffkx6smEoZjc1";
            string template_id = "mS4mZnR0qeB5GShGWhCSt0jeXMAWrr_0lkIwnza9JgE";
            JObject job = new JObject();

            job.Add(new JProperty("touser", openid));
            job.Add(new JProperty("template_id", template_id));
            JObject childData = new JObject();
            childData.Add(new JProperty("date", new JObject(new JProperty("value", DateTime.Now.ToString("yyyy-MM-dd")))));
            childData.Add(new JProperty("good", new JObject(new JProperty("value", "测试商品"))));
            childData.Add(new JProperty("moeny", new JObject(new JProperty("value", "99.00"))));
            childData.Add(new JProperty("remark", new JObject(new JProperty("value", "还需加油哦!"))));
            job.Add(new JProperty("data", childData));
            string Content = job.ToString();
            access_token token = new access_token("wx474f2fdbcbff2dde", "e2d3a16f0c00157e2c2d684759e41389");
            TemplateHelper.SendMessage(Content, token.Get());
        }
    }
}
