using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Core;
using BE.Services.UnitOfWork;

namespace BE.Data.Supplier
{
    public class bl_Supplier
    {
        public UnitOfWork _objUnitOfWork = new UnitOfWork();

        public M_Supplier Create(M_Supplier ObjSupplier)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Supplier_Repository.Insert(ObjSupplier);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjSupplier;
        }

        public M_Supplier Update(M_Supplier ObjSupplier)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Supplier_Repository.Update(ObjSupplier);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjSupplier;
        }

        public M_Supplier Delete(M_Supplier ObjSupplier)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Supplier_Repository.Delete(ObjSupplier.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjSupplier;
        }

        public bool BulkDelete(List<M_Supplier> objList)
        {
            bool bSuccess = false;
            try
            {
                foreach (var item in objList)
                {
                    var vCheckUser = GetById(item.Id);
                    if (vCheckUser != null)
                        Delete(vCheckUser);
                }
                bSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSuccess;
        }

        public M_Supplier GetFirstOrDefault(M_Supplier ObjSupplier)
        {
            var ReturnSupplierObj = new M_Supplier();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ReturnSupplierObj = _objUnitOfWork._M_Supplier_Repository.GetFirstOrDefault(x => x.Name == ObjSupplier.Name);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ReturnSupplierObj;
        }

        public M_Supplier GetById(Guid UserId)
        {
            var ObjSupplier = new M_Supplier();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjSupplier = _objUnitOfWork._M_Supplier_Repository.GetById(UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjSupplier;
        }

        public List<M_Supplier> GetList(M_Supplier ObjSupplier)
        {
            var ObjList = new List<M_Supplier>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryObjList = _objUnitOfWork._M_Supplier_Repository.Query();
                    if (!string.IsNullOrWhiteSpace(ObjSupplier.Name))
                    {
                        queryObjList = queryObjList.Where(x => x.Name.Contains(ObjSupplier.Name));
                    }
                    ObjList = queryObjList.OrderBy(x => x.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjList;
        }
    }
}
