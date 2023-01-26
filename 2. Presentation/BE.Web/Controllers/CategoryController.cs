using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE.Core;
using BE.Data.Category;

namespace BE.Web.Controllers
{
    [CustomAuthentication]
    public class CategoryController : Controller
    {
        protected readonly bl_Category _blCategory = new bl_Category();
        protected readonly CustomAuthentication _objAuthentication = new CustomAuthentication();

        // GET: Category
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
        public ActionResult Create(M_Category ObjCategory)
        {
            try
            {
                bool bAnyError = false;
                if (ModelState.IsValid)
                {
                    var vNameExists = _blCategory.GetFirstOrDefault(ObjCategory);
                    if (vNameExists != null)
                    {
                        ViewBag.ErrorMsg = "Name alreay exists in our system";
                        bAnyError = true;
                    }
                    if (bAnyError == false)
                    {
                        M_Category _Obj_M_Category = new M_Category()
                        {
                            Id = Guid.NewGuid(),
                            CompanyId = ObjCategory.CompanyId,
                            Name = ObjCategory.Name,
                            CreatedBy = _objAuthentication.UserName,
                            CreatedDate = DateTime.Now
                        };
                        var vReturnObj = _blCategory.Create(_Obj_M_Category);
                    }
                }
                else
                {
                    bAnyError = true;
                }

                if (bAnyError)
                {
                    return View(ObjCategory);
                }
                return RedirectToAction("Index", "Category");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            M_Category vModel = new M_Category();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var vDetails = _blCategory.GetById(new Guid(Id));
                if (vDetails != null)
                {
                    vModel.Id = vDetails.Id;
                    vModel.Name = vDetails.Name;
                }
            }
            return View(vModel);
        }

        [HttpPost]
        public ActionResult Edit(M_Category ObjCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjCategory.Id)))
                    {
                        var vObj = _blCategory.GetById(ObjCategory.Id);
                        if (vObj != null)
                        {
                            vObj.Name = ObjCategory.Name;
                            vObj.ModifyDate = DateTime.Now;
                            vObj.ModifyBy = _objAuthentication.UserName;
                            _blCategory.Update(vObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "Category");
        }

        [HttpPost]
        public ActionResult Delete(M_Category ObjCategory)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjCategory.Id)))
                {
                    var vObj = _blCategory.GetById(ObjCategory.Id);
                    if (vObj != null)
                    {
                        _blCategory.Delete(ObjCategory);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjCategory);
        }

        [HttpPost]
        public ActionResult BulkDelete(List<M_Category> DeletedRecord)
        {
            try
            {
                if (DeletedRecord.Count > 0)
                {
                    var vUser = _blCategory.BulkDelete(DeletedRecord);
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
        public JsonResult GetList(M_Category ObjCategory)
        {
            List<M_Category> ObjList = new List<M_Category>();
            try
            {
                var vList = _blCategory.GetList(ObjCategory);
                if (vList.Count > 0)
                {
                    foreach (var item in vList)
                    {
                        var vObjItemUser = new M_Category()
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