using BE.Core;
using BE.Data.Brand;
using BE.Data.Guage;
using BE.Data.Items;
using BE.Data.ItemsType;
using BE.Data.Order;
using BE.Data.Supplier;
using BE.Data.UOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BE.Web.Controllers
{
    [CustomAuthentication]
    public class SalesOrderController : Controller
    {
        #region Gobal Variable

        protected readonly CustomAuthentication _objAuthentication = new CustomAuthentication();
        protected readonly bl_SalesOrder _blSalesOrder = new bl_SalesOrder();
        protected readonly bl_Supplier _blSupplier = new bl_Supplier();
        protected readonly bl_Items _blItems = new bl_Items();
        protected readonly bl_Brand _blBrand = new bl_Brand();
        protected readonly bl_ItemsType _blItemsType = new bl_ItemsType();
        protected readonly bl_Guage _blGuage = new bl_Guage();
        protected readonly bl_Uom _blUOM = new bl_Uom();
        //protected readonly bl_PurchaseOrder _blPurchaseOrder = new bl_PurchaseOrder();
        T_SalesOrder model = new T_SalesOrder();

        #endregion

        #region Constructor

        public SalesOrderController()
        {
        }

        #endregion

        // GET: SalesOrder
        public ActionResult Index()
        {
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            model.OrderId = _blSalesOrder.GetOrderNo();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(T_SalesOrder ObjSalesOrder)
        {
            try
            {
                bool bAnyError = false;
                if (ModelState.IsValid)
                {
                    var vNameExists = _blSalesOrder.GetFirstOrDefault(ObjSalesOrder);
                    if (vNameExists != null)
                    {
                        ViewBag.ErrorMsg = "Order No alreay exists in our system";
                        bAnyError = true;
                    }
                    if (bAnyError == false)
                    {
                        T_SalesOrder _Obj_T_SalesOrder = new T_SalesOrder()
                        {
                            Id = Guid.NewGuid(),
                            EventDate = Convert.ToDateTime(ObjSalesOrder.EventDate),
                            OrderId = ObjSalesOrder.OrderId,
                            BillerName = ObjSalesOrder.BillerName,
                            CreatedBy = _objAuthentication.UserName,
                            CreatedDate = DateTime.Now
                        };
                        var vReturnObj = _blSalesOrder.Create(_Obj_T_SalesOrder);
                    }
                }
                else
                {
                    bAnyError = true;
                }

                if (bAnyError)
                {
                    return View(ObjSalesOrder);
                }
                return RedirectToAction("Index", "SalesOrder");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            var model = new T_SalesOrder();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var vDetails = _blSalesOrder.GetById(new Guid(Id));
                if (vDetails != null)
                {
                    model.EventDate = Convert.ToDateTime(vDetails.EventDate);
                    model.OrderId = vDetails.OrderId;
                    model.BillerName = vDetails.BillerName;
                    model.SalesOrderId = vDetails.Id;
                }
            }

            M_Items ObjItemsModel = new M_Items();
            model.ItemsList = _blItems.GetList(ObjItemsModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x => x.Text).ToList();
            model.ItemsList.Insert(0, new SelectListItem() { Text = "Select", Value = "0", Selected = true });

            M_Brand objBrandModel = new M_Brand();
            model.BrandList = _blBrand.GetList(objBrandModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x => x.Text).ToList();
            model.BrandList.Insert(0, new SelectListItem() { Text = "Select", Value = "0", Selected = true });

            M_Guage objGuageModel = new M_Guage();
            model.GuageList = _blGuage.GetList(objGuageModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x => x.Text).ToList();
            model.GuageList.Insert(0, new SelectListItem() { Text = "Select", Value = "0", Selected = true });

            M_UOM objUomModel = new M_UOM();
            model.UomList = _blUOM.GetList(objUomModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x => x.Text).ToList();
            model.UomList.Insert(0, new SelectListItem() { Text = "Select", Value = "0", Selected = true });

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(T_SalesOrder ObjModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vObj = _blSalesOrder.GetById(ObjModel.Id);
                    if (vObj != null)
                    {
                        vObj.EventDate = Convert.ToDateTime(ObjModel.EventDate);
                        vObj.BillerName = ObjModel.BillerName;

                        vObj.ModifyBy = _objAuthentication.UserName;
                        vObj.ModifyDate = DateTime.Now;
                        _blSalesOrder.Update(vObj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "SalesOrder");
        }

        [HttpPost]
        public ActionResult Delete(T_SalesOrder ObjSalesOrder)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjSalesOrder.Id)))
                {
                    var vObj = _blSalesOrder.GetById(ObjSalesOrder.Id);
                    if (vObj != null)
                    {
                        _blSalesOrder.Delete(ObjSalesOrder);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjSalesOrder);
        }

        [HttpPost]
        public ActionResult BulkDelete(List<T_SalesOrder> DeletedRecord)
        {
            try
            {
                if (DeletedRecord.Count > 0)
                {
                    var vUser = _blSalesOrder.BulkDelete(DeletedRecord);
                    if (vUser)
                    {
                        return Json(new { Result = true, Message = "Sucess" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { Result = false, Message = "Faield" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetList(T_SalesOrder ObjSalesOrder)
        {
            List<T_SalesOrder> ObjList = new List<T_SalesOrder>();
            try
            {
                ObjList = _blSalesOrder.GetList(ObjSalesOrder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjList, JsonRequestBehavior.AllowGet);
        }


        // ********************************** Sales Order Details ************************************* // 

        [HttpPost]
        public ActionResult CreateSalesOrderDetails(T_SalesOrderDetails ObjModel)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjModel.Id)))
                {
                    var vObjExists = _blSalesOrder.GetByIdSalesOrderDetail(ObjModel.Id);

                    if (vObjExists == null)
                    {
                        Guid guidId = Guid.NewGuid();
                        Guid guidSalesOrderId = Guid.Parse(Convert.ToString(ObjModel.SalesOrderId));
                        Guid guidItemsId = Guid.Parse(Convert.ToString(ObjModel.ItemsId));
                        Guid guidBrandId = Guid.Parse(Convert.ToString(ObjModel.BrandId));
                        Guid guidGuageId = Guid.Parse(Convert.ToString(ObjModel.GuageId));
                        Guid guidUomId = Guid.Parse(Convert.ToString(ObjModel.UomId));

                        T_SalesOrderDetails _Obj_T_SalesOrder = new T_SalesOrderDetails()
                        {
                            Id = guidId,
                            SalesOrderId = guidSalesOrderId,
                            SlNo = ObjModel.SlNo,
                            ItemsId = guidItemsId,
                            BrandId = guidBrandId,
                            GuageId = guidGuageId,
                            UomId = guidUomId,
                            StockQuantity = ObjModel.StockQuantity,
                            Quantity = ObjModel.Quantity,
                            LatestPrice = ObjModel.LatestPrice,
                            SellingPrice = ObjModel.SellingPrice,
                            CreatedBy = _objAuthentication.UserName,
                            CreatedDate = DateTime.Now
                        };
                        _blSalesOrder.CreateSalesOrderDetail(_Obj_T_SalesOrder);

                        return Json(new { data = ObjModel, Success = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Guid guidId = Guid.Parse(Convert.ToString(ObjModel.Id));
                        Guid guidSalesOrderId = Guid.Parse(Convert.ToString(ObjModel.SalesOrderId));
                        Guid guidItemsId = Guid.Parse(Convert.ToString(ObjModel.ItemsId));
                        Guid guidBrandId = Guid.Parse(Convert.ToString(ObjModel.BrandId));
                        Guid guidGuageId = Guid.Parse(Convert.ToString(ObjModel.GuageId));
                        Guid guidUomId = Guid.Parse(Convert.ToString(ObjModel.UomId));

                        vObjExists.ItemsId = guidItemsId;
                        vObjExists.BrandId = guidBrandId;
                        vObjExists.GuageId = guidGuageId;
                        vObjExists.UomId = guidUomId;
                        vObjExists.Quantity = ObjModel.Quantity;
                        vObjExists.StockQuantity = ObjModel.StockQuantity;
                        vObjExists.Quantity = ObjModel.Quantity;
                        vObjExists.LatestPrice = ObjModel.LatestPrice;
                        vObjExists.SellingPrice = ObjModel.SellingPrice;

                        vObjExists.ModifyBy = _objAuthentication.UserName;
                        vObjExists.ModifyDate = DateTime.Now;
                        _blSalesOrder.UpdateSalesOrderDetail(vObjExists);

                        return Json(new { data = ObjModel, Success = true }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { data = ObjModel, Success = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteSalesOrderDetails(T_SalesOrderDetails ObjModel)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjModel.Id)))
                {
                    var vObj = _blSalesOrder.GetByIdSalesOrderDetail(ObjModel.Id);
                    if (vObj != null)
                    {
                        _blSalesOrder.DeleteSalesOrderDetail(ObjModel);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjModel);
        }

        [HttpPost]
        public JsonResult GetSalesOrderDetailList(T_SalesOrder ObjSalesOrder)
        {
            var ObjList = new List<T_SalesOrderDetails>();
            try
            {
                ObjList = _blSalesOrder.GetSalesOrderDetailList(ObjSalesOrder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetItemsBrand(T_PurchaseOrderDetails ObjPurchaseOrder)
        {
            var ObjModelList = new List<M_Brand>();
            try
            {
                ObjModelList = _blSalesOrder.GetItemsBrand(ObjPurchaseOrder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { data = ObjModelList, Success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetItemsStock(T_PurchaseOrderDetails ObjPurchaseOrder)
        {
            var ObjModel = new T_PurchaseOrderDetails();
            try
            {
                ObjModel.Quantity = _blSalesOrder.GetItemsStockDetail(ObjPurchaseOrder);

                var vSellingPrice = _blSalesOrder.GetItemsLatestStockDetail(ObjPurchaseOrder).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                if (vSellingPrice != null)
                {
                    ObjModel.SellingRate = vSellingPrice.SellingRate;
                    ObjModel.UomId = vSellingPrice.UomId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { data = ObjModel, Success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ExportToExcel(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return null;
            }
            T_SalesOrder ObjItem = new T_SalesOrder()
            {
                Id = Guid.Parse(Id)
            };
            var vObjList = _blSalesOrder.GetSalesOrderDetailList(ObjItem);

            // ExcelPackageF dll Need
            OfficeOpenXml.ExcelPackage Ep = new OfficeOpenXml.ExcelPackage();
            OfficeOpenXml.ExcelWorksheet workSheet = Ep.Workbook.Worksheets.Add("Report");

            string sEndColumn = "J1";
            workSheet.Cells[string.Format("A1:{0}", sEndColumn)].Style.Font.Bold = true;
            workSheet.Cells[string.Format("A1:{0}", sEndColumn)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            workSheet.Cells[string.Format("A1:{0}", sEndColumn)].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSeaGreen);
            var allCells = workSheet.Cells[string.Format("A1:{0}", sEndColumn)];
            var cellFont = allCells.Style.Font;


            workSheet.Cells["A1"].Value = "Slno";
            //sheet["A3:A4"].Merge();
            workSheet.Cells["B1"].Value = "Item";
            workSheet.Cells["C1"].Value = "Brand";
            workSheet.Cells["D1"].Value = "Guage";
            workSheet.Cells["E1"].Value = "Stock Quantity";
            workSheet.Cells["F1"].Value = "Quantity";
            workSheet.Cells["G1"].Value = "UOM";
            workSheet.Cells["H1"].Value = "Latest Price";
            workSheet.Cells["I1"].Value = "Selling Price";
            workSheet.Cells["J1"].Value = "Created Dated";

            string dateformat = "dd-MM-yyyy";
            int row = 2;
            foreach (var item in vObjList)
            {
                //workSheet.Cells[string.Format("A{0}", row)].Value = item.EventDate;
                //workSheet.Cells[string.Format("A{0}", row)].Style.Numberformat.Format = dateformat;

                workSheet.Cells[string.Format("A{0}", row)].Value = item.SlNo;
                workSheet.Cells[string.Format("B{0}", row)].Value = item.ItemsName;
                workSheet.Cells[string.Format("C{0}", row)].Value = item.BrandName;
                workSheet.Cells[string.Format("D{0}", row)].Value = item.GuageName;
                workSheet.Cells[string.Format("E{0}", row)].Value = item.StockQuantity;
                workSheet.Cells[string.Format("F{0}", row)].Value = item.Quantity;
                workSheet.Cells[string.Format("G{0}", row)].Value = item.UomName;
                workSheet.Cells[string.Format("H{0}", row)].Value = item.LatestPrice;
                workSheet.Cells[string.Format("I{0}", row)].Value = item.SellingPrice;

                workSheet.Cells[string.Format("J{0}", row)].Value = item.CreatedDate;
                workSheet.Cells[string.Format("J{0}", row)].Style.Numberformat.Format = dateformat;
                row++;
            }

            workSheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Expense.csv" + DateTime.Now.ToString("s"));
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();

            return View();
        }


        // ******************************************************************************************** //

        [HttpGet]
        public ActionResult SupplierList()
        {
            M_Supplier objModel = new M_Supplier();
            var vlist = _blSupplier.GetList(objModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return Json(vlist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ProductList()
        {
            M_Items objModel = new M_Items();
            var vlist = _blItems.GetList(objModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return Json(vlist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult BrandList()
        {
            M_Brand objModel = new M_Brand();
            var vlist = _blBrand.GetList(objModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return Json(vlist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ItemsTypeList()
        {
            M_ItemsType objModel = new M_ItemsType();
            var vlist = _blItemsType.GetList(objModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return Json(vlist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GuageList()
        {
            M_Guage objModel = new M_Guage();
            var vlist = _blGuage.GetList(objModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return Json(vlist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UomList()
        {
            M_UOM objModel = new M_UOM();
            var vlist = _blUOM.GetList(objModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return Json(vlist, JsonRequestBehavior.AllowGet);
        }
    }
}