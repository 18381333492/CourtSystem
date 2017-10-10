
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels;
using SystemInterface;
using Newtonsoft.Json.Linq;

namespace SystemModel
{
    public partial class ButtonServices: IButton
    {
    
        public class MenuButton
        {
            public string id;
            public string toid;
            public string text;
            public string iconCls;
            public string sToMenuId;
        }

        /// <summary>
        /// 获取菜单按钮数据列表
        /// </summary>
        /// <returns></returns>
        public override string GetList()
        {
            var menuList = query.QueryList<ES_Menu>(@"SELECT * FROM ES_Menu WHERE bIsDeleted=0 ORDER BY iOrder");
            var buttonList=query.QueryList<ES_Button>(@"SELECT * FROM ES_Button ORDER BY iOrder");

            //根级菜单
            var FirstMenu = (from m in menuList
                             where string.IsNullOrEmpty(m.sParentMenuId)==true
                             orderby m.iOrder
                             select new MenuButton
                             {
                                 id = m.ID.ToString(),
                                 text = m.sMenuName,
                             }).ToList();
            //二级菜单
            var Menu = (from m in menuList
                        where string.IsNullOrEmpty(m.sParentMenuId) == false
                        orderby m.iOrder
                        select new MenuButton
                        {
                            id = m.ID.ToString(),
                            text = m.sMenuName,
                            toid = m.sParentMenuId
                        }).ToList();
            //菜单按钮
            var Button = from m in buttonList
                         orderby m.iOrder
                         select new MenuButton
                         {
                             id = m.ID.ToString(),
                             text = m.sButtonName,
                             iconCls = m.sButtonIcon,
                             sToMenuId = m.sMenuId.ToString()
                         };

            JArray Main = new JArray();
            foreach (var m in FirstMenu)
            {
                JObject job = new JObject();
                job.Add(new JProperty("id", m.id));
                job.Add(new JProperty("text", m.text));
                JArray Sec_Array = new JArray();
                foreach (var n in Menu)
                {
                    if (m.id == n.toid)
                    {
                        JObject job_sec = new JObject();
                        job_sec.Add(new JProperty("id", n.id));
                        job_sec.Add(new JProperty("text", n.text));
                        job_sec.Add(new JProperty("iconCls", string.Empty));

                        JArray third_Array = new JArray();
                        foreach (var k in Button)
                        {
                            if (n.id == k.sToMenuId)
                            {//按钮所属菜单
                                JObject job_thrid = new JObject();
                                job_thrid.Add(new JProperty("id", k.id));
                                job_thrid.Add(new JProperty("text", k.text));
                                job_thrid.Add(new JProperty("iconCls", "icon-edit"));
                                third_Array.Add(job_thrid);
                            }
                        }
                        job_sec.Add(new JProperty("children", third_Array));
                        Sec_Array.Add(job_sec);
                    }
                }
                job.Add(new JProperty("children", Sec_Array));
                Main.Add(job);
            }
            return Main.ToString();
        }
    


        /// <summary>
        /// 根据按钮主键ID获取按钮
        /// </summary>
        /// <param name="sButtonId"></param>
        /// <returns></returns>
        public override ES_Button GetById(string sButtonId)
        {
            var button = query.SingleQuery<ES_Button>(@"SELECT * FROM ES_Button WHERE ID=@ID", new { ID = sButtonId });
            return button;
        }


        /// <summary>
        /// 获取所有的按钮（SuperMan专用通道）
        /// </summary>
        /// <returns></returns>
        public override List<ES_Button> GetAllButtonList()
        {
            return query.QueryList<ES_Button>("SELECT * FROM ES_Button ORDER BY iOrder").ToList();
        }
    }
}
