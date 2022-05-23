using BE.Core;
using BE.Data.Supplier;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BE.Web.Controllers
{
    [CustomAuthentication]
    public class SupplierController : Controller
    {
        protected readonly bl_Supplier _blSupplier = new bl_Supplier();
        protected readonly CustomAuthentication _objAuthentication = new CustomAuthentication();

        // GET: Supplier
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
        public ActionResult Create(M_Supplier objSupplier)
        {
            try
            {
                bool bAnyError = false;
                if (ModelState.IsValid)
                {
                    var vNameExists = _blSupplier.GetFirstOrDefault(objSupplier);
                    if (vNameExists != null)
                    {
                        ViewBag.ErrorMsg = "Supplier Name alreay exists in our system";
                        bAnyError = true;
                    }
                    if (bAnyError == false)
                    {
                        M_Supplier _Obj_M_Supplier = new M_Supplier()
                        {
                            Id = Guid.NewGuid(),
                            Name = objSupplier.Name,
                            Phone = objSupplier.Phone,
                            Email = objSupplier.Email,
                            Address = objSupplier.Address,
                            CreatedBy = _objAuthentication.UserName,
                            CreatedDate = DateTime.Now
                        };
                        var vReturnObj = _blSupplier.Create(_Obj_M_Supplier);
                    }
                }
                else
                {
                    bAnyError = true;
                }

                if (bAnyError)
                {
                    return View(objSupplier);
                }
                return RedirectToAction("Index", "Supplier");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            M_Supplier vModel = new M_Supplier();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var vDetails = _blSupplier.GetById(new Guid(Id));
                if (vDetails != null)
                {
                    vModel.Id = vDetails.Id;
                    vModel.Name = vDetails.Name;
                    vModel.Phone = vDetails.Phone;
                    vModel.Email = vDetails.Email;
                    vModel.Address = vDetails.Address;
                }
            }
            return View(vModel);
        }

        [HttpPost]
        public ActionResult Edit(M_Supplier objSupplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(objSupplier.Id)))
                    {
                        var vObj = _blSupplier.GetById(objSupplier.Id);
                        if (vObj != null)
                        {
                            vObj.Name = objSupplier.Name;
                            vObj.Phone = objSupplier.Phone;
                            vObj.Email = objSupplier.Email;
                            vObj.Address = objSupplier.Address;
                            vObj.ModifyDate = DateTime.Now;
                            vObj.ModifyBy = _objAuthentication.UserName;
                            _blSupplier.Update(vObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "Supplier");
        }

        [HttpPost]
        public ActionResult Delete(M_Supplier objSupplier)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(objSupplier.Id)))
                {
                    var vObj = _blSupplier.GetById(objSupplier.Id);
                    if (vObj != null)
                    {
                        _blSupplier.Delete(objSupplier);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(objSupplier);
        }

        [HttpPost]
        public ActionResult BulkDelete(List<M_Supplier> DeletedRecord)
        {
            try
            {
                if (DeletedRecord.Count > 0)
                {
                    var vUser = _blSupplier.BulkDelete(DeletedRecord);
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
        public JsonResult GetList(M_Supplier objSupplier)
        {
            List<M_Supplier> ObjList = new List<M_Supplier>();
            try
            {
                var vList = _blSupplier.GetList(objSupplier);
                if (vList.Count > 0)
                {
                    foreach (var item in vList)
                    {
                        var vObjItemUser = new M_Supplier()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Phone = item.Phone,
                            Email = item.Email,
                            Address = item.Address,
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