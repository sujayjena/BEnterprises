using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE.Core;
using BE.Data.Items;

namespace BE.Web.Controllers
{
    [CustomAuthentication]
    public class ItemsController : Controller
    {
        protected readonly bl_Items _blItems = new bl_Items();
        protected readonly CustomAuthentication _objAuthentication = new CustomAuthentication();

        // GET: Items
        public ActionResult Index()
        {
            var vItemsTypeList = _blItems.GetItemsTypeList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            ViewBag.ItemsTypeList = vItemsTypeList;
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var vItemsTypeList = _blItems.GetItemsTypeList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            ViewBag.ItemsTypeList = vItemsTypeList;
            return View();
        }

        [HttpPost]
        public ActionResult Create(M_Items ObjItems)
        {
            try
            {
                bool bAnyError = false;
                if (ModelState.IsValid)
                {
                    var vNameExists = _blItems.GetFirstOrDefault(ObjItems);
                    if (vNameExists != null)
                    {
                        ViewBag.ErrorMsg = "Name alreay exists in our system";
                        bAnyError = true;
                    }
                    if (bAnyError == false)
                    {
                        M_Items _Obj_M_Items = new M_Items()
                        {
                            Id = Guid.NewGuid(),
                            ItemsTypeId = ObjItems.ItemsTypeId,
                            Name = ObjItems.Name,
                            CreatedBy = _objAuthentication.UserName,
                            CreatedDate = DateTime.Now
                        };
                        var vReturnObj = _blItems.Create(_Obj_M_Items);
                    }
                }
                else
                {
                    bAnyError = true;
                }

                if (bAnyError)
                {
                    var vItemsTypeList = _blItems.GetItemsTypeList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
                    ViewBag.ItemsTypeList = vItemsTypeList;
                    return View(ObjItems);
                }
                return RedirectToAction("Index", "Items");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            M_Items vModel = new M_Items();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var vDetails = _blItems.GetById(new Guid(Id));
                if (vDetails != null)
                {
                    vModel.Id = vDetails.Id;
                    vModel.Name = vDetails.Name;
                    vModel.ItemsTypeId = vDetails.ItemsTypeId;
                }
            }
            var vItemsTypeList = _blItems.GetItemsTypeList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            ViewBag.ItemsTypeList = vItemsTypeList;

            return View(vModel);
        }

        [HttpPost]
        public ActionResult Edit(M_Items ObjItems)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjItems.Id)))
                    {
                        var vObj = _blItems.GetById(ObjItems.Id);
                        if (vObj != null)
                        {
                            vObj.ItemsTypeId = ObjItems.ItemsTypeId;
                            vObj.Name = ObjItems.Name;
                            vObj.ModifyDate = DateTime.Now;
                            vObj.ModifyBy = _objAuthentication.UserName;
                            _blItems.Update(vObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "Items");
        }

        [HttpPost]
        public ActionResult Delete(M_Items ObjItems)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjItems.Id)))
                {
                    var vObj = _blItems.GetById(ObjItems.Id);
                    if (vObj != null)
                    {
                        _blItems.Delete(ObjItems);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjItems);
        }

        [HttpPost]
        public ActionResult BulkDelete(List<M_Items> DeletedRecord)
        {
            try
            {
                if (DeletedRecord.Count > 0)
                {
                    var vUser = _blItems.BulkDelete(DeletedRecord);
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
        public JsonResult GetList(M_Items ObjItems)
        {
            List<M_Items> ObjList = new List<M_Items>();
            try
            {
                ObjList = _blItems.GetList(ObjItems).OrderBy(x=>x.ItemsTypeName).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjList, JsonRequestBehavior.AllowGet);
        }
    }
}