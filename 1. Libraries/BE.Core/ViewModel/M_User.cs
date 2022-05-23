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
    [MetadataType(typeof(M_User_MetaData))]
    public partial class M_User
    {
    }

    public class M_User_MetaData
    {
        [Display(Name= "Id")]
        [Required(ErrorMessage = "{0} is required")]
        public Guid Id { get; set; }

        [Display(Name= "Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [Display(Name= "Phone")]
        public string Phone { get; set; }

        [Display(Name= "Email")]
        public string Email { get; set; }

        [Display(Name= "UserName")]
        [Required(ErrorMessage = "{0} is required")]
        public string UserName { get; set; }

        [Display(Name= "UserPassword")]
        [Required(ErrorMessage = "{0} is required")]
        public string UserPassword { get; set; }

        [Display(Name= "RoleId")]
        [Required(ErrorMessage = "{0} is required")]
        public string RoleId { get; set; }

        [Display(Name= "LastLoginTime")]
        public Nullable<System.DateTime> LastLoginTime { get; set; }

        [Display(Name= "LastLoginIP")]
        public string LastLoginIP { get; set; }

        [Display(Name= "CurrentLoginTime")]
        public Nullable<System.DateTime> CurrentLoginTime { get; set; }

        [Display(Name= "CurrentLoginIP")]
        public string CurrentLoginIP { get; set; }

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
