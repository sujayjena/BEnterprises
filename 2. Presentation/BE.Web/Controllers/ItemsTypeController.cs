using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE.Core;
using BE.Data.ItemsType;

namespace BE.Web.Controllers
{
    [CustomAuthentication]
    public class ItemsTypeController : Controller
    {
        protected readonly bl_ItemsType _blItemsType = new bl_ItemsType();
        protected readonly CustomAuthentication _objAuthentication = new CustomAuthentication();

        // GET: ItemsType
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
        public ActionResult Create(M_ItemsType ObjItemsType)
        {
            try
            {
                bool bAnyError = false;
                if (ModelState.IsValid)
                {
                    var vNameExists = _blItemsType.GetFirstOrDefault(ObjItemsType);
                    if (vNameExists != null)
                    {
                        ViewBag.ErrorMsg = "Name alreay exists in our system";
                        bAnyError = true;
                    }
                    if (bAnyError == false)
                    {
                        M_ItemsType _Obj_M_ItemsType = new M_ItemsType()
                        {
                            Id = Guid.NewGuid(),
                            Name = ObjItemsType.Name,
                            CreatedBy = _objAuthentication.UserName,
                            CreatedDate = DateTime.Now
                        };
                        var vReturnObj = _blItemsType.Create(_Obj_M_ItemsType);
                    }
                }
                else
                {
                    bAnyError = true;
                }

                if (bAnyError)
                {
                    return View(ObjItemsType);
                }
                return RedirectToAction("Index", "ItemsType");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            M_ItemsType vModel = new M_ItemsType();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var vDetails = _blItemsType.GetById(new Guid(Id));
                if (vDetails != null)
                {
                    vModel.Id = vDetails.Id;
                    vModel.Name = vDetails.Name;
                }
            }
            return View(vModel);
        }

        [HttpPost]
        public ActionResult Edit(M_ItemsType ObjItemsType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjItemsType.Id)))
                    {
                        var vObj = _blItemsType.GetById(ObjItemsType.Id);
                        if (vObj != null)
                        {
                            vObj.Name = ObjItemsType.Name;
                            vObj.ModifyDate = DateTime.Now;
                            vObj.ModifyBy = _objAuthentication.UserName;
                            _blItemsType.Update(vObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "ItemsType");
        }

        [HttpPost]
        public ActionResult Delete(M_ItemsType ObjItemsType)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjItemsType.Id)))
                {
                    var vObj = _blItemsType.GetById(ObjItemsType.Id);
                    if (vObj != null)
                    {
                        _blItemsType.Delete(ObjItemsType);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjItemsType);
        }

        [HttpPost]
        public ActionResult BulkDelete(List<M_ItemsType> DeletedRecord)
        {
            try
            {
                if (DeletedRecord.Count > 0)
                {
                    var vUser = _blItemsType.BulkDelete(DeletedRecord);
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
        public JsonResult GetList(M_ItemsType ObjItemsType)
        {
            List<M_ItemsType> ObjList = new List<M_ItemsType>();
            try
            {
                var vList = _blItemsType.GetList(ObjItemsType);
                if (vList.Count > 0)
                {
                    foreach (var item in vList)
                    {
                        var vObjItemUser = new M_ItemsType()
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