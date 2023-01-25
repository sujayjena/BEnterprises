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
    public partial class T_Loan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "2a8f67e7-e996-407c-aa0c-0980876adb50:DoNotCallOverridableMethodsInConstructors")]
        public T_Loan()
        {
           this.T_LoanDetails = new HashSet<T_LoanDetails>();
        }

        [Key]
        public Guid Id { get; set; }
        public System.DateTime EventDate { get; set; }
        public string LoanNumber { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "d8a59fa3-ef17-4d2c-8f78-c5c335d1d2a0:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_LoanDetails> T_LoanDetails { get; set; }
    }
}
