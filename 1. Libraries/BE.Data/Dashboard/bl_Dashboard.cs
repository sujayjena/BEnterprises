using BE.Core;
using BE.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BE.Data.Dashboard
{
    public class bl_Dashboard
    {
        public UnitOfWork _objUnitOfWork = new UnitOfWork();


        public List<M_Product> GetItems()
        {
            List<M_Product> objList = new List<M_Product>();
            try
            {
                objList = _objUnitOfWork._M_Product_Repository.Get();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objList;
        }

        public List<T_PurchaseOrder> GetPurchaseOrder()
        {
            List<T_PurchaseOrder> objList = new List<T_PurchaseOrder>();
            try
            {
                var queryPurchaseOrder = _objUnitOfWork._T_PurchaseOrder_Repository.Query();
                var queryPurchaseOrderDetails = _objUnitOfWork._T_PurchaseOrderDetails_Repository.Query();

                queryPurchaseOrder = queryPurchaseOrder.OrderByDescending(x => x.CreatedDate);

                var vList = queryPurchaseOrder.ToList();
                objList = vList.Select(x => new T_PurchaseOrder
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    TotalAmount = queryPurchaseOrderDetails.Where(s => s.PurchaseOrderId == x.Id).ToList().Sum(y => y.BuyingPrice),
                    EventDate = x.EventDate,
                    BillerName = x.BillerName,
                    CreatedDate = x.CreatedDate
                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objList;
        }

        public List<T_SalesOrder> GetSalesOrder()
        {
            var ObjList = new List<T_SalesOrder>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var querySalesOrder = _objUnitOfWork._T_SalesOrder_Repository.Query();
                    var querySalesOrderDetails = _objUnitOfWork._T_SalesOrderDetails_Repository.Query();

                    querySalesOrder = querySalesOrder.OrderByDescending(x => x.CreatedDate);

                    var vList = querySalesOrder.ToList();
                    ObjList = vList.Select(x => new T_SalesOrder
                    {
                        Id = x.Id,
                        OrderId = x.OrderId,
                        TotalAmount = querySalesOrderDetails.Where(s => s.SalesOrderId == x.Id).ToList().Sum(y => y.SellingPrice),
                        EventDate = x.EventDate,
                        BillerName = x.BillerName,
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

        public List<T_Expense> GetExpense()
        {
            var ObjList = new List<T_Expense>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryExpense = _objUnitOfWork._T_Expense_Repository.Query();
                    var queryExpenseType = _objUnitOfWork._M_ExpenseType_Repository.Query();

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

        public List<Sys_Dashboard> GetSalesAndPurchaseList()
        {
            var ObjList = new List<Sys_Dashboard>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryPurchaseOrderDetails = _objUnitOfWork._T_PurchaseOrderDetails_Repository.Query();
                    var querySalesOrderDetails = _objUnitOfWork._T_SalesOrderDetails_Repository.Query();
                    var querySalesOrder = _objUnitOfWork._T_SalesOrder_Repository.Query();

                    var vquery = (from p in queryPurchaseOrderDetails
                                  join s in querySalesOrderDetails on p.ProductId equals s.ProductId
                                  join so in querySalesOrder on s.SalesOrderId equals so.Id
                                  select new
                                  {
                                      purchaseItemId = p.ProductId,
                                      purchaseBuyingRate = p.BuyingRate,
                                      purchaseQuantity = p.Quantity,

                                      salesItemId = s.ProductId,
                                      salesBuyingRate = s.SellingPrice,
                                      salesQuantity = s.Quantity,
                                      salesEventDate = so.EventDate
                                  }).ToList();

                    var vObjModelList = vquery.Select(x => new Sys_Dashboard()
                    {
                        PurchaseItemId = Convert.ToString(x.purchaseItemId),
                        PurchaseBuyingRate = x.purchaseBuyingRate,
                        PurchaseQuantity = x.purchaseQuantity,
                        SalesItemId = Convert.ToString(x.salesItemId),
                        SalesRate = x.salesBuyingRate,
                        SalesQuantity = x.salesQuantity,
                        EventDate = x.salesEventDate,

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

        public List<T_Loan> GetLoanList()
        {
            var ObjList = new List<T_Loan>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryLoan = _objUnitOfWork._T_Loan_Repository.Query();
                    var queryLoanDetails = _objUnitOfWork._T_LoanDetails_Repository.Query();

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

        public List<T_OutStanding> GetOutStandingList()
        {
            var ObjList = new List<T_OutStanding>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryOutStanding = _objUnitOfWork._T_OutStanding_Repository.Query();
                    var queryOutStandingDetails = _objUnitOfWork._T_OutStandingDetails_Repository.Query();
                    var querySalesOrder = _objUnitOfWork._T_SalesOrder_Repository.Query();

                    ObjList = (from os in queryOutStanding
                               join so in querySalesOrder on os.SalesOrderId equals so.Id into OsSoJoin
                               from OsObj in OsSoJoin.DefaultIfEmpty()
                               select new
                               {
                                   id = os.Id,
                                   oSNumber = os.OSNumber,
                                   salesOrderId = OsObj.OrderId,
                                   amount = os.Amount,
                                   eventDate = os.EventDate,
                                   name = os.Name,
                                   remark = os.Remark,
                                   createdDate = os.CreatedDate
                               }).ToList().Select(x => new T_OutStanding
                               {
                                   Id = x.id,
                                   OSNumber = x.oSNumber,
                                   SalesOrderNumber = x.salesOrderId,
                                   Amount = x.amount,
                                   PaidAmount = queryOutStandingDetails.Where(s => s.OSId == x.id).ToList().Sum(y => y.PaidAmount),
                                   DueAmount = (x.amount - queryOutStandingDetails.Where(s => s.OSId == x.id).ToList().Sum(y => y.PaidAmount)),
                                   EventDate = x.eventDate,
                                   Name = x.name,
                                   Remark = x.remark,
                                   CreatedDate = x.createdDate
                               }).ToList();
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
