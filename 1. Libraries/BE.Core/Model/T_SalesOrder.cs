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
    public partial class T_SalesOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "495282a6-bec7-4d4b-b825-ac1fd956281e:DoNotCallOverridableMethodsInConstructors")]
        public T_SalesOrder()
        {
           this.T_OutStanding = new HashSet<T_OutStanding>();
           this.T_SalesOrderDetails = new HashSet<T_SalesOrderDetails>();
        }

        [Key]
        public Guid Id { get; set; }
        public System.DateTime EventDate { get; set; }
        public string OrderId { get; set; }
        public string BillerName { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "188e727f-a78f-4c8a-a800-0cbadda7be4f:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_OutStanding> T_OutStanding { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "188e727f-a78f-4c8a-a800-0cbadda7be4f:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_SalesOrderDetails> T_SalesOrderDetails { get; set; }
    }
}
