using BE.Core;
using BE.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BE.Data.Items
{
    public class bl_Items
    {
        public UnitOfWork _objUnitOfWork = new UnitOfWork();

        public M_Items Create(M_Items ObjItems)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Items_Repository.Insert(ObjItems);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjItems;
        }

        public M_Items Update(M_Items ObjItems)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Items_Repository.Update(ObjItems);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjItems;
        }

        public M_Items Delete(M_Items ObjItems)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Items_Repository.Delete(ObjItems.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjItems;
        }

        public bool BulkDelete(List<M_Items> objList)
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

        public M_Items GetFirstOrDefault(M_Items ObjItems)
        {
            var ReturnCompanyObj = new M_Items();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ReturnCompanyObj = _objUnitOfWork._M_Items_Repository.GetFirstOrDefault(x => x.Name == ObjItems.Name && x.ItemsTypeId == ObjItems.ItemsTypeId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ReturnCompanyObj;
        }

        public M_Items GetById(Guid UserId)
        {
            var ObjItems = new M_Items();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjItems = _objUnitOfWork._M_Items_Repository.GetById(UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjItems;
        }

        public List<M_Items> GetList(M_Items ObjItems)
        {
            var ObjList = new List<M_Items>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryObjList = _objUnitOfWork._M_Items_Repository.Query();
                    var queryItemsType = _objUnitOfWork._M_ItemsType_Repository.Query();

                    if (!string.IsNullOrWhiteSpace(ObjItems.Name))
                    {
                        queryObjList = queryObjList.Where(x => x.Name.Contains(ObjItems.Name));
                    }
                    if (!string.IsNullOrWhiteSpace(ObjItems.ItemsTypeId.ToString()) && ObjItems.ItemsTypeId.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        queryObjList = queryObjList.Where(x => x.ItemsTypeId == ObjItems.ItemsTypeId);
                    }

                    queryObjList = queryObjList.OrderBy(x => x.Name);

                    var vQuery = queryObjList.Join(queryItemsType, item => item.ItemsTypeId, itemType => itemType.Id, (item, itemType) => new { item, itemType }).ToList();

                    ObjList = vQuery.Select(x => new M_Items
                    {
                        Id = x.item.Id,
                        ItemsTypeId = x.item.ItemsTypeId,
                        ItemsTypeName = x.itemType.Name,
                        Name = x.item.Name,
                        CreatedDate=x.item.CreatedDate
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjList;
        }

        public List<M_ItemsType> GetItemsTypeList()
        {
            var ObjList = new List<M_ItemsType>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjList = _objUnitOfWork._M_ItemsType_Repository.Get().OrderBy(x => x.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjList;
        }

        public List<M_Items> GetStockList(M_Items ObjItems)
        {
            List<M_Items> objList = new List<M_Items>();
            try
            {
                var queryItem = _objUnitOfWork._M_Items_Repository.Query();
                var queryItemType = _objUnitOfWork._M_ItemsType_Repository.Query();
                var queryPurchaseOrderDetails = _objUnitOfWork._T_PurchaseOrderDetails_Repository.Query();
                var querySalesOrderDetails = _objUnitOfWork._T_SalesOrderDetails_Repository.Query();
                var queryUom = _objUnitOfWork._M_UOM_Repository.Query();

                if (!string.IsNullOrWhiteSpace(ObjItems.Name))
                {
                    queryItem = queryItem.Where(x => x.Name.Contains(ObjItems.Name));
                }
                if (!string.IsNullOrWhiteSpace(ObjItems.ItemsTypeId.ToString()) && ObjItems.ItemsTypeId.ToString() != "00000000-0000-0000-0000-000000000000")
                {
                    queryItem = queryItem.Where(x => x.ItemsTypeId == ObjItems.ItemsTypeId);
                }

                var vObjQuery = from i in queryItem

                                join it in queryItemType on i.ItemsTypeId equals it.Id
                                join p in queryPurchaseOrderDetails on i.Id equals p.ItemsId into ip
                                from ipj in ip.DefaultIfEmpty()
                                join u in queryUom on ipj.UomId equals u.Id into iu
                                from iuj in iu.DefaultIfEmpty()
                                select new
                                {
                                    itemId = i.Id,
                                    itemName = i.Name,
                                    itemTypeName = it.Name,
                                    uomName = iuj.Name,
                                    purchaseQuantity = ipj.Quantity != null ? ipj.Quantity : 0,
                                    salesQuantity = (querySalesOrderDetails.Where(x => x.ItemsId == i.Id)).ToList().Count > 0 ? querySalesOrderDetails.Where(x => x.ItemsId == i.Id).ToList().Sum(c => c.Quantity) : 0,
                                    createDate=i.CreatedDate
                                };

                objList = vObjQuery.ToList().Select(x => new M_Items()
                {
                    Id = x.itemId,
                    Name = x.itemName,
                    ItemsTypeName = x.itemTypeName,
                    UomName = x.uomName,
                    PurchaseQuantity = x.purchaseQuantity,
                    SalesQuantity = x.salesQuantity,
                    StockQuantity = (Convert.ToDecimal(x.purchaseQuantity) - Convert.ToDecimal(x.salesQuantity)),
                    CreatedDate=x.createDate
                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objList;
        }
    }
}
