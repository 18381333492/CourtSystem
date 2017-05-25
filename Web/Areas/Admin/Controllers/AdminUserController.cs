
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.App_Start.BaseController;
using EFModels;
using EFModels.MyModels;
using Web.App_Start;
using SystemInterface;

namespace Web.Areas.Admin.Controllers
{

    public class AdminUserController : AdminBase<IAdminUser>
    {
        //
        // GET: /Admin/AdminUser/

        #region 后台用户相关视图
    
        public ActionResult Index()
        {
            return View();
        }
 
        public ActionResult Add()
        {
            return View(manage.GetAllRoleNameList());
        }

        public ActionResult Edit()
        {
            return View();
        }

        #endregion

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="Info">分页参数</param>
        /// <param name="searchText">搜索的文本</param>
        /// <param name="iState">会员状态(默认全部)</param>
        /// <returns></returns>
        [NoLogin]
        public ActionResult List(PageInfo pageInfo,string searchText,int iState=-1)
        {
            return Content(manage.PageList(pageInfo, searchText, iState));
        }

        /// <summary>
        /// 添加后台用户
        /// </summary>
        /// <param name="adminUser"></param>
        public void Insert(CDELINK_AdminUser adminUser)
        {
            if (manage.Insert(adminUser) > 0)
                result.success = true;
        }


        /// <summary>
        /// 编辑后台用户
        /// </summary>
        /// <param name="adminUser"></param>
        public void Update(CDELINK_AdminUser adminUser)
        {
            if (manage.Update(adminUser) > 0)
                result.success = true;
        }



        /// <summary>
        /// 后台用户登录
        /// </summary>
        /// <param name="sLoginAccout"></param>
        /// <param name="sPassWord"></param>
        public void  Login(string sLoginAccout,string sPassWord)
        {
            var adminUser=manage.ValidateLogin(sLoginAccout, sPassWord);
            if (adminUser.iState == 0)
            {//该用户被冻结
                result.info = "该账户已被冻结,请联系管理员";
            }
            else
            {
                result.success = true;
            }
        }


    }
}
