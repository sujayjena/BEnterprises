using BE.Core;
using BE.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BE.Data.Order
{
    public class bl_Loan
    {
        public UnitOfWork _objUnitOfWork = new UnitOfWork();

        public T_Loan Create(T_Loan ObjLoan)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_Loan_Repository.Insert(ObjLoan);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjLoan;
        }

        public T_Loan Update(T_Loan ObjLoan)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_Loan_Repository.Update(ObjLoan);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjLoan;
        }

        public T_Loan Delete(T_Loan ObjLoan)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_Loan_Repository.Delete(ObjLoan.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjLoan;
        }

        public bool BulkDelete(List<T_Loan> objList)
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

        public T_Loan GetFirstOrDefault(T_Loan ObjLoan)
        {
            var ReturnCompanyObj = new T_Loan();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ReturnCompanyObj = _objUnitOfWork._T_Loan_Repository.GetFirstOrDefault(x => x.LoanNumber == ObjLoan.LoanNumber);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ReturnCompanyObj;
        }

        public T_Loan GetById(Guid Id)
        {
            var ObjLoan = new T_Loan();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjLoan = _objUnitOfWork._T_Loan_Repository.GetById(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjLoan;
        }

        public List<T_Loan> GetList(T_Loan ObjLoan)
        {
            var ObjList = new List<T_Loan>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryLoan = _objUnitOfWork._T_Loan_Repository.Query();
                    var queryLoanDetails = _objUnitOfWork._T_LoanDetails_Repository.Query();

                    if (ObjLoan.EventDate != null && ObjLoan.EventDate.ToString("yyyy-MM-dd") != "0001-01-01")
                    {
                        queryLoan = queryLoan.Where(x => x.EventDate == ObjLoan.EventDate);
                    }
                    if (!string.IsNullOrWhiteSpace(ObjLoan.LoanNumber))
                    {
                        queryLoan = queryLoan.Where(x => x.LoanNumber.Contains(ObjLoan.LoanNumber));
                    }
                    if (!string.IsNullOrWhiteSpace(ObjLoan.Name))
                    {
                        queryLoan = queryLoan.Where(x => x.Name.Contains(ObjLoan.Name));
                    }
                    queryLoan = queryLoan.OrderByDescending(x => x.CreatedDate);

                    var vList = queryLoan.ToList();

                    ObjList = vList.Select(x => new T_Loan
                    {
                        Id = x.Id,
                        LoanNumber = x.LoanNumber,
                        Amount = x.Amount,
                        PaidAmount = queryLoanDetails.Where(s => s.LoanId == x.Id).ToList().Sum(y => y.PaidAmount),
                        DueAmount = (x.Amount - queryLoanDetails.Where(s => s.LoanId == x.Id).ToList().Sum(y => y.PaidAmount)),
                        EventDate = x.EventDate,
                        Name = x.Name,
                        Remark = x.Remark,
                        CreatedDate = x.CreatedDate
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjList;
        }

        public string GetOrderNo()
        {
            var vOrderNo = string.Empty;
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var vObj = _objUnitOfWork._T_Loan_Repository.Get().OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                    if (vObj != null)
                    {
                        string[] sVal = vObj.LoanNumber.Split('-');
                        if (!string.IsNullOrWhiteSpace(sVal[2]))
                        {
                            vOrderNo = sVal[0] + "-" + sVal[1] + "-" + String.Format("{0:0000000000}", Convert.ToInt32(sVal[2]) + 1);
                        }
                        else
                            vOrderNo = "BE-LOAN-0000000001";
                    }
                    else
                        vOrderNo = "BE-LOAN-0000000001";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vOrderNo;
        }

        // For Edit Detail Pages 

        public T_LoanDetails CreateLoanDetail(T_LoanDetails ObjModel)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_LoanDetails_Repository.Insert(ObjModel);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjModel;
        }

        public T_LoanDetails UpdateLoanDetail(T_LoanDetails ObjModel)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_LoanDetails_Repository.Update(ObjModel);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjModel;
        }

        public T_LoanDetails DeleteLoanDetail(T_LoanDetails ObjModel)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_LoanDetails_Repository.Delete(ObjModel.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjModel;
        }

        public List<T_LoanDetails> GetLoanDetailList(T_Loan ObjLoan)
        {
            var ObjList = new List<T_LoanDetails>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var vList = (from LoanDetail in _objUnitOfWork._T_LoanDetails_Repository.Get()
                                 join Loan in _objUnitOfWork._T_Loan_Repository.Get() on LoanDetail.LoanId equals Loan.Id
                                 where LoanDetail.LoanId == ObjLoan.Id

                                 select new T_LoanDetails()
                                 {
                                     Id = LoanDetail.Id,
                                     EventDate = LoanDetail.EventDate,
                                     LoanId = LoanDetail.LoanId,
                                     SlNo = LoanDetail.SlNo,
                                     OutStandingAmount = LoanDetail.OutStandingAmount,
                                     PaidAmount = LoanDetail.PaidAmount,
                                     DueAmount = LoanDetail.DueAmount,
                                     Remark = LoanDetail.Remark,
                                     CreatedDate = LoanDetail.CreatedDate
                                 }).OrderBy(x => x.SlNo).ToList();

                    ObjList = vList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjList;
        }

        public T_LoanDetails GetLoanOutStandingDetail(T_Loan ObjLoan)
        {
            var ObjList = new T_LoanDetails();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryLoan = _objUnitOfWork._T_Loan_Repository.Query();
                    var queryLoanDetail = _objUnitOfWork._T_LoanDetails_Repository.Query();

                    var vLoan = queryLoan.Where(x => x.Id == ObjLoan.Id).FirstOrDefault();
                    var vLoanDetail = queryLoanDetail.Where(x => x.LoanId == ObjLoan.Id).ToList();

                    if (vLoan != null)
                    {
                        ObjList = vLoanDetail.ToList().Select(x => new T_LoanDetails()
                        {
                            OutStandingAmount = (vLoan.Amount - vLoanDetail.Sum(y => y.PaidAmount)),
                        }).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjList;
        }

        public T_LoanDetails GetByIdLoanDetail(Guid UserId)
        {
            var ObjLoan = new T_LoanDetails();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjLoan = _objUnitOfWork._T_LoanDetails_Repository.GetById(UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjLoan;
        }
    }
}
