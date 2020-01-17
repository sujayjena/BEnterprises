using BE.Core;
using BE.Data.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BE.Web.Controllers
{
    public class UserController : Controller
    {
        protected readonly bl_User _blUser = new bl_User();

        [HttpGet]
        [ActionName("Index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var vRoleList = _blUser.GetRoleList().Select(x => new SelectListItem { Text = x.RoleName, Value = x.Id });
            ViewBag.RoleList = vRoleList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(M_User objUser)
        {
            try
            {
                bool bAnyError = false;
                if (ModelState.IsValid)
                {
                    var vNameExists = _blUser.CheckByNameNEmail(objUser.UserName, objUser.Email);
                    if (vNameExists != null)
                    {
                        if (vNameExists.UserName == objUser.UserName)
                            ViewBag.ErrorMsg = "User Name alreay exists in our system";
                        else if (vNameExists.Email == objUser.Email)
                            ViewBag.ErrorMsg = "Email alreay exists in our system";

                        bAnyError = true;
                    }
                    if (bAnyError == false)
                    {
                        M_User _Obj_M_User = new M_User()
                        {
                            Id = Guid.NewGuid(),
                            Name = objUser.Name,
                            Phone = objUser.Phone,
                            Email = objUser.Email,
                            UserName = objUser.UserName,
                            UserPassword = objUser.UserPassword,
                            RoleId = objUser.RoleId,
                            CreatedBy = objUser.CreatedBy,
                            CreatedDate = DateTime.Now
                        };
                        var vReturnObj = _blUser.Create(_Obj_M_User);
                    }
                }
                else
                {
                    bAnyError = true;
                }

                if (bAnyError)
                {
                    var vRoleList = _blUser.GetRoleList().Select(x => new SelectListItem { Text = x.RoleName, Value = x.Id });
                    ViewBag.RoleList = vRoleList;

                    return View(objUser);
                }
                return RedirectToAction("Index", "User");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Update(M_User objUser)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(objUser.Id)))
                {
                    var vObj = _blUser.GetById(objUser.Id);
                    if (vObj != null)
                    {
                        vObj.Phone = objUser.Phone;
                        _blUser.Update(objUser);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(objUser);
        }

        [HttpPost]
        public ActionResult Delete(M_User objUser)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(objUser.Id)))
                {
                    var vObj = _blUser.GetById(objUser.Id);
                    if (vObj != null)
                    {
                        _blUser.Delete(objUser);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(objUser);
        }

        [HttpGet]
        public JsonResult GetList(M_User objUser)
        {
            List<M_User> ObjList = new List<M_User>();
            try
            {
                var vList = _blUser.GetList(objUser);
                if (vList.Count > 0)
                {
                    foreach (var item in vList)
                    {
                        var vObjItemUser = new M_User()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Phone = item.Phone,
                            Email = item.Email,
                            UserName = item.UserName,
                            UserPassword = item.UserPassword,
                            RoleId = item.RoleId,
                            LastLoginTime = item.LastLoginTime,
                            LastLoginIP = item.LastLoginIP,
                            CurrentLoginTime = item.CurrentLoginTime,
                            CurrentLoginIP = item.CurrentLoginIP,
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