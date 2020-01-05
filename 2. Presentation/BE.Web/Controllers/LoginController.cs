using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using BE.Core;
using BE.Data.User;

namespace BE.Web.Controllers
{
    public class LoginController : Controller
    {
        bl_Login _objLogin = new bl_Login();

        // GET: Login   
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(M_User _ObjUser)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(_ObjUser.UserName) && !string.IsNullOrWhiteSpace(_ObjUser.UserPassword))
                {
                    var vObject = _objLogin.CheckUser(_ObjUser);
                    if (vObject != null)
                    {
                        FormsAuthentication.SetAuthCookie(vObject.UserName, true);
                        return Json(new { Result = true, Message = "Login Sucess", RedirectTo = Url.Action("Index", "Home") }, JsonRequestBehavior.AllowGet);
                    }
                    else
                        return Json(new { Result = false, Message = "Login Failed, Please enter valid user name and password!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { Result = false, Message = "Login Failed, Please enter valid user name and password!" }, JsonRequestBehavior.AllowGet);
        }
    }
}