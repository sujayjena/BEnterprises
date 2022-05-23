using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE.Core;
using BE.Data.Expense;

namespace BE.Web.Controllers
{
    [CustomAuthentication]
    public class ExpenseTypeController : Controller
    {
        protected readonly bl_ExpenseType _blExpenseType = new bl_ExpenseType();
        protected readonly CustomAuthentication _objAuthentication = new CustomAuthentication();

        // GET: Expense
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(M_ExpenseType ObjExpenseType)
        {
            try
            {
                bool bAnyError = false;
                if (ModelState.IsValid)
                {
                    var vNameExists = _blExpenseType.GetFirstOrDefault(ObjExpenseType);
                    if (vNameExists != null)
                    {
                        ViewBag.ErrorMsg = "Name alreay exists in our system";
                        bAnyError = true;
                    }
                    if (bAnyError == false)
                    {
                        M_ExpenseType _Obj_M_ExpenseType = new M_ExpenseType()
                        {
                            Id = Guid.NewGuid(),
                            Name = ObjExpenseType.Name,
                            Remark= ObjExpenseType.Remark,
                            CreatedBy = _objAuthentication.UserName,
                            CreatedDate = DateTime.Now
                        };
                        var vReturnObj = _blExpenseType.Create(_Obj_M_ExpenseType);
                    }
                }
                else
                {
                    bAnyError = true;
                }

                if (bAnyError)
                {
                    return View(ObjExpenseType);
                }
                return RedirectToAction("Index", "ExpenseType");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            M_ExpenseType vModel = new M_ExpenseType();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var vDetails = _blExpenseType.GetById(new Guid(Id));
                if (vDetails != null)
                {
                    vModel.Id = vDetails.Id;
                    vModel.Name = vDetails.Name;
                    vModel.Remark = vDetails.Remark;
                }
            }
            return View(vModel);
        }

        [HttpPost]
        public ActionResult Edit(M_ExpenseType ObjExpenseType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjExpenseType.Id)))
                    {
                        var vObj = _blExpenseType.GetById(ObjExpenseType.Id);
                        if (vObj != null)
                        {
                            vObj.Name = ObjExpenseType.Name;
                            vObj.Remark = ObjExpenseType.Remark;
                            vObj.ModifyDate = DateTime.Now;
                            vObj.ModifyBy = _objAuthentication.UserName;
                            _blExpenseType.Update(vObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "ExpenseType");
        }

        [HttpPost]
        public ActionResult Delete(M_ExpenseType ObjExpenseType)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjExpenseType.Id)))
                {
                    var vObj = _blExpenseType.GetById(ObjExpenseType.Id);
                    if (vObj != null)
                    {
                        _blExpenseType.Delete(ObjExpenseType);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjExpenseType);
        }

        [HttpPost]
        public ActionResult BulkDelete(List<M_ExpenseType> DeletedRecord)
        {
            try
            {
                if (DeletedRecord.Count > 0)
                {
                    var vUser = _blExpenseType.BulkDelete(DeletedRecord);
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
        public JsonResult GetList(M_ExpenseType ObjExpenseType)
        {
            List<M_ExpenseType> ObjList = new List<M_ExpenseType>();
            try
            {
                var vList = _blExpenseType.GetList(ObjExpenseType);
                if (vList.Count > 0)
                {
                    foreach (var item in vList)
                    {
                        var vObjItemUser = new M_ExpenseType()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Remark = item.Remark,
                            CreatedBy = item.CreatedBy,
                            CreatedDate = item.CreatedDate,
                            ModifyBy = item.ModifyBy,
                            ModifyDate = item.ModifyDate
                        };
                        ObjList.Add(vObjItemUser);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjList, JsonRequestBehavior.AllowGet);
        }
    }
}