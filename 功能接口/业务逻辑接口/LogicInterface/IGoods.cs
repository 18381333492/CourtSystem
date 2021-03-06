﻿using EFModels;
using EFModels.MyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicHandlerInterface
{
    /// <summary>
    /// 商品业务相关接口
    /// </summary>
    public abstract class IGoods:BaseBll
    {

        #region 查询


        /// <summary>
        /// 分页获取商品数据列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="searchText"></param>
        /// <param name="bIsShelves"></param>
        /// <param name="iGoodsType"></param>
        /// <returns></returns>
        public abstract string List(PageInfo pageInfo, string searchText, int bIsShelves, int iGoodsType);

        /// <summary>
        /// 根据商品主键ID获取商品
        /// </summary>
        /// <param name="sGoodsId"></param>
        /// <returns></returns>
        public abstract ES_Goods Get(string sGoodsId);

        /// <summary>
        /// 根据商品编号获取商品
        /// </summary>
        /// <param name="sGoodsNo"></param>
        /// <returns></returns>
        public abstract ES_Goods GetByGoodsNo(string sGoodsNo);


        /*******************************************前端的相关查询接口***********************************************/

        /// <summary>
        /// 根据商品编号获取商品的信息
        /// </summary>
        /// <param name="sGoodsNo"></param>
        /// <returns></returns>
        public abstract ES_Goods GetGoodsInfoByGoodsNo(string sGoodsNo);

      

        #endregion


        #region 操作

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public abstract int Insert(ES_Goods goods);


        /// <summary>
        /// 编辑商品
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public abstract int Update(ES_Goods goods);

        /// <summary>
        ///上下架商品
        /// </summary>
        /// <param name="sGoodsId"></param>
        /// <returns></returns>
        public abstract int Shelves(string sGoodsId);


        /// <summary>
        /// 商品活动的开启与禁用
        /// </summary>
        /// <param name="sGoodsId"></param>
        /// <returns></returns>
        public abstract int Activity(string sGoodsId);


        /// <summary>
        /// 设置商品的积分
        /// </summary>
        /// <param name="sGoodsId"></param>
        /// <param name="Integral"></param>
        /// <param name="IsRateAdd">是否按比例增长</param>
        /// <returns></returns>
        public abstract int SetIntegral(string sGoodsId, int Integral, bool IsRateAdd);

        /// <summary>
        /// 设置商品的优惠价格
        /// </summary>
        /// <param name="sGoodsId"></param>
        /// <param name="DisPrices"></param>
        /// <param name="IsRateGiving"></param>
        /// <returns></returns>
        public abstract int SetDisPrices(string sGoodsId, decimal DisPrices, bool IsRateGiving);

        #endregion
    }
}
