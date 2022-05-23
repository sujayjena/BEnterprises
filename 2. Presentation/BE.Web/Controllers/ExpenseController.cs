using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE.Core;
using BE.Data.Expense;
using System.Collections;


namespace BE.Web.Controllers
{
    [CustomAuthentication]
    public class ExpenseController : Controller
    {
        protected readonly bl_Expense _blExpense = new bl_Expense();
        protected readonly CustomAuthentication _objAuthentication = new CustomAuthentication();

        // GET: Expense
        public ActionResult Index()
        {
            //ExportToExcel();
            var vExpenseTypeList = _blExpense.GetExpenseTypeList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            ViewBag.ExpenseTypeList = vExpenseTypeList;
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var vExpenseTypeList = _blExpense.GetExpenseTypeList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            ViewBag.ExpenseTypeList = vExpenseTypeList;
            return View();
        }

        [HttpPost]
        public ActionResult Create(T_Expense ObjExpense)
        {
            try
            {
                bool bAnyError = false;
                if (ModelState.IsValid)
                {
                    T_Expense _Obj_T_Expense = new T_Expense()
                    {
                        Id = Guid.NewGuid(),
                        ExpenseTypeId = ObjExpense.ExpenseTypeId,
                        EventDate = Convert.ToDateTime(ObjExpense.EventDate),
                        ExpenseTypeName = ObjExpense.ExpenseTypeName,
                        Amount = ObjExpense.Amount,
                        Remark = ObjExpense.Remark,
                        CreatedBy = _objAuthentication.UserName,
                        CreatedDate = DateTime.Now
                    };
                    var vReturnObj = _blExpense.Create(_Obj_T_Expense);

                    bAnyError = false;
                }

                if (bAnyError)
                {
                    var vExpenseTypeList = _blExpense.GetExpenseTypeList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
                    ViewBag.ExpenseTypeList = vExpenseTypeList;
                    return View(ObjExpense);
                }
                return RedirectToAction("Index", "Expense");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            T_Expense vModel = new T_Expense();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var vDetails = _blExpense.GetById(new Guid(Id));
                if (vDetails != null)
                {
                    vModel.Id = vDetails.Id;
                    vModel.EventDate = vDetails.EventDate;
                    vModel.ExpenseTypeId = vDetails.ExpenseTypeId;
                    vModel.Amount = vDetails.Amount;
                    vModel.Remark = vDetails.Remark;
                }
            }
            var vExpenseTypeList = _blExpense.GetExpenseTypeList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            ViewBag.ExpenseTypeList = vExpenseTypeList;
            return View(vModel);
        }

        [HttpPost]
        public ActionResult Edit(T_Expense ObjExpense)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjExpense.Id)))
                    {
                        var vObj = _blExpense.GetById(ObjExpense.Id);
                        if (vObj != null)
                        {
                            vObj.EventDate = ObjExpense.EventDate;
                            vObj.ExpenseTypeId = ObjExpense.ExpenseTypeId;
                            vObj.Amount = ObjExpense.Amount;
                            vObj.Remark = ObjExpense.Remark;
                            vObj.ModifyDate = DateTime.Now;
                            vObj.ModifyBy = _objAuthentication.UserName;
                            _blExpense.Update(vObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "Expense");
        }

        [HttpPost]
        public ActionResult Delete(T_Expense ObjExpense)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjExpense.Id)))
                {
                    var vObj = _blExpense.GetById(ObjExpense.Id);
                    if (vObj != null)
                    {
                        _blExpense.Delete(ObjExpense);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjExpense);
        }

        [HttpPost]
        public ActionResult BulkDelete(List<T_Expense> DeletedRecord)
        {
            try
            {
                if (DeletedRecord.Count > 0)
                {
                    var vUser = _blExpense.BulkDelete(DeletedRecord);
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
        public ActionResult GetList(T_Expense ObjExpense)
        {
            List<T_Expense> ObjList = new List<T_Expense>();
            try
            {
                ObjList = _blExpense.GetList(ObjExpense);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ExportToExcel()
        {
            T_Expense ObjPurchaseOrder = new T_Expense();
            var vObjList = _blExpense.GetList(ObjPurchaseOrder);

            // ExcelPackageF dll Need
            OfficeOpenXml.ExcelPackage Ep = new OfficeOpenXml.ExcelPackage();
            OfficeOpenXml.ExcelWorksheet workSheet = Ep.Workbook.Worksheets.Add("Report");

            string sEndColumn = "E1";
            workSheet.Cells[string.Format("A1:{0}", sEndColumn)].Style.Font.Bold = true;
            workSheet.Cells[string.Format("A1:{0}", sEndColumn)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            workSheet.Cells[string.Format("A1:{0}", sEndColumn)].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSeaGreen);
            var allCells = workSheet.Cells[string.Format("A1:{0}", sEndColumn)];
            var cellFont = allCells.Style.Font;


            workSheet.Cells["A1"].Value = "Date";
            //sheet["A3:A4"].Merge();
            workSheet.Cells["B1"].Value = "Enpense Type";
            workSheet.Cells["C1"].Value = "Amount";
            workSheet.Cells["D1"].Value = "Remark";
            workSheet.Cells["E1"].Value = "Create Date";

            string dateformat = "dd-MM-yyyy";
            int row = 2;
            foreach (var item in vObjList)
            {
                workSheet.Cells[string.Format("A{0}", row)].Value = item.EventDate;
                workSheet.Cells[string.Format("A{0}", row)].Style.Numberformat.Format = dateformat;

                workSheet.Cells[string.Format("B{0}", row)].Value = item.ExpenseTypeName;
                workSheet.Cells[string.Format("C{0}", row)].Value = item.Amount;
                workSheet.Cells[string.Format("D{0}", row)].Value = item.Remark;
                workSheet.Cells[string.Format("E{0}", row)].Value = item.CreatedDate;
                workSheet.Cells[string.Format("E{0}", row)].Style.Numberformat.Format = dateformat;
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