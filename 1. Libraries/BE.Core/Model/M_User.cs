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
    public partial class M_User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string RoleId { get; set; }
        public Nullable<System.DateTime> LastLoginTime { get; set; }
        public string LastLoginIP { get; set; }
        public Nullable<System.DateTime> CurrentLoginTime { get; set; }
        public string CurrentLoginIP { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }

        [ForeignKey("RoleId")]
        public virtual M_Roles M_Roles { get; set; }
    }
}