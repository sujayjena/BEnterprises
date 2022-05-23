using BE.Core;
using BE.Data.Order;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BE.Web.Controllers
{
    [CustomAuthentication]
    public class LoanController : Controller
    {
        #region Gobal Variable

        protected readonly CustomAuthentication _objAuthentication = new CustomAuthentication();
        protected readonly bl_Loan _blLoan = new bl_Loan();
        T_Loan model = new T_Loan();

        #endregion

        #region Constructor

        public LoanController()
        {
        }

        #endregion

        // GET: Loan
        public ActionResult Index()
        {
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            model.LoanNumber = _blLoan.GetOrderNo();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(T_Loan ObjLoan)
        {
            try
            {
                bool bAnyError = false;
                if (ModelState.IsValid)
                {
                    var vNameExists = _blLoan.GetFirstOrDefault(ObjLoan);
                    if (vNameExists != null)
                    {
                        ViewBag.ErrorMsg = "Order No alreay exists in our system";
                        bAnyError = true;
                    }
                    if (bAnyError == false)
                    {
                        T_Loan _Obj_T_Loan = new T_Loan()
                        {
                            Id = Guid.NewGuid(),
                            EventDate = Convert.ToDateTime(ObjLoan.EventDate),
                            LoanNumber = ObjLoan.LoanNumber,
                            Amount = ObjLoan.Amount,
                            Name = ObjLoan.Name,
                            Remark = ObjLoan.Remark,
                            CreatedBy = _objAuthentication.UserName,
                            CreatedDate = DateTime.Now
                        };
                        var vReturnObj = _blLoan.Create(_Obj_T_Loan);
                    }
                }
                else
                {
                    bAnyError = true;
                }

                if (bAnyError)
                {
                    return View(ObjLoan);
                }
                return RedirectToAction("Index", "Loan");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            var model = new T_Loan();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var vDetails = _blLoan.GetById(new Guid(Id));
                if (vDetails != null)
                {
                    model.EventDate = Convert.ToDateTime(vDetails.EventDate);
                    model.LoanNumber = vDetails.LoanNumber;
                    model.Name = vDetails.Name;
                    model.Amount = vDetails.Amount;
                    model.Remark = vDetails.Remark;
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(T_Loan ObjModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vObj = _blLoan.GetById(ObjModel.Id);
                    if (vObj != null)
                    {
                        vObj.EventDate = Convert.ToDateTime(ObjModel.EventDate);
                        vObj.Name = ObjModel.Name;
                        vObj.Amount = ObjModel.Amount;
                        vObj.Remark = ObjModel.Remark;

                        vObj.ModifyBy = _objAuthentication.UserName;
                        vObj.ModifyDate = DateTime.Now;
                        _blLoan.Update(vObj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "Loan");
        }

        [HttpPost]
        public ActionResult Delete(T_Loan ObjLoan)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjLoan.Id)))
                {
                    var vObj = _blLoan.GetById(ObjLoan.Id);
                    if (vObj != null)
                    {
                        _blLoan.Delete(ObjLoan);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjLoan);
        }

        [HttpPost]
        public ActionResult BulkDelete(List<T_Loan> DeletedRecord)
        {
            try
            {
                if (DeletedRecord.Count > 0)
                {
                    var vUser = _blLoan.BulkDelete(DeletedRecord);
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
        public JsonResult GetList(T_Loan ObjLoan)
        {
            List<T_Loan> ObjList = new List<T_Loan>();
            try
            {
                ObjList = _blLoan.GetList(ObjLoan);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjList, JsonRequestBehavior.AllowGet);
        }


        // ********************************** Sales Order Details ************************************* // 

        [HttpPost]
        public ActionResult CreateLoanDetails(T_LoanDetails ObjModel)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjModel.Id)))
                {
                    var vObjExists = _blLoan.GetByIdLoanDetail(ObjModel.Id);

                    if (vObjExists == null)
                    {
                        Guid guidId = Guid.NewGuid();
                        Guid guidLoanId = Guid.Parse(Convert.ToString(ObjModel.LoanId));

                        T_LoanDetails _Obj_T_Loan = new T_LoanDetails()
                        {
                            Id = guidId,
                            LoanId = guidLoanId,
                            EventDate = Convert.ToDateTime(ObjModel.EventDate),
                            SlNo = ObjModel.SlNo,
                            OutStandingAmount = ObjModel.OutStandingAmount,
                            PaidAmount = ObjModel.PaidAmount,
                            DueAmount = ObjModel.DueAmount,
                            Remark = ObjModel.Remark,
                            CreatedBy = _objAuthentication.UserName,
                            CreatedDate = DateTime.Now
                        };
                        _blLoan.CreateLoanDetail(_Obj_T_Loan);

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
                        _blLoan.UpdateLoanDetail(vObjExists);

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
        public ActionResult DeleteLoanDetails(T_LoanDetails ObjModel)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjModel.Id)))
                {
                    var vObj = _blLoan.GetByIdLoanDetail(ObjModel.Id);
                    if (vObj != null)
                    {
                        _blLoan.DeleteLoanDetail(ObjModel);
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
        public JsonResult GetLoanDetailList(T_Loan ObjLoan)
        {
            var ObjList = new List<T_LoanDetails>();
            try
            {
                ObjList = _blLoan.GetLoanDetailList(ObjLoan);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetLoanOutStandingDetail(T_Loan ObjLoan)
        {
            var ObjList = new T_LoanDetails();
            try
            {
                if (_blLoan.GetLoanDetailList(ObjLoan).Count > 0)
                {
                    ObjList = _blLoan.GetLoanOutStandingDetail(ObjLoan);
                }
                else
                {
                    var t_Loan = _blLoan.GetById(ObjLoan.Id);
                    if (t_Loan != null)
                    {
                        ObjList.TotalLoanAmount = t_Loan.Amount;
                        ObjList.OutStandingAmount = t_Loan.Amount;
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
            T_Loan ObjItem = new T_Loan()
            {
                Id = Guid.Parse(Id)
            };
            var vObjList = _blLoan.GetLoanDetailList(ObjItem);

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