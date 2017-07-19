﻿using LogicHandlerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFModels.MyModels;

namespace LogicHandlerModel
{

    /// <summary>
    /// 会员的相关的读操作
    /// </summary>
    public partial class ClientServices:IClient
    {
        /// <summary>
        /// 分页获取会员数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public override string List(PageInfo pageInfo, string searchText)
        {
            var res = query.PageQuery<Dictionary<string, object>>("select * from ES_Client",pageInfo);
            return res.toJson();
        }


        /// <summary>
        /// 根据OpenId判断该会员是否存在
        /// </summary>
        /// <param name="sOpenId"></param>
        /// <returns></returns>
        public override bool IsExistByOpenId(string sOpenId)
        {
            return query.Any("select * from ES_Client where sOpenId=@sOpenId", new { sOpenId = sOpenId });
        }
    }
}
