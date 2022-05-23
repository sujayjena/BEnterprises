using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Core;
using BE.Services.UnitOfWork;

namespace BE.Data.Guage
{
    public class bl_Guage
    {
        public UnitOfWork _objUnitOfWork = new UnitOfWork();

        public M_Guage Create(M_Guage ObjGuage)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Guage_Repository.Insert(ObjGuage);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjGuage;
        }

        public M_Guage Update(M_Guage ObjGuage)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Guage_Repository.Update(ObjGuage);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjGuage;
        }

        public M_Guage Delete(M_Guage ObjGuage)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Guage_Repository.Delete(ObjGuage.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjGuage;
        }

        public bool BulkDelete(List<M_Guage> objList)
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

        public M_Guage GetFirstOrDefault(M_Guage ObjGuage)
        {
            var ReturnCompanyObj = new M_Guage();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ReturnCompanyObj = _objUnitOfWork._M_Guage_Repository.GetFirstOrDefault(x => x.Name == ObjGuage.Name);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ReturnCompanyObj;
        }

        public M_Guage GetById(Guid UserId)
        {
            var ObjGuage = new M_Guage();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjGuage = _objUnitOfWork._M_Guage_Repository.GetById(UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjGuage;
        }

        public List<M_Guage> GetList(M_Guage ObjGuage)
        {
            var ObjList = new List<M_Guage>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryObjList = _objUnitOfWork._M_Guage_Repository.Query();
                    if (!string.IsNullOrWhiteSpace(ObjGuage.Name))
                    {
                        queryObjList = queryObjList.Where(x => x.Name.Contains(ObjGuage.Name));
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
