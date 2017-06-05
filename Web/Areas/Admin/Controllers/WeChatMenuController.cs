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

namespace Web.Areas.Admin.Controllers
{
    /// <summary>
    ///微信自定义菜单控制器
    /// </summary>
    public class WeChatMenuController : AdminBase<IWeChatMenu>
    {
        //
        // GET: /Admin/WeChatKeyWord/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View(manage.GetMainMenuList());
        }

        public ActionResult Edit(string sWeChatMenuId)
        {
            ViewBag.list = manage.GetMainMenuList();
            return View(manage.GetMenuById(sWeChatMenuId));
        }

        /// <summary>
        /// 分页获取微信自定义菜单数据列表
        /// </summary>
        /// <param name="info"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public ActionResult List(PageInfo info, string searchText)
        {
            return Content(manage.PageList(info, searchText));
        }


        /// <summary>
        /// 添加微信菜单
        /// </summary>
        /// <param name="wechatMenu"></param>
        public void Insert(CDELINK_WeChatMenu wechatMenu)
        {
            if (string.IsNullOrEmpty(wechatMenu.sParentMenuId))
            {//创建一级菜单
                if (!manage.CheckMianMenuThree())
                {
                    if (manage.Insert(wechatMenu) > 0)
                        result.success = true;
                }
                else
                    result.info = "一级菜单最多只能创建3个";
            }
            else
            {
                if (!manage.CheckChildMenuFive())
                {
                    if (manage.Insert(wechatMenu) > 0)
                        result.success = true;
                }
                else
                    result.info = "二级菜单最多只能创建5个";
            }
        }


        /// <summary>
        /// 编辑微信菜单
        /// </summary>
        /// <param name="wechatMenu"></param>
        public void Update(CDELINK_WeChatMenu wechatMenu)
        {
            if (string.IsNullOrEmpty(wechatMenu.sParentMenuId))
            {//编辑一级菜单
                if (!manage.CheckMianMenuThree())
                {
                    if (manage.Update(wechatMenu) > 0)
                        result.success = true;
                }
                else
                    result.info = "一级菜单最多只能创建3个";
            }
            else
            {
                if (!manage.CheckChildMenuFive())
                {
                    if (manage.Update(wechatMenu) > 0)
                        result.success = true;
                }
                else
                    result.info = "二级菜单最多只能创建5个";
            }
        }


        /// <summary>
        /// 根据主键ID集合删除微信自定义菜单
        /// </summary>
        /// <param name="Ids"></param>
        public void Cancel(string Ids)
        {
            if (manage.Cancel(Ids) > 0)
                result.success = true;
        }


        /// <summary>
        /// 生成自定义菜单
        /// </summary>
        public void MakeMenu()
        {
            var list = manage.GetAllList();
            var data = (from m in list
                       orderby m.iOrder ascending
                       select new button
                       {
                           id=m.ID.ToString(),
                           name = m.sMenuName,
                           url = m.sUrl,
                           key = m.sKey,
                           type = m.iType.ToString(),
                           toid=m.sParentMenuId
                       }).ToList();
            string sJsonData=MenuHelper.InstallData(data);
            var manageWeChat = Resolve<IWeChat>().GetWeChat();
            access_token at = new access_token(manageWeChat.sAppId, manageWeChat.sAppSecret);
            if (MenuHelper.MakeMenu(sJsonData,at.Get(), out result.info))
            {
                result.success = true;
            }
            else result.success = false;
        }

    }
}
