using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using EFModels;
using EFModels.MyModels;

namespace Web.App_Start.BaseController
{
    public class AdminBase<T>: Controller
    {

        //返回结果集
        protected Result result = new Result();
       
        //接口
        protected T manage;

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        public AdminBase()
        {
            manage = Resolve<T>();
        }

        /// <summary>
        /// 映射实现接口
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <returns></returns>
        protected M Resolve<M>()
        {
            return DIEntity.GetInstance().GetImpl<M>();
        }
    }
}