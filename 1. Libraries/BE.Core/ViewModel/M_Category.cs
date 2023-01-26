//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE.Core
{
    [MetadataType(typeof(M_Category_MetaData))]
    public partial class M_Category
    {
    }

    public class M_Category_MetaData
    {
        [Display(Name= "Id")]
        [Required(ErrorMessage = "{0} is required")]
        public Guid Id { get; set; }

        [Display(Name= "CompanyId")]
        [Required(ErrorMessage = "{0} is required")]
        public Guid CompanyId { get; set; }

        [Display(Name= "Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [Display(Name= "CreatedBy")]
        public string CreatedBy { get; set; }

        [Display(Name= "CreatedDate")]
        [Required(ErrorMessage = "{0} is required")]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name= "ModifyBy")]
        public string ModifyBy { get; set; }

        [Display(Name= "ModifyDate")]
        public Nullable<System.DateTime> ModifyDate { get; set; }

    }
}