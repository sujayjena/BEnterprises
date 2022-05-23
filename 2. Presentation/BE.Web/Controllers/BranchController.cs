using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BE.Core;
using BE.Data.Branch;

namespace BE.Web.Controllers
{
    [CustomAuthentication]
    public class BranchController : Controller
    {
        protected readonly bl_Branch _blBranch = new bl_Branch();
        protected readonly CustomAuthentication _objAuthentication = new CustomAuthentication();

        // GET: Branch
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var vRoleList = _blBranch.GetCompanyList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            ViewBag.RoleList = vRoleList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(M_Branch objBranch)
        {
            try
            {
                bool bAnyError = false;
                if (ModelState.IsValid)
                {
                    var vNameExists = _blBranch.GetFirstOrDefault(objBranch);
                    if (vNameExists != null)
                    {
                        ViewBag.ErrorMsg = "Name alreay exists in our system";
                        bAnyError = true;
                    }
                    if (bAnyError == false)
                    {
                        M_Branch _Obj_M_Branch = new M_Branch()
                        {
                            Id = Guid.NewGuid(),
                            CompanyId = objBranch.CompanyId,
                            Name = objBranch.Name,
                            Phone = objBranch.Phone,
                            Email = objBranch.Email,
                            Address = objBranch.Address,
                            CreatedBy = _objAuthentication.UserName,
                            CreatedDate = DateTime.Now
                        };
                        var vReturnObj = _blBranch.Create(_Obj_M_Branch);
                    }
                }
                else
                {
                    bAnyError = true;
                }

                if (bAnyError)
                {
                    return View(objBranch);
                }
                return RedirectToAction("Index", "Branch");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            M_Branch vModel = new M_Branch();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var vDetails = _blBranch.GetById(new Guid(Id));
                if (vDetails != null)
                {
                    vModel.Id = vDetails.Id;
                    vModel.Name = vDetails.Name;
                    vModel.Phone = vDetails.Phone;
                    vModel.Email = vDetails.Email;
                    vModel.Address = vDetails.Address;
                    vModel.CompanyId = vDetails.CompanyId;
                }
            }
            var vRoleList = _blBranch.GetCompanyList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            ViewBag.RoleList = vRoleList;

            return View(vModel);
        }

        [HttpPost]
        public ActionResult Edit(M_Branch objBranch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(objBranch.Id)))
                    {
                        var vObj = _blBranch.GetById(objBranch.Id);
                        if (vObj != null)
                        {
                            vObj.Name = objBranch.Name;
                            vObj.Phone = objBranch.Phone;
                            vObj.Email = objBranch.Email;
                            vObj.Address = objBranch.Address;
                            vObj.CompanyId = objBranch.CompanyId;
                            vObj.ModifyDate = DateTime.Now;
                            vObj.ModifyBy = _objAuthentication.UserName;
                            _blBranch.Update(vObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "Branch");
        }

        [HttpPost]
        public ActionResult Delete(M_Branch objBranch)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(objBranch.Id)))
                {
                    var vObj = _blBranch.GetById(objBranch.Id);
                    if (vObj != null)
                    {
                        _blBranch.Delete(objBranch);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(objBranch);
        }

        [HttpPost]
        public ActionResult BulkDelete(List<M_Branch> DeletedRecord)
        {
            try
            {
                if (DeletedRecord.Count > 0)
                {
                    var vUser = _blBranch.BulkDelete(DeletedRecord);
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
        public JsonResult GetList(M_Branch objBranch)
        {
            List<M_Branch> ObjList = new List<M_Branch>();
            try
            {
                ObjList = _blBranch.GetList(objBranch);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjList, JsonRequestBehavior.AllowGet);
        }
    }
}