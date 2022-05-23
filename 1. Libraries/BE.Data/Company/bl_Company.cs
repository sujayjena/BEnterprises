using System;
using System.Collections.Generic;
using System.Linq;
using BE.Core;
using BE.Services.UnitOfWork;

namespace BE.Data.Company
{
    public class bl_Company
    {
        public UnitOfWork _objUnitOfWork = new UnitOfWork();

        public M_Company Create(M_Company ObjCompany)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Company_Repository.Insert(ObjCompany);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjCompany;
        }

        public M_Company Update(M_Company ObjCompany)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Company_Repository.Update(ObjCompany);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjCompany;
        }

        public M_Company Delete(M_Company ObjCompany)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Company_Repository.Delete(ObjCompany.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjCompany;
        }

        public bool BulkDelete(List<M_Company> objList)
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

        public M_Company GetFirstOrDefault(M_Company ObjCompany)
        {
            var ReturnCompanyObj = new M_Company();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ReturnCompanyObj = _objUnitOfWork._M_Company_Repository.GetFirstOrDefault(x => x.Name == ObjCompany.Name);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ReturnCompanyObj;
        }

        public M_Company GetById(Guid UserId)
        {
            var ObjCompany = new M_Company();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjCompany = _objUnitOfWork._M_Company_Repository.GetById(UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjCompany;
        }

        public List<M_Company> GetList(M_Company ObjCompany)
        {
            var ObjList = new List<M_Company>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryObjList = _objUnitOfWork._M_Company_Repository.Query();
                    if (!string.IsNullOrWhiteSpace(ObjCompany.Name))
                    {
                        queryObjList = queryObjList.Where(x => x.Name.Contains(ObjCompany.Name));
                    }
                    ObjList = queryObjList.OrderByDescending(x => x.CreatedDate).ToList();
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
