using BE.Core;
using BE.Data.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BE.Web.Controllers
{
    [CustomAuthentication]
    public class OutStandingController : Controller
    {
        #region Gobal Variable

        protected readonly CustomAuthentication _objAuthentication = new CustomAuthentication();
        protected readonly bl_OutStanding _blOutStanding = new bl_OutStanding();
        T_OutStanding model = new T_OutStanding();
        protected readonly bl_SalesOrder _blSalesOrder = new bl_SalesOrder();

        #endregion

        #region Constructor

        public OutStandingController()
        {
        }

        #endregion

        // GET: OutStanding
        public ActionResult Index()
        {
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            model.OSNumber = _blOutStanding.GetOrderNo();

            var objModel = new T_SalesOrder();
            var vlist = _blSalesOrder.GetList(objModel).Select(x => new SelectListItem { Text = x.OrderId, Value = x.Id.ToString() }).OrderByDescending(x => x.Text).ToList();
            ViewBag.SalesOrderList = vlist;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(T_OutStanding ObjOutStanding)
        {
            try
            {
                bool bAnyError = false;
                if (ModelState.IsValid)
                {
                    var vNameExists = _blOutStanding.GetFirstOrDefault(ObjOutStanding);
                    if (vNameExists != null)
                    {
                        ViewBag.ErrorMsg = "Order No alreay exists in our system";
                        bAnyError = true;
                    }
                    if (bAnyError == false)
                    {
                        T_OutStanding _Obj_T_OutStanding = new T_OutStanding()
                        {
                            Id = Guid.NewGuid(),
                            EventDate = Convert.ToDateTime(ObjOutStanding.EventDate),
                            SalesOrderId = ObjOutStanding.SalesOrderId,
                            OSNumber = ObjOutStanding.OSNumber,
                            Amount = ObjOutStanding.Amount,
                            Name = ObjOutStanding.Name,
                            Remark = ObjOutStanding.Remark,
                            CreatedBy = _objAuthentication.UserName,
                            CreatedDate = DateTime.Now
                        };
                        var vReturnObj = _blOutStanding.Create(_Obj_T_OutStanding);
                    }
                }
                else
                {
                    bAnyError = true;
                }

                if (bAnyError)
                {
                    return View(ObjOutStanding);
                }
                return RedirectToAction("Index", "OutStanding");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            var model = new T_OutStanding();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var vDetails = _blOutStanding.GetById(new Guid(Id));
                if (vDetails != null)
                {
                    model.EventDate = Convert.ToDateTime(vDetails.EventDate);
                    model.OSNumber = vDetails.OSNumber;
                    model.Name = vDetails.Name;
                    model.Amount = vDetails.Amount;
                    model.Remark = vDetails.Remark;
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(T_OutStanding ObjModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vObj = _blOutStanding.GetById(ObjModel.Id);
                    if (vObj != null)
                    {
                        vObj.EventDate = Convert.ToDateTime(ObjModel.EventDate);
                        vObj.Name = ObjModel.Name;
                        vObj.Amount = ObjModel.Amount;
                        vObj.Remark = ObjModel.Remark;

                        vObj.ModifyBy = _objAuthentication.UserName;
                        vObj.ModifyDate = DateTime.Now;
                        _blOutStanding.Update(vObj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "OutStanding");
        }

        [HttpPost]
        public ActionResult Delete(T_OutStanding ObjOutStanding)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjOutStanding.Id)))
                {
                    var vObj = _blOutStanding.GetById(ObjOutStanding.Id);
                    if (vObj != null)
                    {
                        _blOutStanding.Delete(ObjOutStanding);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjOutStanding);
        }

        [HttpPost]
        public ActionResult BulkDelete(List<T_OutStanding> DeletedRecord)
        {
            try
            {
                if (DeletedRecord.Count > 0)
                {
                    var vUser = _blOutStanding.BulkDelete(DeletedRecord);
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
        public JsonResult GetList(T_OutStanding ObjOutStanding)
        {
            List<T_OutStanding> ObjList = new List<T_OutStanding>();
            try
            {
                ObjList = _blOutStanding.GetList(ObjOutStanding);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjList, JsonRequestBehavior.AllowGet);
        }


        // ********************************** Sales Order Details ************************************* // 

        [HttpPost]
        public ActionResult CreateOutStandingDetails(T_OutStandingDetails ObjModel)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjModel.Id)))
                {
                    var vObjExists = _blOutStanding.GetByIdOutStandingDetail(ObjModel.Id);

                    if (vObjExists == null)
                    {
                        Guid guidId = Guid.NewGuid();
                        Guid guidOSId = Guid.Parse(Convert.ToString(ObjModel.OSId));

                        T_OutStandingDetails _Obj_T_OutStanding = new T_OutStandingDetails()
                        {
                            Id = guidId,
                            OSId = guidOSId,
                            EventDate = Convert.ToDateTime(ObjModel.EventDate),
                            SlNo = ObjModel.SlNo,
                            OutStandingAmount = ObjModel.OutStandingAmount,
                            PaidAmount = ObjModel.PaidAmount,
                            DueAmount = ObjModel.DueAmount,
                            Remark = ObjModel.Remark,
                            CreatedBy = _objAuthentication.UserName,
                            CreatedDate = DateTime.Now
                        };
                        _blOutStanding.CreateOutStandingDetail(_Obj_T_OutStanding);

                        return Json(new { data = ObjModel, Success = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        vObjExists.OutStandingAmount = ObjModel.OutStandingAmount;
                        vObjExists.PaidAmount = ObjModel.PaidAmount;
                        vObjExists.DueAmount = ObjModel.DueAmount;
                        vObjExists.Remark = ObjModel.Remark;

                        vObjExists.ModifyBy = _objAuthentication.UserName;
                        vObjExists.ModifyDate = DateTime.Now;
                        _blOutStanding.UpdateOutStandingDetail(vObjExists);

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
        public ActionResult DeleteOutStandingDetails(T_OutStandingDetails ObjModel)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjModel.Id)))
                {
                    var vObj = _blOutStanding.GetByIdOutStandingDetail(ObjModel.Id);
                    if (vObj != null)
                    {
                        _blOutStanding.DeleteOutStandingDetail(ObjModel);
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
        public JsonResult GetOutStandingDetailList(T_OutStanding ObjOutStanding)
        {
            var ObjList = new List<T_OutStandingDetails>();
            try
            {
                ObjList = _blOutStanding.GetOutStandingDetailList(ObjOutStanding);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetOutStandingOutStandingDetail(T_OutStanding ObjOutStanding)
        {
            var ObjList = new T_OutStandingDetails();
            try
            {
                if (_blOutStanding.GetOutStandingDetailList(ObjOutStanding).Count > 0)
                {
                    ObjList = _blOutStanding.GetOutStandingOutStandingDetail(ObjOutStanding);
                }
                else
                {
                    var t_OutStanding = _blOutStanding.GetById(ObjOutStanding.Id);
                    if (t_OutStanding != null)
                    {
                        ObjList.TotalOutStandingAmount = t_OutStanding.Amount;
                        ObjList.OutStandingAmount = t_OutStanding.Amount;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { ObjList, Message = "Sucess" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ExportToExcel(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return null;
            }
            T_OutStanding ObjItem = new T_OutStanding()
            {
                Id = Guid.Parse(Id)
            };
            var vObjList = _blOutStanding.GetOutStandingDetailList(ObjItem);

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
                //workSheet.Cells[string.Format("B{0}", row)].Value = item.ItemsName;
                //workSheet.Cells[string.Format("C{0}", row)].Value = item.BrandName;
                //workSheet.Cells[string.Format("D{0}", row)].Value = item.GuageName;
                //workSheet.Cells[string.Format("E{0}", row)].Value = item.StockQuantity;
                //workSheet.Cells[string.Format("F{0}", row)].Value = item.Quantity;
                //workSheet.Cells[string.Format("G{0}", row)].Value = item.UomName;
                //workSheet.Cells[string.Format("H{0}", row)].Value = item.LatestPrice;
                //workSheet.Cells[string.Format("I{0}", row)].Value = item.SellingPrice;

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
    }
}