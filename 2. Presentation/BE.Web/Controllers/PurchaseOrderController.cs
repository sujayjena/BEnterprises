using BE.Core;
using BE.Data.Brand;
using BE.Data.Guage;
using BE.Data.ItemsType;
using BE.Data.Order;
using BE.Data.Items;
using BE.Data.Supplier;
using BE.Data.UOM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BE.Web.Controllers
{
    [CustomAuthentication]
    public class PurchaseOrderController : Controller
    {
        #region Gobal Variable

        protected readonly CustomAuthentication _objAuthentication = new CustomAuthentication();
        protected readonly bl_PurchaseOrder _blPurchaseOrder = new bl_PurchaseOrder();
        protected readonly bl_Supplier _blSupplier = new bl_Supplier();
        protected readonly bl_Items _blItems = new bl_Items();
        protected readonly bl_Brand _blBrand = new bl_Brand();
        protected readonly bl_ItemsType _blItemsType = new bl_ItemsType();
        protected readonly bl_Guage _blGuage = new bl_Guage();
        protected readonly bl_Uom _blUOM = new bl_Uom();
        T_PurchaseOrder model = new T_PurchaseOrder();

        #endregion

        // GET: PurchaseOrder
        public ActionResult Index()
        {
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            model.OrderId = _blPurchaseOrder.GetOrderNo();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(T_PurchaseOrder ObjPurchaseOrder)
        {
            try
            {
                bool bAnyError = false;
                if (ModelState.IsValid)
                {
                    var vNameExists = _blPurchaseOrder.GetFirstOrDefault(ObjPurchaseOrder);
                    if (vNameExists != null)
                    {
                        ViewBag.ErrorMsg = "Order No alreay exists in our system";
                        bAnyError = true;
                    }
                    if (bAnyError == false)
                    {
                        T_PurchaseOrder _Obj_T_PurchaseOrder = new T_PurchaseOrder()
                        {
                            Id = Guid.NewGuid(),
                            EventDate = Convert.ToDateTime(ObjPurchaseOrder.EventDate),
                            OrderId = ObjPurchaseOrder.OrderId,
                            BillerName = ObjPurchaseOrder.BillerName,
                            CreatedBy = _objAuthentication.UserName,
                            CreatedDate = DateTime.Now
                        };
                        var vReturnObj = _blPurchaseOrder.Create(_Obj_T_PurchaseOrder);
                    }
                }
                else
                {
                    bAnyError = true;
                }

                if (bAnyError)
                {
                    return View(ObjPurchaseOrder);
                }
                return RedirectToAction("Index", "PurchaseOrder");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult CreatePurchaseOrderDetails(T_PurchaseOrderDetails ObjModel)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjModel.Id)))
                {
                    var vObjExists = _blPurchaseOrder.GetByIdPurchaseOrderDetail(ObjModel.Id);

                    if (vObjExists == null)
                    {
                        Guid guidId = Guid.NewGuid();
                        Guid guidPurchaseOrderId = Guid.Parse(Convert.ToString(ObjModel.PurchaseOrderId));
                        Guid guidItemsId = Guid.Parse(Convert.ToString(ObjModel.ItemsId));
                        Guid guidBrandId = Guid.Parse(Convert.ToString(ObjModel.BrandId));
                        Guid guidGuageId = Guid.Parse(Convert.ToString(ObjModel.GuageId));
                        Guid guidUomId = Guid.Parse(Convert.ToString(ObjModel.UomId));

                        T_PurchaseOrderDetails _Obj_T_PurchaseOrder = new T_PurchaseOrderDetails()
                        {
                            Id = guidId,
                            PurchaseOrderId = guidPurchaseOrderId,
                            SlNo = ObjModel.SlNo,
                            ItemsId = guidItemsId,
                            BrandId = guidBrandId,
                            GuageId = guidGuageId,
                            UomId = guidUomId,
                            Quantity = ObjModel.Quantity,
                            BuyingRate = ObjModel.BuyingRate,
                            BuyingPrice = ObjModel.BuyingPrice,
                            SellingRate = ObjModel.SellingRate,
                            SellingPrice = ObjModel.SellingPrice,
                            DifferenceAmount = ObjModel.DifferenceAmount,
                            Remark = ObjModel.Remark,
                            CreatedBy = _objAuthentication.UserName,
                            CreatedDate = DateTime.Now
                        };
                        _blPurchaseOrder.CreatePurchaseOrderDetail(_Obj_T_PurchaseOrder);

                        return Json(new { data = ObjModel, Success = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Guid guidId = Guid.Parse(Convert.ToString(ObjModel.Id));
                        Guid guidPurchaseOrderId = Guid.Parse(Convert.ToString(ObjModel.PurchaseOrderId));
                        Guid guidItemsId = Guid.Parse(Convert.ToString(ObjModel.ItemsId));
                        Guid guidBrandId = Guid.Parse(Convert.ToString(ObjModel.BrandId));
                        Guid guidGuageId = Guid.Parse(Convert.ToString(ObjModel.GuageId));
                        Guid guidUomId = Guid.Parse(Convert.ToString(ObjModel.UomId));

                        vObjExists.ItemsId = guidItemsId;
                        vObjExists.BrandId = guidBrandId;
                        vObjExists.GuageId = guidGuageId;
                        vObjExists.UomId = guidUomId;
                        vObjExists.Quantity = ObjModel.Quantity;
                        vObjExists.BuyingRate = ObjModel.BuyingRate;
                        vObjExists.BuyingPrice = ObjModel.BuyingPrice;
                        vObjExists.SellingRate = ObjModel.SellingRate;
                        vObjExists.SellingPrice = ObjModel.SellingPrice;
                        vObjExists.DifferenceAmount = ObjModel.DifferenceAmount;
                        vObjExists.Remark = ObjModel.Remark;

                        vObjExists.ModifyBy = _objAuthentication.UserName;
                        vObjExists.ModifyDate = DateTime.Now;
                        _blPurchaseOrder.UpdatePurchaseOrderDetail(vObjExists);

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

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            var model = new T_PurchaseOrder();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var vDetails = _blPurchaseOrder.GetById(new Guid(Id));
                if (vDetails != null)
                {
                    model.PurchaseOrderId = vDetails.Id;
                    model.EventDate = Convert.ToDateTime(vDetails.EventDate);
                    model.OrderId = vDetails.OrderId;
                    model.BillerName = vDetails.BillerName;
                }
            }
            M_Items ObjItemsModel = new M_Items();
            model.ItemsList = _blItems.GetList(ObjItemsModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x=>x.Text).ToList();

            M_Brand objBrandModel = new M_Brand();
            model.BrandList = _blBrand.GetList(objBrandModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x => x.Text).ToList();

            M_Guage objGuageModel = new M_Guage();
            model.GuageList = _blGuage.GetList(objGuageModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x => x.Text).ToList();

            M_UOM objUomModel = new M_UOM();
            model.UomList = _blUOM.GetList(objUomModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x => x.Text).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(T_PurchaseOrder ObjModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vObj = _blPurchaseOrder.GetById(ObjModel.Id);
                    if (vObj != null)
                    {
                        vObj.EventDate = Convert.ToDateTime(ObjModel.EventDate);
                        vObj.BillerName = ObjModel.BillerName;

                        vObj.ModifyBy = _objAuthentication.UserName;
                        vObj.ModifyDate = DateTime.Now;
                        _blPurchaseOrder.Update(vObj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "PurchaseOrder");
        }

        [HttpPost]
        public ActionResult Delete(T_PurchaseOrder ObjPurchaseOrder)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjPurchaseOrder.Id)))
                {
                    var vObj = _blPurchaseOrder.GetById(ObjPurchaseOrder.Id);
                    if (vObj != null)
                    {
                        _blPurchaseOrder.Delete(ObjPurchaseOrder);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjPurchaseOrder);
        }

        [HttpPost]
        public ActionResult DeletePurchaseOrderDetails(T_PurchaseOrderDetails ObjModel)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjModel.Id)))
                {
                    var vObj = _blPurchaseOrder.GetByIdPurchaseOrderDetail(ObjModel.Id);
                    if (vObj != null)
                    {
                        _blPurchaseOrder.DeletePurchaseOrderDetail(ObjModel);
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
        public ActionResult BulkDelete(List<T_PurchaseOrder> DeletedRecord)
        {
            try
            {
                if (DeletedRecord.Count > 0)
                {
                    var vUser = _blPurchaseOrder.BulkDelete(DeletedRecord);
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
        public JsonResult GetList(T_PurchaseOrder ObjPurchaseOrder)
        {
            List<T_PurchaseOrder> ObjList = new List<T_PurchaseOrder>();
            try
            {
                ObjList = _blPurchaseOrder.GetList(ObjPurchaseOrder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPurchaseOrderDetailList(T_PurchaseOrder ObjPurchaseOrder)
        {
            var ObjList = new List<T_PurchaseOrderDetails>();
            try
            {
                ObjList = _blPurchaseOrder.GetPurchaseOrderDetailList(ObjPurchaseOrder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjList, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult SupplierList()
        {
            M_Supplier objModel = new M_Supplier();
            var vlist = _blSupplier.GetList(objModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x => x.Text).ToList();
            return Json(vlist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ItemsList()
        {
            M_Items objModel = new M_Items();
            var vlist = _blItems.GetList(objModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x=>x.Text).ToList();
            return Json(vlist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult BrandList()
        {
            M_Brand objModel = new M_Brand();
            var vlist = _blBrand.GetList(objModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x => x.Text).ToList();
            return Json(vlist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ItemsTypeList()
        {
            M_ItemsType objModel = new M_ItemsType();
            var vlist = _blItemsType.GetList(objModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x => x.Text).ToList();
            return Json(vlist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GuageList()
        {
            M_Guage objModel = new M_Guage();
            var vlist = _blGuage.GetList(objModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x => x.Text).ToList();
            return Json(vlist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UomList()
        {
            M_UOM objModel = new M_UOM();
            var vlist = _blUOM.GetList(objModel).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return Json(vlist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ExportToExcel(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return null;
            }
            T_PurchaseOrder ObjItem = new T_PurchaseOrder()
            {
                Id = Guid.Parse(Id)
            };
            var vObjList = _blPurchaseOrder.GetPurchaseOrderDetailList(ObjItem);

            // ExcelPackageF dll Need
            OfficeOpenXml.ExcelPackage Ep = new OfficeOpenXml.ExcelPackage();
            OfficeOpenXml.ExcelWorksheet workSheet = Ep.Workbook.Worksheets.Add("Report");

            string sEndColumn = "M1";
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
            workSheet.Cells["E1"].Value = "Quantity";
            workSheet.Cells["F1"].Value = "UOM";
            workSheet.Cells["G1"].Value = "Buying Rate";
            workSheet.Cells["H1"].Value = "Buying Price";
            workSheet.Cells["I1"].Value = "Selling Rate";
            workSheet.Cells["J1"].Value = "Selling Price";
            workSheet.Cells["K1"].Value = "Profit";
            workSheet.Cells["L1"].Value = "Remark";
            workSheet.Cells["M1"].Value = "Created Dated";

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
                workSheet.Cells[string.Format("E{0}", row)].Value = item.Quantity;
                workSheet.Cells[string.Format("F{0}", row)].Value = item.UomName;
                workSheet.Cells[string.Format("G{0}", row)].Value = item.BuyingRate;
                workSheet.Cells[string.Format("H{0}", row)].Value = item.BuyingPrice;
                workSheet.Cells[string.Format("I{0}", row)].Value = item.SellingRate;
                workSheet.Cells[string.Format("J{0}", row)].Value = item.SellingPrice;
                workSheet.Cells[string.Format("K{0}", row)].Value = item.DifferenceAmount;
                workSheet.Cells[string.Format("L{0}", row)].Value = item.Remark;

                workSheet.Cells[string.Format("M{0}", row)].Value = item.CreatedDate;
                workSheet.Cells[string.Format("M{0}", row)].Style.Numberformat.Format = dateformat;
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
    }
}