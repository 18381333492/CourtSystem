//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class ES_Goods
    {
        public System.Guid ID { get; set; }
        public string sGoodsName { get; set; }
        public int iGoodsType { get; set; }
        public string sGoodsNo { get; set; }
        public decimal sDisPrices { get; set; }
        public decimal sGoodsPrices { get; set; }
        public bool IsRateGiving { get; set; }
        public int iCount { get; set; }
        public bool bIsShelves { get; set; }
        public string sOrderHtmlKey { get; set; }
        public System.DateTime dInsertTime { get; set; }
        public bool bIsDeleted { get; set; }
        public int iIntegral { get; set; }
        public bool IsRateAdd { get; set; }
        public bool bIsActivity { get; set; }
        public string sGoodsStandardId { get; set; }
    }
}
