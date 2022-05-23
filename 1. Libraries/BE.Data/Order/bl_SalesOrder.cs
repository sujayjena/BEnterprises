using BE.Core;
using BE.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BE.Data.Order
{
    public class bl_SalesOrder
    {
        public UnitOfWork _objUnitOfWork = new UnitOfWork();

        public T_SalesOrder Create(T_SalesOrder ObjSalesOrder)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_SalesOrder_Repository.Insert(ObjSalesOrder);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjSalesOrder;
        }

        public T_SalesOrder Update(T_SalesOrder ObjSalesOrder)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_SalesOrder_Repository.Update(ObjSalesOrder);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjSalesOrder;
        }

        public T_SalesOrder Delete(T_SalesOrder ObjSalesOrder)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_SalesOrder_Repository.Delete(ObjSalesOrder.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjSalesOrder;
        }

        public bool BulkDelete(List<T_SalesOrder> objList)
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

        public T_SalesOrder GetFirstOrDefault(T_SalesOrder ObjSalesOrder)
        {
            var ReturnCompanyObj = new T_SalesOrder();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ReturnCompanyObj = _objUnitOfWork._T_SalesOrder_Repository.GetFirstOrDefault(x => x.OrderId == ObjSalesOrder.OrderId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ReturnCompanyObj;
        }

        public T_SalesOrder GetById(Guid UserId)
        {
            var ObjSalesOrder = new T_SalesOrder();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjSalesOrder = _objUnitOfWork._T_SalesOrder_Repository.GetById(UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjSalesOrder;
        }

        public List<T_SalesOrder> GetList(T_SalesOrder ObjSalesOrder)
        {
            var ObjList = new List<T_SalesOrder>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var querySalesOrder = _objUnitOfWork._T_SalesOrder_Repository.Query();
                    var querySalesOrderDetails = _objUnitOfWork._T_SalesOrderDetails_Repository.Query();

                    if (ObjSalesOrder.EventDate != null && ObjSalesOrder.EventDate.ToString("yyyy-MM-dd") != "0001-01-01")
                    {
                        querySalesOrder = querySalesOrder.Where(x => x.EventDate == ObjSalesOrder.EventDate);
                    }
                    if (!string.IsNullOrWhiteSpace(ObjSalesOrder.OrderId))
                    {
                        querySalesOrder = querySalesOrder.Where(x => x.OrderId.Contains(ObjSalesOrder.OrderId));
                    }
                    if (!string.IsNullOrWhiteSpace(ObjSalesOrder.BillerName))
                    {
                        querySalesOrder = querySalesOrder.Where(x => x.BillerName.Contains(ObjSalesOrder.BillerName));
                    }
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

        public string GetOrderNo()
        {
            var vOrderNo = string.Empty;
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var vObj = _objUnitOfWork._T_SalesOrder_Repository.Get().OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                    if (vObj != null)
                    {
                        string[] sVal = vObj.OrderId.Split('-');
                        if (!string.IsNullOrWhiteSpace(sVal[2]))
                        {
                            vOrderNo = sVal[0] + "-" + sVal[1] + "-" + String.Format("{0:0000000000}", Convert.ToInt32(sVal[2]) + 1);
                        }
                        else
                            vOrderNo = "BE-SALE-0000000001";
                    }
                    else
                        vOrderNo = "BE-SALE-0000000001";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vOrderNo;
        }

        // For Edit Detail Pages 

        public T_SalesOrderDetails CreateSalesOrderDetail(T_SalesOrderDetails ObjModel)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_SalesOrderDetails_Repository.Insert(ObjModel);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjModel;
        }

        public T_SalesOrderDetails UpdateSalesOrderDetail(T_SalesOrderDetails ObjModel)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_SalesOrderDetails_Repository.Update(ObjModel);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjModel;
        }

        public T_SalesOrderDetails DeleteSalesOrderDetail(T_SalesOrderDetails ObjModel)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_SalesOrderDetails_Repository.Delete(ObjModel.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjModel;
        }

        public List<T_SalesOrderDetails> GetSalesOrderDetailList(T_SalesOrder ObjSalesOrder)
        {
            var ObjList = new List<T_SalesOrderDetails>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var vList = (from productDetail in _objUnitOfWork._T_SalesOrderDetails_Repository.Get()
                                 join SalesOrder in _objUnitOfWork._T_SalesOrder_Repository.Get() on productDetail.SalesOrderId equals SalesOrder.Id

                                 join product in _objUnitOfWork._M_Items_Repository.Get() on productDetail.ItemsId equals product.Id
                                 join uom in _objUnitOfWork._M_UOM_Repository.Get() on productDetail.UomId equals uom.Id

                                 join brand in _objUnitOfWork._M_Brand_Repository.Get() on productDetail.BrandId equals brand.Id into _brand
                                 from brand in _brand.DefaultIfEmpty()

                                 join guage in _objUnitOfWork._M_Guage_Repository.Get() on productDetail.GuageId equals guage.Id into _guage
                                 from guage in _guage.DefaultIfEmpty()

                                 where productDetail.SalesOrderId == ObjSalesOrder.Id

                                 select new T_SalesOrderDetails()
                                 {
                                     Id = productDetail.Id,
                                     SalesOrderId = SalesOrder.Id,
                                     SlNo = productDetail.SlNo,
                                     ItemsId = productDetail.ItemsId,
                                     ItemsName = product.Name,
                                     BrandId = productDetail.BrandId,
                                     BrandName = brand.Name,
                                     GuageId = productDetail.GuageId,
                                     GuageName = guage.Name,
                                     UomId = productDetail.UomId,
                                     UomName = uom.Name,
                                     StockQuantity = productDetail.StockQuantity,
                                     Quantity = productDetail.Quantity,
                                     LatestPrice = productDetail.LatestPrice,
                                     SellingPrice = productDetail.SellingPrice,
                                     CreatedDate = productDetail.CreatedDate
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

        public T_SalesOrderDetails GetByIdSalesOrderDetail(Guid UserId)
        {
            var ObjSalesOrder = new T_SalesOrderDetails();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjSalesOrder = _objUnitOfWork._T_SalesOrderDetails_Repository.GetById(UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjSalesOrder;
        }

        public decimal GetItemsStockDetail(T_PurchaseOrderDetails Objmodel)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var vQueryPurchaseOrder = _objUnitOfWork._T_PurchaseOrderDetails_Repository.Query();
                    var vQuerySalesOrder = _objUnitOfWork._T_SalesOrderDetails_Repository.Query();

                    if (!string.IsNullOrWhiteSpace(Convert.ToString(Objmodel.ItemsId)) && !string.IsNullOrWhiteSpace(Convert.ToString(Objmodel.BrandId)))
                    {
                        vQueryPurchaseOrder = vQueryPurchaseOrder.Where(x => x.ItemsId == Objmodel.ItemsId && x.BrandId == Objmodel.BrandId);
                    }
                    else if (!string.IsNullOrWhiteSpace(Convert.ToString(Objmodel.ItemsId)))
                    {
                        vQueryPurchaseOrder = vQueryPurchaseOrder.Where(x => x.ItemsId == Objmodel.ItemsId);
                    }
                    else if (!string.IsNullOrWhiteSpace(Convert.ToString(Objmodel.BrandId)))
                    {
                        vQueryPurchaseOrder = vQueryPurchaseOrder.Where(x => x.BrandId == Objmodel.BrandId);
                    }

                    if (!string.IsNullOrWhiteSpace(Convert.ToString(Objmodel.ItemsId)) && !string.IsNullOrWhiteSpace(Convert.ToString(Objmodel.BrandId)))
                    {
                        vQuerySalesOrder = vQuerySalesOrder.Where(x => x.ItemsId == Objmodel.ItemsId && x.BrandId == Objmodel.BrandId);
                    }
                    else if (!string.IsNullOrWhiteSpace(Convert.ToString(Objmodel.ItemsId)))
                    {
                        vQuerySalesOrder = vQuerySalesOrder.Where(x => x.ItemsId == Objmodel.ItemsId);
                    }
                    else if (!string.IsNullOrWhiteSpace(Convert.ToString(Objmodel.BrandId)))
                    {
                        vQuerySalesOrder = vQuerySalesOrder.Where(x => x.BrandId == Objmodel.BrandId);
                    }

                    var vTotalPurchaseOrderQuantity = vQueryPurchaseOrder.ToList().Sum(x => x.Quantity);
                    var vTotalSalesOrderQuantity = vQuerySalesOrder.ToList().Sum(x => x.Quantity);

                    return (vTotalPurchaseOrderQuantity - vTotalSalesOrderQuantity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T_PurchaseOrderDetails> GetItemsLatestStockDetail(T_PurchaseOrderDetails Objmodel)
        {
            List<T_PurchaseOrderDetails> objPurchaseList = new List<T_PurchaseOrderDetails>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var vQuery = _objUnitOfWork._T_PurchaseOrderDetails_Repository.Query();

                    if (!string.IsNullOrWhiteSpace(Convert.ToString(Objmodel.ItemsId)) && !string.IsNullOrWhiteSpace(Convert.ToString(Objmodel.BrandId)))
                    {
                        vQuery = vQuery.Where(x => x.ItemsId == Objmodel.ItemsId && x.BrandId == Objmodel.BrandId);
                    }
                    else if (!string.IsNullOrWhiteSpace(Convert.ToString(Objmodel.ItemsId)))
                    {
                        vQuery = vQuery.Where(x => x.ItemsId == Objmodel.ItemsId);
                    }
                    else if (!string.IsNullOrWhiteSpace(Convert.ToString(Objmodel.BrandId)))
                    {
                        vQuery = vQuery.Where(x => x.BrandId == Objmodel.BrandId);
                    }

                    objPurchaseList = vQuery.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objPurchaseList;
        }

        public List<M_Brand> GetItemsBrand(T_PurchaseOrderDetails Objmodel)
        {
            List<M_Brand> objBrandList = new List<M_Brand>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var vQuery = _objUnitOfWork._T_PurchaseOrderDetails_Repository.Query();
                    var vQueryBrand = _objUnitOfWork._M_Brand_Repository.Query();

                    if (!string.IsNullOrWhiteSpace(Convert.ToString(Objmodel.ItemsId)))
                    {
                        vQuery = vQuery.Where(x => x.ItemsId == Objmodel.ItemsId && x.Quantity > 0);
                    }
                    var vJoinQuery = vQuery.Join(vQueryBrand, p => p.BrandId, b => b.Id, (p, b) => new { p, b }).ToList();

                    var vObjList = vJoinQuery.Select(x => new M_Brand()
                    {
                        Id = x.b.Id,
                        Name = x.b.Name
                    }).ToList();

                    objBrandList = vObjList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objBrandList;
        }
    }
}
