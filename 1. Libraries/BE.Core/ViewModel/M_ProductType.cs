//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BE.Core
{
    [MetadataType(typeof(M_ProductType_MetaData))]
    public partial class M_ProductType
    {
    }

    public class M_ProductType_MetaData
    {
        [Display(Name= "Id")]
        [Required(ErrorMessage = "{0} is required")]
        public Guid Id { get; set; }

        [Display(Name= "Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [Display(Name= "CreatedBy")]
        public string CreatedBy { get; set; }

        [Display(Name= "CreatedDate")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [Display(Name= "ModifyBy")]
        public string ModifyBy { get; set; }

        [Display(Name= "ModifyDate")]
        public Nullable<System.DateTime> ModifyDate { get; set; }

    }
}
