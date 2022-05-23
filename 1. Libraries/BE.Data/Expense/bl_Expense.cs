using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Core;
using BE.Services.UnitOfWork;

namespace BE.Data.Expense
{
    public class bl_Expense
    {
        public UnitOfWork _objUnitOfWork = new UnitOfWork();

        public T_Expense Create(T_Expense ObjExpense)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_Expense_Repository.Insert(ObjExpense);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjExpense;
        }

        public T_Expense Update(T_Expense ObjExpense)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_Expense_Repository.Update(ObjExpense);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjExpense;
        }

        public T_Expense Delete(T_Expense ObjExpense)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_Expense_Repository.Delete(ObjExpense.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjExpense;
        }

        public bool BulkDelete(List<T_Expense> objList)
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

        public T_Expense GetById(Guid UserId)
        {
            var ObjExpense = new T_Expense();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjExpense = _objUnitOfWork._T_Expense_Repository.GetById(UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjExpense;
        }

        public List<T_Expense> GetList(T_Expense ObjExpense)
        {
            var ObjList = new List<T_Expense>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryExpense = _objUnitOfWork._T_Expense_Repository.Query();
                    var queryExpenseType = _objUnitOfWork._M_ExpenseType_Repository.Query();

                    if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjExpense.ExpenseTypeId)) && Convert.ToString(ObjExpense.ExpenseTypeId) != "00000000-0000-0000-0000-000000000000")
                    {
                        queryExpense = queryExpense.Where(x => x.ExpenseTypeId == ObjExpense.ExpenseTypeId);
                    }
                    var vqueryExpense = queryExpense.Join(queryExpenseType, expense => expense.ExpenseTypeId, eType => eType.Id, (expense, eType) => new { expense, eType }).ToList();

                    var vObjModelList = vqueryExpense.Select(x => new T_Expense()
                    {
                        Id = x.expense.Id,
                        EventDate = x.expense.EventDate,
                        ExpenseTypeId = x.expense.ExpenseTypeId,
                        ExpenseTypeName = x.eType.Name,
                        Amount = x.expense.Amount,
                        Remark = x.expense.Remark,
                        CreatedDate = x.expense.CreatedDate,
                        CreatedBy = x.expense.CreatedBy,
                    }).ToList();

                    ObjList = vObjModelList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjList;
        }

        public List<M_ExpenseType> GetExpenseTypeList()
        {
            var ObjList = new List<M_ExpenseType>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryExpenseType = _objUnitOfWork._M_ExpenseType_Repository.Get();
                    ObjList = queryExpenseType.ToList();
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
