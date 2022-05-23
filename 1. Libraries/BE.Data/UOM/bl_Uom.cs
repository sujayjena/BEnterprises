using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Core;
using BE.Services.UnitOfWork;

namespace BE.Data.UOM
{
    public class bl_Uom
    {
        public UnitOfWork _objUnitOfWork = new UnitOfWork();

        public M_UOM Create(M_UOM ObjUOM)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_UOM_Repository.Insert(ObjUOM);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjUOM;
        }

        public M_UOM Update(M_UOM ObjUOM)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_UOM_Repository.Update(ObjUOM);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjUOM;
        }

        public M_UOM Delete(M_UOM ObjUOM)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_UOM_Repository.Delete(ObjUOM.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjUOM;
        }

        public bool BulkDelete(List<M_UOM> objList)
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

        public M_UOM GetFirstOrDefault(M_UOM ObjUOM)
        {
            var ReturnCompanyObj = new M_UOM();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ReturnCompanyObj = _objUnitOfWork._M_UOM_Repository.GetFirstOrDefault(x => x.Name == ObjUOM.Name);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ReturnCompanyObj;
        }

        public M_UOM GetById(Guid UserId)
        {
            var ObjUOM = new M_UOM();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjUOM = _objUnitOfWork._M_UOM_Repository.GetById(UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjUOM;
        }

        public List<M_UOM> GetList(M_UOM ObjUOM)
        {
            var ObjList = new List<M_UOM>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryObjList = _objUnitOfWork._M_UOM_Repository.Query();
                    if (!string.IsNullOrWhiteSpace(ObjUOM.Name))
                    {
                        queryObjList = queryObjList.Where(x => x.Name.Contains(ObjUOM.Name));
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
