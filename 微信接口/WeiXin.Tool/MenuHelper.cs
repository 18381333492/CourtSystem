using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeiXin.Tool
{
    /// <summary>
    /// 微信自定义菜单帮助类
    /// </summary>
    public class MenuHelper
    {

        /// <summary>
        /// 组装数据
        /// </summary>
        /// <param name="data"></param>
        public static string InstallData(List<button> list)
        {
            var MainList = list.Where(m => string.IsNullOrEmpty(m.toid) == true);//一级菜单
            var ChildList = list.Where(m => string.IsNullOrEmpty(m.toid) == false);//二级菜单

            JObject JsonData = new JObject();
            JArray JsonArray = new JArray();
            foreach (var m in MainList)
            {
                JObject MainJobject = new JObject();
                JArray ChildArray = new JArray();//二级数组

                MainJobject.Add(new JProperty("name", m.name));
                if (m.type == "1")
                {
                    MainJobject.Add(new JProperty("type", "view"));
                    MainJobject.Add(new JProperty("url", m.url));
                }
                else
                {
                    MainJobject.Add(new JProperty("type", "click"));
                    MainJobject.Add(new JProperty("key", m.key));
                }

                //二级菜单遍历
                foreach (var n in ChildList)
                {
                    JObject ChildJobject = new JObject();
                    if (m.id == n.toid)
                    {
                        ChildJobject.Add(new JProperty("name", n.name));
                        if (n.type == "1")
                        {
                            ChildJobject.Add(new JProperty("type", "view"));
                            ChildJobject.Add(new JProperty("url", n.url));
                        }
                        else
                        {
                            ChildJobject.Add(new JProperty("type", "click"));
                            ChildJobject.Add(new JProperty("key", n.key));
                        }
                        ChildArray.Add(ChildJobject);
                    }
                }
                MainJobject.Add(new JProperty("sub_button", ChildArray));
                JsonArray.Add(MainJobject);
            }
            JsonData.Add(new JProperty("button", JsonArray));
            return JsonData.ToString();

        }

        /// <summary>
        /// 生成菜单
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool MakeMenu(string data,string accaccess_token, out string info)
        {
            info = string.Empty;
            string sUrl = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", accaccess_token);
            string res=HttpHelper.HttpPost(sUrl, data);
            JObject result = JObject.Parse(res);
            if (Convert.ToInt32(result["errcode"]) == 0)
                return true;
            else
            {
                info = result["errmsg"].ToString();
                return false;
            }
        }
    }


    public class button
    {
        public string name;//按钮名称

        //按钮触发类型  view:跳转 click:点击                
        public string type;

        public string url;//跳转链接

        public string key;//自定义key值

        public string toid;//上级归属id

        public string id;//归属id
    }
}
