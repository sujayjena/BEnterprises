using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BE.Core;
using BE.Data.Company;

namespace BE.Web.Controllers
{
    [CustomAuthentication]
    public class CompanyController : Controller
    {
        protected readonly bl_Company _blCompany = new bl_Company();
        protected readonly CustomAuthentication _objAuthentication = new CustomAuthentication();

        // GET: Company
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
        public ActionResult Create(M_Company objCompany)
        {
            try
            {
                bool bAnyError = false;
                if (ModelState.IsValid)
                {
                    var vNameExists = _blCompany.GetFirstOrDefault(objCompany);
                    if (vNameExists != null)
                    {
                        ViewBag.ErrorMsg = "Company Name alreay exists in our system";
                        bAnyError = true;
                    }
                    if (bAnyError == false)
                    {
                        M_Company _Obj_M_Company = new M_Company()
                        {
                            Id = Guid.NewGuid(),
                            Name = objCompany.Name,
                            Phone = objCompany.Phone,
                            Email = objCompany.Email,
                            Address = objCompany.Address,
                            CreatedBy = _objAuthentication.UserName,
                            CreatedDate = DateTime.Now
                        };
                        var vReturnObj = _blCompany.Create(_Obj_M_Company);
                    }
                }
                else
                {
                    bAnyError = true;
                }

                if (bAnyError)
                {
                    return View(objCompany);
                }
                return RedirectToAction("Index", "Company");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            M_Company vModel = new M_Company();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var vDetails = _blCompany.GetById(new Guid(Id));
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
        public ActionResult Edit(M_Company objCompany)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(objCompany.Id)))
                    {
                        var vObj = _blCompany.GetById(objCompany.Id);
                        if (vObj != null)
                        {
                            vObj.Name = objCompany.Name;
                            vObj.Phone = objCompany.Phone;
                            vObj.Email = objCompany.Email;
                            vObj.Address = objCompany.Address;
                            vObj.ModifyDate = DateTime.Now;
                            vObj.ModifyBy = _objAuthentication.UserName;
                            _blCompany.Update(vObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "Company");
        }

        [HttpPost]
        public ActionResult Delete(M_Company objCompany)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(objCompany.Id)))
                {
                    var vObj = _blCompany.GetById(objCompany.Id);
                    if (vObj != null)
                    {
                        _blCompany.Delete(objCompany);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(objCompany);
        }

        [HttpPost]
        public ActionResult BulkDelete(List<M_Company> DeletedRecord)
        {
            try
            {
                if (DeletedRecord.Count > 0)
                {
                    var vUser = _blCompany.BulkDelete(DeletedRecord);
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
        public JsonResult GetList(M_Company objCompany)
        {
            List<M_Company> ObjList = new List<M_Company>();
            try
            {
                var vList = _blCompany.GetList(objCompany);
                if (vList.Count > 0)
                {
                    foreach (var item in vList)
                    {
                        var vObjItemUser = new M_Company()
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