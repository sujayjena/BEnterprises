using BE.Core;
using BE.Data.Login;
using System.Web.Mvc;

namespace BE.Web.Controllers
{
    public class LoginController : Controller
    {
        bl_Login _objLogin = new bl_Login();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckUser(M_User _ObjUser)
        {
            if (!string.IsNullOrWhiteSpace(_ObjUser.UserName) && !string.IsNullOrWhiteSpace(_ObjUser.UserPassword))
            {
                var vObject = _objLogin.CheckUser(_ObjUser);
                if (vObject != null)
                    return Json(new { Result = true, Message = "Login Sucess", RedirectTo = Url.Action("Index", "Home") }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { Result = false, Message = "Login Failed, Please enter valid user name and password!" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Result = false, Message = "Login Failed, Please enter valid user name and password!" }, JsonRequestBehavior.AllowGet);
        }
    }
}