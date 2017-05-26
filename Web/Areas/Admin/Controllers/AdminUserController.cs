
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
            if (!manage.CheckLoginAccout(adminUser.sLoginAccout))
            {
                if (manage.Insert(adminUser) > 0)
                    result.success = true;
            }
            else
                result.info =string.Format("账号：{0}已被注册,请重新输入", adminUser.sLoginAccout);
        }


        /// <summary>
        ///  根据主键ID重置后台用户账户密码
        /// </summary>
        /// <param name="adminUser"></param>
        public void Reset(string ID)
        {
            if (manage.Reset(ID) > 0)
                result.success = true;
        }

        /// <summary>
        /// 根据主键ID集合删除后台用户
        /// </summary>
        /// <param name="Ids"></param>
        public void Cancel(string Ids)
        {
            if (!manage.CheckIsSuperByIds(Ids))
            {
                if (manage.Cancel(Ids) > 0)
                    result.success = true;
            }
            else
                result.info = "超级管理员不能被执行删除,请重新操作";
        }

        /// <summary>
        /// 根据主键ID集合冻结用户
        /// </summary>
        /// <param name="Ids"></param>
        public void Freeze(string Ids)
        {
            if (!manage.CheckIsSuperByIds(Ids))
            {
                if (manage.Freeze(Ids) > 0)
                    result.success = true;
            }
            else
                result.info = "超级管理员不能被执行冻结,请重新操作";
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
