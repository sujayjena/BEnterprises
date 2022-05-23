//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Core
{
    public partial class T_SalesOrderDetails
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SalesOrderId { get; set; }
        public int SlNo { get; set; }
        public Guid ItemsId { get; set; }
        public Guid BrandId { get; set; }
        public Guid GuageId { get; set; }
        public Guid UomId { get; set; }
        public decimal StockQuantity { get; set; }
        public decimal Quantity { get; set; }
        public decimal LatestPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }

        [ForeignKey("BrandId")]
        public virtual M_Brand M_Brand { get; set; }
        [ForeignKey("GuageId")]
        public virtual M_Guage M_Guage { get; set; }
        [ForeignKey("ItemsId")]
        public virtual M_Items M_Items { get; set; }
        [ForeignKey("UomId")]
        public virtual M_UOM M_UOM { get; set; }
        [ForeignKey("SalesOrderId")]
        public virtual T_SalesOrder T_SalesOrder { get; set; }
    }
}
