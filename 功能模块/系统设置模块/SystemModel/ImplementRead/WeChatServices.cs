﻿using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterface;
using EFModels;
using Common;

namespace SystemModel
{
    /// <summary>
    /// 网站的读操作相关
    /// </summary>
    public partial class WeChatServices : IWeChat
    {
        /// <summary>
        /// 获取微信公众号信息
        /// </summary>
        /// <returns></returns>
        public override CDELINK_WeChat GetWeChat()
        {
            return query.SingleQuery<CDELINK_WeChat>(@"SELECT * FROM CDELINK_WeChat");
        }
    }
}
