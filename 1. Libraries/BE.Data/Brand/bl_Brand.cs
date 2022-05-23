using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Core;
using BE.Services.UnitOfWork;

namespace BE.Data.Brand
{
    public class bl_Brand
    {
        public UnitOfWork _objUnitOfWork = new UnitOfWork();

        public M_Brand Create(M_Brand ObjBrand)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Brand_Repository.Insert(ObjBrand);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjBrand;
        }

        public M_Brand Update(M_Brand ObjBrand)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Brand_Repository.Update(ObjBrand);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjBrand;
        }

        public M_Brand Delete(M_Brand ObjBrand)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Brand_Repository.Delete(ObjBrand.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjBrand;
        }

        public bool BulkDelete(List<M_Brand> objList)
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

        public M_Brand GetFirstOrDefault(M_Brand ObjBrand)
        {
            var ReturnCompanyObj = new M_Brand();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ReturnCompanyObj = _objUnitOfWork._M_Brand_Repository.GetFirstOrDefault(x => x.Name == ObjBrand.Name);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ReturnCompanyObj;
        }

        public M_Brand GetById(Guid UserId)
        {
            var ObjBrand = new M_Brand();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjBrand = _objUnitOfWork._M_Brand_Repository.GetById(UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjBrand;
        }

        public List<M_Brand> GetList(M_Brand ObjBrand)
        {
            var ObjList = new List<M_Brand>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryObjList = _objUnitOfWork._M_Brand_Repository.Query();
                    if (!string.IsNullOrWhiteSpace(ObjBrand.Name))
                    {
                        queryObjList = queryObjList.Where(x => x.Name.Contains(ObjBrand.Name));
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
