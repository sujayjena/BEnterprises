using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE.Core;
using BE.Data.Guage;

namespace BE.Web.Controllers
{
    [CustomAuthentication]
    public class GuageController : Controller
    {
        protected readonly bl_Guage _blGuage = new bl_Guage();
        protected readonly CustomAuthentication _objAuthentication = new CustomAuthentication();

        // GET: Guage
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
        public ActionResult Create(M_Guage ObjGuage)
        {
            try
            {
                bool bAnyError = false;
                if (ModelState.IsValid)
                {
                    var vNameExists = _blGuage.GetFirstOrDefault(ObjGuage);
                    if (vNameExists != null)
                    {
                        ViewBag.ErrorMsg = "Name alreay exists in our system";
                        bAnyError = true;
                    }
                    if (bAnyError == false)
                    {
                        M_Guage _Obj_M_Guage = new M_Guage()
                        {
                            Id = Guid.NewGuid(),
                            Name = ObjGuage.Name,
                            CreatedBy = _objAuthentication.UserName,
                            CreatedDate = DateTime.Now
                        };
                        var vReturnObj = _blGuage.Create(_Obj_M_Guage);
                    }
                }
                else
                {
                    bAnyError = true;
                }

                if (bAnyError)
                {
                    return View(ObjGuage);
                }
                return RedirectToAction("Index", "Guage");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            M_Guage vModel = new M_Guage();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var vDetails = _blGuage.GetById(new Guid(Id));
                if (vDetails != null)
                {
                    vModel.Id = vDetails.Id;
                    vModel.Name = vDetails.Name;
                }
            }
            return View(vModel);
        }

        [HttpPost]
        public ActionResult Edit(M_Guage ObjGuage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjGuage.Id)))
                    {
                        var vObj = _blGuage.GetById(ObjGuage.Id);
                        if (vObj != null)
                        {
                            vObj.Name = ObjGuage.Name;
                            vObj.ModifyDate = DateTime.Now;
                            vObj.ModifyBy = _objAuthentication.UserName;
                            _blGuage.Update(vObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "Guage");
        }

        [HttpPost]
        public ActionResult Delete(M_Guage ObjGuage)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjGuage.Id)))
                {
                    var vObj = _blGuage.GetById(ObjGuage.Id);
                    if (vObj != null)
                    {
                        _blGuage.Delete(ObjGuage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjGuage);
        }

        [HttpPost]
        public ActionResult BulkDelete(List<M_Guage> DeletedRecord)
        {
            try
            {
                if (DeletedRecord.Count > 0)
                {
                    var vUser = _blGuage.BulkDelete(DeletedRecord);
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
        public JsonResult GetList(M_Guage ObjGuage)
        {
            List<M_Guage> ObjList = new List<M_Guage>();
            try
            {
                var vList = _blGuage.GetList(ObjGuage);
                if (vList.Count > 0)
                {
                    foreach (var item in vList)
                    {
                        var vObjItemUser = new M_Guage()
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