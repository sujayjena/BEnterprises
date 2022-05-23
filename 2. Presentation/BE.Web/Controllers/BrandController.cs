using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE.Core;
using BE.Data.Brand;

namespace BE.Web.Controllers
{
    [CustomAuthentication]
    public class BrandController : Controller
    {
        protected readonly bl_Brand _blCompany = new bl_Brand();
        protected readonly CustomAuthentication _objAuthentication = new CustomAuthentication();

        // GET: Brand
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
        public ActionResult Create(M_Brand ObjBrand)
        {
            try
            {
                bool bAnyError = false;
                if (ModelState.IsValid)
                {
                    var vNameExists = _blCompany.GetFirstOrDefault(ObjBrand);
                    if (vNameExists != null)
                    {
                        ViewBag.ErrorMsg = "Name alreay exists in our system";
                        bAnyError = true;
                    }
                    if (bAnyError == false)
                    {
                        M_Brand _Obj_M_Brand = new M_Brand()
                        {
                            Id = Guid.NewGuid(),
                            Name = ObjBrand.Name,
                            CreatedBy = _objAuthentication.UserName,
                            CreatedDate = DateTime.Now
                        };
                        var vReturnObj = _blCompany.Create(_Obj_M_Brand);
                    }
                }
                else
                {
                    bAnyError = true;
                }

                if (bAnyError)
                {
                    return View(ObjBrand);
                }
                return RedirectToAction("Index", "Brand");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            M_Brand vModel = new M_Brand();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var vDetails = _blCompany.GetById(new Guid(Id));
                if (vDetails != null)
                {
                    vModel.Id = vDetails.Id;
                    vModel.Name = vDetails.Name;
                }
            }
            return View(vModel);
        }

        [HttpPost]
        public ActionResult Edit(M_Brand ObjBrand)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjBrand.Id)))
                    {
                        var vObj = _blCompany.GetById(ObjBrand.Id);
                        if (vObj != null)
                        {
                            vObj.Name = ObjBrand.Name;
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
            return RedirectToAction("Index", "Brand");
        }

        [HttpPost]
        public ActionResult Delete(M_Brand ObjBrand)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjBrand.Id)))
                {
                    var vObj = _blCompany.GetById(ObjBrand.Id);
                    if (vObj != null)
                    {
                        _blCompany.Delete(ObjBrand);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjBrand);
        }

        [HttpPost]
        public ActionResult BulkDelete(List<M_Brand> DeletedRecord)
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
        public JsonResult GetList(M_Brand ObjBrand)
        {
            List<M_Brand> ObjList = new List<M_Brand>();
            try
            {
                var vList = _blCompany.GetList(ObjBrand);
                if (vList.Count > 0)
                {
                    foreach (var item in vList)
                    {
                        var vObjItemUser = new M_Brand()
                        {
                            Id = item.Id,
                            Name = item.Name,
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