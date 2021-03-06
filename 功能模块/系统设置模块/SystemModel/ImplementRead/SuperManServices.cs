﻿using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterface;
using EFModels;
using Common;
using DapperHelper;

namespace SystemModel
{
    /// <summary>
    /// 超级管理员的读相关操作
    /// </summary>
    public partial class SuperManServices : ISuperMan
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="sLoginAccout"></param>
        /// <param name="sPassWord"></param>
        /// <returns></returns>
        public override Dictionary<string, object> CheckLogin(string sOpenId)
        {
            var connectionStr = C_Config.ReadAppSetting("SuperConnection");
         // connectionStr = C_Security.RSADecrypt(connectionStr);
            query.SetconnectionStr(connectionStr);

            return query.SingleQuery<Dictionary<string,object>>(@"SELECT * FROM SuperAdmin WHERE sOpenId=@sOpenId", new
            {
                sOpenId = sOpenId,   
            });

        }
    }
}
