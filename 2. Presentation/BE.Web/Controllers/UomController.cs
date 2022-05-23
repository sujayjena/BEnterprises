using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE.Core;
using BE.Data.UOM;

namespace BE.Web.Controllers
{
    [CustomAuthentication]
    public class UomController : Controller
    {
        protected readonly bl_Uom _blUom = new bl_Uom();
        protected readonly CustomAuthentication _objAuthentication = new CustomAuthentication();

        // GET: Uom
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
        public ActionResult Create(M_UOM ObjUOM)
        {
            try
            {
                bool bAnyError = false;
                if (ModelState.IsValid)
                {
                    var vNameExists = _blUom.GetFirstOrDefault(ObjUOM);
                    if (vNameExists != null)
                    {
                        ViewBag.ErrorMsg = "Name alreay exists in our system";
                        bAnyError = true;
                    }
                    if (bAnyError == false)
                    {
                        M_UOM _Obj_M_UOM = new M_UOM()
                        {
                            Id = Guid.NewGuid(),
                            Name = ObjUOM.Name,
                            CreatedBy = _objAuthentication.UserName,
                            CreatedDate = DateTime.Now
                        };
                        var vReturnObj = _blUom.Create(_Obj_M_UOM);
                    }
                }
                else
                {
                    bAnyError = true;
                }

                if (bAnyError)
                {
                    return View(ObjUOM);
                }
                return RedirectToAction("Index", "Uom");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            M_UOM vModel = new M_UOM();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var vDetails = _blUom.GetById(new Guid(Id));
                if (vDetails != null)
                {
                    vModel.Id = vDetails.Id;
                    vModel.Name = vDetails.Name;
                }
            }
            return View(vModel);
        }

        [HttpPost]
        public ActionResult Edit(M_UOM ObjUOM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjUOM.Id)))
                    {
                        var vObj = _blUom.GetById(ObjUOM.Id);
                        if (vObj != null)
                        {
                            vObj.Name = ObjUOM.Name;
                            vObj.ModifyDate = DateTime.Now;
                            vObj.ModifyBy = _objAuthentication.UserName;
                            _blUom.Update(vObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "UOM");
        }

        [HttpPost]
        public ActionResult Delete(M_UOM ObjUOM)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjUOM.Id)))
                {
                    var vObj = _blUom.GetById(ObjUOM.Id);
                    if (vObj != null)
                    {
                        _blUom.Delete(ObjUOM);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjUOM);
        }

        [HttpPost]
        public ActionResult BulkDelete(List<M_UOM> DeletedRecord)
        {
            try
            {
                if (DeletedRecord.Count > 0)
                {
                    var vUser = _blUom.BulkDelete(DeletedRecord);
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
        public JsonResult GetList(M_UOM ObjUOM)
        {
            List<M_UOM> ObjList = new List<M_UOM>();
            try
            {
                var vList = _blUom.GetList(ObjUOM);
                if (vList.Count > 0)
                {
                    foreach (var item in vList)
                    {
                        var vObjItemUser = new M_UOM()
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