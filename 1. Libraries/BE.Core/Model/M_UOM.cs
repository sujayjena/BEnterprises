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
    public partial class M_UOM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "d4426218-610e-48d8-8e84-9cc369fa4857:DoNotCallOverridableMethodsInConstructors")]
        public M_UOM()
        {
           this.T_PurchaseOrderDetails = new HashSet<T_PurchaseOrderDetails>();
           this.T_SalesOrderDetails = new HashSet<T_SalesOrderDetails>();
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "bb229eb7-6efd-4998-862f-2cfe1648c92d:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_PurchaseOrderDetails> T_PurchaseOrderDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "bb229eb7-6efd-4998-862f-2cfe1648c92d:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_SalesOrderDetails> T_SalesOrderDetails { get; set; }
    }
}
