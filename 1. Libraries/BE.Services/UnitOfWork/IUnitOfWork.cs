//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using BE.Services.Repository;
using BE.Core;

namespace BE.Services.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Save();

        IGenericRepository<M_Branch> M_Branch_Repository { get; set; }
        IGenericRepository<M_Brand> M_Brand_Repository { get; set; }
        IGenericRepository<M_Company> M_Company_Repository { get; set; }
        IGenericRepository<M_Guage> M_Guage_Repository { get; set; }
        IGenericRepository<M_Product> M_Product_Repository { get; set; }
        IGenericRepository<M_ProductType> M_ProductType_Repository { get; set; }
        IGenericRepository<M_Supplier> M_Supplier_Repository { get; set; }
        IGenericRepository<M_UOM> M_UOM_Repository { get; set; }
    }
}