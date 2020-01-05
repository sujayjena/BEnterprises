using BE.Core;
using BE.Data.User;
using System;
using System.Web.Mvc;

namespace BE.Web.Controllers
{
    public class UserController : Controller
    {
        protected readonly bl_User _blUser = new bl_User();

        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(M_User objUser)
        {
            try
            {
                if (ModelState.IsValid)
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
                    var vReturnObj = _blUser.Save(_Obj_M_User);
                }
                return RedirectToAction("Index", "User");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}