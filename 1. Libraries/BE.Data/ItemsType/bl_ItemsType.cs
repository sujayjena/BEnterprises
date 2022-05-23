using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Core;
using BE.Services.UnitOfWork;

namespace BE.Data.ItemsType
{
    public class bl_ItemsType
    {
        public UnitOfWork _objUnitOfWork = new UnitOfWork();

        public M_ItemsType Create(M_ItemsType ObjItemsType)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_ItemsType_Repository.Insert(ObjItemsType);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjItemsType;
        }

        public M_ItemsType Update(M_ItemsType ObjItemsType)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_ItemsType_Repository.Update(ObjItemsType);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjItemsType;
        }

        public M_ItemsType Delete(M_ItemsType ObjItemsType)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_ItemsType_Repository.Delete(ObjItemsType.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjItemsType;
        }

        public bool BulkDelete(List<M_ItemsType> objList)
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

        public M_ItemsType GetFirstOrDefault(M_ItemsType ObjItemsType)
        {
            var ReturnCompanyObj = new M_ItemsType();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ReturnCompanyObj = _objUnitOfWork._M_ItemsType_Repository.GetFirstOrDefault(x => x.Name == ObjItemsType.Name);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ReturnCompanyObj;
        }

        public M_ItemsType GetById(Guid UserId)
        {
            var ObjItemsType = new M_ItemsType();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjItemsType = _objUnitOfWork._M_ItemsType_Repository.GetById(UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjItemsType;
        }

        public List<M_ItemsType> GetList(M_ItemsType ObjItemsType)
        {
            var ObjList = new List<M_ItemsType>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryObjList = _objUnitOfWork._M_ItemsType_Repository.Query();
                    if (!string.IsNullOrWhiteSpace(ObjItemsType.Name))
                    {
                        queryObjList = queryObjList.Where(x => x.Name.Contains(ObjItemsType.Name));
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
