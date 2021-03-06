//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using BE.Services.Repository;
using BE.Services.DbConnections;
using BE.Core;

namespace BE.Services.UnitOfWork
{
    public partial class UnitOfWork : IUnitOfWork, System.IDisposable
    {
        private readonly SqlDbContext _context;
        private bool disposed;

        public UnitOfWork()
        {
            this._context = new SqlDbContext();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }


        public IGenericRepository<M_Branch> M_Branch_Repository { get; set; }
        public IGenericRepository<M_Branch> _M_Branch_Repository
        {
            get { return M_Branch_Repository ?? (M_Branch_Repository= new GenericRepository<M_Branch>(_context)); }
            set { _M_Branch_Repository = value; }
        }
        public IGenericRepository<M_Brand> M_Brand_Repository { get; set; }
        public IGenericRepository<M_Brand> _M_Brand_Repository
        {
            get { return M_Brand_Repository ?? (M_Brand_Repository= new GenericRepository<M_Brand>(_context)); }
            set { _M_Brand_Repository = value; }
        }
        public IGenericRepository<M_Company> M_Company_Repository { get; set; }
        public IGenericRepository<M_Company> _M_Company_Repository
        {
            get { return M_Company_Repository ?? (M_Company_Repository= new GenericRepository<M_Company>(_context)); }
            set { _M_Company_Repository = value; }
        }
        public IGenericRepository<M_Guage> M_Guage_Repository { get; set; }
        public IGenericRepository<M_Guage> _M_Guage_Repository
        {
            get { return M_Guage_Repository ?? (M_Guage_Repository= new GenericRepository<M_Guage>(_context)); }
            set { _M_Guage_Repository = value; }
        }
        public IGenericRepository<M_Product> M_Product_Repository { get; set; }
        public IGenericRepository<M_Product> _M_Product_Repository
        {
            get { return M_Product_Repository ?? (M_Product_Repository= new GenericRepository<M_Product>(_context)); }
            set { _M_Product_Repository = value; }
        }
        public IGenericRepository<M_ProductType> M_ProductType_Repository { get; set; }
        public IGenericRepository<M_ProductType> _M_ProductType_Repository
        {
            get { return M_ProductType_Repository ?? (M_ProductType_Repository= new GenericRepository<M_ProductType>(_context)); }
            set { _M_ProductType_Repository = value; }
        }
        public IGenericRepository<M_Roles> M_Roles_Repository { get; set; }
        public IGenericRepository<M_Roles> _M_Roles_Repository
        {
            get { return M_Roles_Repository ?? (M_Roles_Repository= new GenericRepository<M_Roles>(_context)); }
            set { _M_Roles_Repository = value; }
        }
        public IGenericRepository<M_Supplier> M_Supplier_Repository { get; set; }
        public IGenericRepository<M_Supplier> _M_Supplier_Repository
        {
            get { return M_Supplier_Repository ?? (M_Supplier_Repository= new GenericRepository<M_Supplier>(_context)); }
            set { _M_Supplier_Repository = value; }
        }
        public IGenericRepository<M_UOM> M_UOM_Repository { get; set; }
        public IGenericRepository<M_UOM> _M_UOM_Repository
        {
            get { return M_UOM_Repository ?? (M_UOM_Repository= new GenericRepository<M_UOM>(_context)); }
            set { _M_UOM_Repository = value; }
        }
        public IGenericRepository<M_User> M_User_Repository { get; set; }
        public IGenericRepository<M_User> _M_User_Repository
        {
            get { return M_User_Repository ?? (M_User_Repository= new GenericRepository<M_User>(_context)); }
            set { _M_User_Repository = value; }
        }
    }
}
