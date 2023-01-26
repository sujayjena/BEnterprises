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

        IGenericRepository<M_Category> M_Category_Repository { get; set; }
        IGenericRepository<M_Company> M_Company_Repository { get; set; }
        IGenericRepository<M_ExpenseType> M_ExpenseType_Repository { get; set; }
        IGenericRepository<M_Product> M_Product_Repository { get; set; }
        IGenericRepository<M_Roles> M_Roles_Repository { get; set; }
        IGenericRepository<M_Supplier> M_Supplier_Repository { get; set; }
        IGenericRepository<M_UOM> M_UOM_Repository { get; set; }
        IGenericRepository<M_User> M_User_Repository { get; set; }
        IGenericRepository<T_Expense> T_Expense_Repository { get; set; }
        IGenericRepository<T_Loan> T_Loan_Repository { get; set; }
        IGenericRepository<T_LoanDetails> T_LoanDetails_Repository { get; set; }
        IGenericRepository<T_OutStanding> T_OutStanding_Repository { get; set; }
        IGenericRepository<T_OutStandingDetails> T_OutStandingDetails_Repository { get; set; }
        IGenericRepository<T_PurchaseOrder> T_PurchaseOrder_Repository { get; set; }
        IGenericRepository<T_PurchaseOrderDetails> T_PurchaseOrderDetails_Repository { get; set; }
        IGenericRepository<T_SalesOrder> T_SalesOrder_Repository { get; set; }
        IGenericRepository<T_SalesOrderDetails> T_SalesOrderDetails_Repository { get; set; }
    }
}
