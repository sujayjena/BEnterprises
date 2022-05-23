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
    [MetadataType(typeof(T_Loan_MetaData))]
    public partial class T_Loan
    {
        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:#.##}", ApplyFormatInEditMode = true)]
        public decimal DueAmount { get; set; }

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:#.##}", ApplyFormatInEditMode = true)]
        public decimal PaidAmount { get; set; }
    }

    public class T_Loan_MetaData
    {
        [Display(Name= "Id")]
        [Required(ErrorMessage = "{0} is required")]
        public Guid Id { get; set; }

        [Display(Name= "EventDate")]
        [Required(ErrorMessage = "{0} is required")]
        public System.DateTime EventDate { get; set; }

        [Display(Name= "Loan No.")]
        [Required(ErrorMessage = "{0} is required")]
        public string LoanNumber { get; set; }

        [Display(Name= "Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "{0} is required")]
        [DisplayFormat(DataFormatString = "{0:#.##}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }

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
