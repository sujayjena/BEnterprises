using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Core;
using BE.Services.UnitOfWork;


namespace BE.Data.Expense
{
    public class bl_ExpenseType
    {
        public UnitOfWork _objUnitOfWork = new UnitOfWork();

        public M_ExpenseType Create(M_ExpenseType ObjExpenseType)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_ExpenseType_Repository.Insert(ObjExpenseType);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjExpenseType;
        }

        public M_ExpenseType Update(M_ExpenseType ObjExpenseType)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_ExpenseType_Repository.Update(ObjExpenseType);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjExpenseType;
        }

        public M_ExpenseType Delete(M_ExpenseType ObjExpenseType)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_ExpenseType_Repository.Delete(ObjExpenseType.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjExpenseType;
        }

        public bool BulkDelete(List<M_ExpenseType> objList)
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

        public M_ExpenseType GetFirstOrDefault(M_ExpenseType ObjExpenseType)
        {
            var ReturnCompanyObj = new M_ExpenseType();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ReturnCompanyObj = _objUnitOfWork._M_ExpenseType_Repository.GetFirstOrDefault(x => x.Name == ObjExpenseType.Name);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ReturnCompanyObj;
        }

        public M_ExpenseType GetById(Guid UserId)
        {
            var ObjExpenseType = new M_ExpenseType();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjExpenseType = _objUnitOfWork._M_ExpenseType_Repository.GetById(UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjExpenseType;
        }

        public List<M_ExpenseType> GetList(M_ExpenseType ObjExpenseType)
        {
            var ObjList = new List<M_ExpenseType>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryObjList = _objUnitOfWork._M_ExpenseType_Repository.Query();
                    if (!string.IsNullOrWhiteSpace(ObjExpenseType.Name))
                    {
                        queryObjList = queryObjList.Where(x => x.Name.Contains(ObjExpenseType.Name));
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
