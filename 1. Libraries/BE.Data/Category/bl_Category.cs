using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Core;
using BE.Services.UnitOfWork;

namespace BE.Data.Category
{
    public class bl_Category
    {
        public UnitOfWork _objUnitOfWork = new UnitOfWork();

        public M_Category Create(M_Category ObjCategory)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Category_Repository.Insert(ObjCategory);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjCategory;
        }

        public M_Category Update(M_Category ObjCategory)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Category_Repository.Update(ObjCategory);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjCategory;
        }

        public M_Category Delete(M_Category ObjCategory)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Category_Repository.Delete(ObjCategory.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjCategory;
        }

        public bool BulkDelete(List<M_Category> objList)
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

        public M_Category GetFirstOrDefault(M_Category ObjCategory)
        {
            var ReturnCompanyObj = new M_Category();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ReturnCompanyObj = _objUnitOfWork._M_Category_Repository.GetFirstOrDefault(x => x.Name == ObjCategory.Name);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ReturnCompanyObj;
        }

        public M_Category GetById(Guid UserId)
        {
            var ObjCategory = new M_Category();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjCategory = _objUnitOfWork._M_Category_Repository.GetById(UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjCategory;
        }

        public List<M_Category> GetList(M_Category ObjCategory)
        {
            var ObjList = new List<M_Category>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryObjList = _objUnitOfWork._M_Category_Repository.Query();
                    if (!string.IsNullOrWhiteSpace(ObjCategory.Name))
                    {
                        queryObjList = queryObjList.Where(x => x.Name.Contains(ObjCategory.Name));
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
