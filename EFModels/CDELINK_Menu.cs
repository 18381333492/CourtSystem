//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFModels
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// CDELINK_Menu
    /// </summary>
    public partial class CDELINK_Menu
    {
    	/// <summary>
        /// ID
        /// </summary>
        public System.Guid ID { get; set; }
    	/// <summary>
        /// 菜单名称
        /// </summary>
        public string sMenuName { get; set; }
    	/// <summary>
        /// 上级菜单主键ID
        /// </summary>
        public string sParentMenuId { get; set; }
    	/// <summary>
        /// 添加时间
        /// </summary>
        public System.DateTime dInsertTime { get; set; }
    	/// <summary>
        /// 菜单连接
        /// </summary>
        public string sMenuUrl { get; set; }
    	/// <summary>
        /// 菜单排序
        /// </summary>
        public int iOrder { get; set; }
    	/// <summary>
        /// 逻辑删除标识
        /// </summary>
        public bool bIsDeleted { get; set; }
    	/// <summary>
        /// 菜单图标
        /// </summary>
        public string sMenuIcon { get; set; }
    }
}
