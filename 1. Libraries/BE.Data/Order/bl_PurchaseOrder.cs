using BE.Core;
using BE.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BE.Data.Order
{
    public class bl_PurchaseOrder
    {
        public UnitOfWork _objUnitOfWork = new UnitOfWork();

        public T_PurchaseOrder Create(T_PurchaseOrder ObjPurchaseOrder)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_PurchaseOrder_Repository.Insert(ObjPurchaseOrder);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjPurchaseOrder;
        }

        public T_PurchaseOrderDetails CreatePurchaseOrderDetail(T_PurchaseOrderDetails ObjModel)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_PurchaseOrderDetails_Repository.Insert(ObjModel);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjModel;
        }

        public T_PurchaseOrder Update(T_PurchaseOrder ObjPurchaseOrder)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_PurchaseOrder_Repository.Update(ObjPurchaseOrder);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjPurchaseOrder;
        }

        public T_PurchaseOrderDetails UpdatePurchaseOrderDetail(T_PurchaseOrderDetails ObjModel)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_PurchaseOrderDetails_Repository.Update(ObjModel);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjModel;
        }

        public T_PurchaseOrder Delete(T_PurchaseOrder ObjPurchaseOrder)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_PurchaseOrder_Repository.Delete(ObjPurchaseOrder.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjPurchaseOrder;
        }

        public T_PurchaseOrderDetails DeletePurchaseOrderDetail(T_PurchaseOrderDetails ObjModel)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_PurchaseOrderDetails_Repository.Delete(ObjModel.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjModel;
        }

        public bool BulkDelete(List<T_PurchaseOrder> objList)
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

        public T_PurchaseOrder GetFirstOrDefault(T_PurchaseOrder ObjPurchaseOrder)
        {
            var ReturnCompanyObj = new T_PurchaseOrder();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ReturnCompanyObj = _objUnitOfWork._T_PurchaseOrder_Repository.GetFirstOrDefault(x => x.OrderId == ObjPurchaseOrder.OrderId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ReturnCompanyObj;
        }

        public bool GetFirstOrDefaultPruchaseOrderDetail(T_PurchaseOrderDetails Objmodel)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var vObj = _objUnitOfWork._T_PurchaseOrderDetails_Repository.GetFirstOrDefault(x => x.Id == Objmodel.Id);
                    if (vObj != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public T_PurchaseOrder GetById(Guid UserId)
        {
            var ObjPurchaseOrder = new T_PurchaseOrder();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjPurchaseOrder = _objUnitOfWork._T_PurchaseOrder_Repository.GetById(UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjPurchaseOrder;
        }

        public T_PurchaseOrderDetails GetByIdPurchaseOrderDetail(Guid UserId)
        {
            var ObjPurchaseOrder = new T_PurchaseOrderDetails();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjPurchaseOrder = _objUnitOfWork._T_PurchaseOrderDetails_Repository.GetById(UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjPurchaseOrder;
        }

        public List<T_PurchaseOrder> GetList(T_PurchaseOrder ObjPurchaseOrder)
        {
            var ObjList = new List<T_PurchaseOrder>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryPurchaseOrder = _objUnitOfWork._T_PurchaseOrder_Repository.Query();
                    var queryPurchaseOrderDetails = _objUnitOfWork._T_PurchaseOrderDetails_Repository.Query();

                    if (ObjPurchaseOrder.EventDate != null && ObjPurchaseOrder.EventDate.ToString("yyyy-MM-dd") != "0001-01-01")
                    {
                        queryPurchaseOrder = queryPurchaseOrder.Where(x => x.EventDate == ObjPurchaseOrder.EventDate);
                    }
                    if (!string.IsNullOrWhiteSpace(ObjPurchaseOrder.OrderId))
                    {
                        queryPurchaseOrder = queryPurchaseOrder.Where(x => x.OrderId.Contains(ObjPurchaseOrder.OrderId));
                    }
                    if (!string.IsNullOrWhiteSpace(ObjPurchaseOrder.BillerName))
                    {
                        queryPurchaseOrder = queryPurchaseOrder.Where(x => x.BillerName.Contains(ObjPurchaseOrder.BillerName));
                    }

                    queryPurchaseOrder = queryPurchaseOrder.OrderByDescending(x => x.CreatedDate);

                    var vList = queryPurchaseOrder.ToList();

                    ObjList = vList.Select(x => new T_PurchaseOrder
                    {
                        Id = x.Id,
                        OrderId = x.OrderId,
                        TotalAmount = queryPurchaseOrderDetails.Where(s => s.PurchaseOrderId == x.Id).ToList().Sum(y => y.BuyingPrice),
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

        public List<T_PurchaseOrderDetails> GetPurchaseOrderDetailList(T_PurchaseOrder ObjPurchaseOrder)
        {
            var ObjList = new List<T_PurchaseOrderDetails>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var vList = (from productDetail in _objUnitOfWork._T_PurchaseOrderDetails_Repository.Get()
                                 join purchaseOrder in _objUnitOfWork._T_PurchaseOrder_Repository.Get() on productDetail.PurchaseOrderId equals purchaseOrder.Id

                                 join product in _objUnitOfWork._M_Items_Repository.Get() on productDetail.ItemsId equals product.Id
                                 join uom in _objUnitOfWork._M_UOM_Repository.Get() on productDetail.UomId equals uom.Id

                                 where productDetail.PurchaseOrderId == ObjPurchaseOrder.Id

                                 select new T_PurchaseOrderDetails()
                                 {
                                     Id = productDetail.Id,
                                     PurchaseOrderId = purchaseOrder.Id,
                                     SlNo = productDetail.SlNo,
                                     ItemsId = productDetail.ItemsId,
                                     ItemsName = product.Name,
                                     UomId = productDetail.UomId,
                                     UomName = uom.Name,
                                     Quantity = productDetail.Quantity,
                                     BuyingRate = productDetail.BuyingRate,
                                     BuyingPrice = productDetail.BuyingPrice,
                                     SellingRate = productDetail.SellingRate,
                                     SellingPrice = productDetail.SellingPrice,
                                     DifferenceAmount = productDetail.DifferenceAmount,
                                     Remark = productDetail.Remark,
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

        public string GetOrderNo()
        {
            var vOrderNo = string.Empty;
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var vObj = _objUnitOfWork._T_PurchaseOrder_Repository.Get().OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                    if (vObj != null)
                    {
                        string[] sVal = vObj.OrderId.Split('-');
                        if (!string.IsNullOrWhiteSpace(sVal[2]))
                        {
                            vOrderNo = sVal[0] + "-" + sVal[1] + "-" + String.Format("{0:0000000000}", Convert.ToInt32(sVal[2]) + 1);
                        }
                        else
                            vOrderNo = "BE-BUY-0000000001";
                    }
                    else
                        vOrderNo = "BE-BUY-0000000001";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vOrderNo;
        }

    }
}
