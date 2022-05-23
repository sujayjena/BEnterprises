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
using System.Web.Mvc;

namespace BE.Core
{
    [MetadataType(typeof(T_PurchaseOrder_MetaData))]
    public partial class T_PurchaseOrder
    {
        [NotMapped]
        public List<SelectListItem> ItemsList { get; set; }

        [NotMapped]
        public List<SelectListItem> BrandList { get; set; }

        [NotMapped]
        public List<SelectListItem> MaterialTypeList { get; set; }

        [NotMapped]
        public List<SelectListItem> GuageList { get; set; }

        [NotMapped]
        public List<SelectListItem> UomList { get; set; }

        [NotMapped]
        public Guid PurchaseOrderId { get; set; }

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:#.##}", ApplyFormatInEditMode = true)]
        public decimal TotalAmount { get; set; }
    }

    public class T_PurchaseOrder_MetaData
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is required")]
        public Guid Id { get; set; }

        [Display(Name = "Order No")]
        [Required(ErrorMessage = "{0} is required")]
        public string OrderId { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "{0} is required")]
        public System.DateTime EventDate { get; set; }

        [Display(Name = "Biller Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string BillerName { get; set; }

        [Display(Name = "CreatedBy")]
        public string CreatedBy { get; set; }

        [Display(Name = "CreatedDate")]
        [Required(ErrorMessage = "{0} is required")]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "ModifyBy")]
        public string ModifyBy { get; set; }

        [Display(Name = "ModifyDate")]
        public Nullable<System.DateTime> ModifyDate { get; set; }

    }
}