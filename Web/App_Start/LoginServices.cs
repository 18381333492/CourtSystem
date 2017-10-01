﻿using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SystemInterface;
using Unity;
using Web.App_Start.BaseController;
using WeiXin.Tool;

namespace Web.App_Start
{

    /// <summary>
    /// 登录服务
    /// </summary>
    public class LoginServices
    {
      
        /// <summary>
        /// 账户登录
        /// </summary>
        /// <param name="sLoginAccout"></param>
        /// <param name="sPassWord"></param>
        /// <param name="sCode"></param>
        /// <returns></returns>
        public static Result AccountLogin(string sLoginAccout, string sPassWord, string sCode)
        {
            var result = new Result();
            if (sCode == HttpContext.Current.Session[SESSION.ImgCode].ToString())
            {
                if (string.IsNullOrEmpty(sLoginAccout) || string.IsNullOrEmpty(sPassWord))
                {
                    result.info = "账户和密码不能为空!";
                }
                else
                {
                    var manage = DIEntity.Instance.GetImpl<IAdminUser>();
                    var adminUser = manage.ValidateLogin(sLoginAccout, sPassWord);
                    if (adminUser != null)
                    {
                        if (adminUser.iState == 0)
                        {//在审核中
                            result.info = "该账户在审核中,请联系管理员";
                        }
                        else
                        {
                            var obj = manage.GetMenuAndButtonByRoleId(adminUser.sRoleId);
                            HttpContext.Current.Session[SESSION.Menu] = obj.menuList;//缓存的二级菜单
                            HttpContext.Current.Session[SESSION.Button] = obj.buttonList;//缓存的按钮
                            HttpContext.Current.Session[SESSION.AdminUser] = new UserInfo()
                            {
                                sUserName = adminUser.sName,
                                iState = adminUser.iState,
                                ID = adminUser.ID,
                                sRoleId = new Guid(adminUser.sRoleId),
                                sHeadPic = adminUser.sHeadPicture,//头像
                                bIsSuperMan = false
                            };
                            result.success = true;
                        }
                    }
                    else
                    {
                        result.info = "账号或者密码错误";
                    }
                }
            }
            else
            {
                result.info = "验证码错误或者失效";
            }
            return result;
        }


        /// <summary>
        /// 微信扫码登录
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static Result ScanLogin(WeChatUser userInfo)
        {
            var result = new Result();
            if (!string.IsNullOrEmpty(userInfo.openid))
            {
                var manage = DIEntity.Instance.GetImpl<IAdminUser>();
                var user=manage.ScanLogin(userInfo.openid);
                if (user != null)
                {
                    if (user.iState == 0)
                    {//该账户正在审核中
                        result.info = "该账户在审核中,请联系管理员";
                    }
                    else
                    {
                        var obj = manage.GetMenuAndButtonByRoleId(user.sRoleId);
                        HttpContext.Current.Session[SESSION.Menu] = obj.menuList;//缓存的二级菜单
                        HttpContext.Current.Session[SESSION.Button] = obj.buttonList;//缓存的按钮
                        HttpContext.Current.Session[SESSION.AdminUser] = new UserInfo()
                        {
                            sUserName = userInfo.nickname,
                            iState = user.iState,
                            ID = user.ID,
                            sRoleId = new Guid(user.sRoleId),
                            sHeadPic = userInfo.headimgurl,//微信头像
                            bIsSuperMan = false
                        };
                        result.success = true;
                    }
                }
                else
                {
                    result.info = "登录失败,你还不是管理员";
                }

            }
            else
            {
                result.info = "登录失败,缺少参数";
            }
            return result;
        }
    }
}