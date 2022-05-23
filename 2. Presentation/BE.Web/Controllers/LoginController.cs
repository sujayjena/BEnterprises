using System;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using BE.Core;
using BE.Data.User;
using System.Net;

namespace BE.Web.Controllers
{
    public class LoginController : Controller
    {
        bl_Login _objLogin = new bl_Login();

        // GET: Login   
        public ActionResult Index()
        {
            HttpCookie authCookie = HttpContext.Request.Cookies["UserInfo"];
            if (authCookie != null)
            {
                return RedirectToAction("Index", "Home");
            }
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
                    var vUserObject = _objLogin.CheckUser(_ObjUser);
                    if (vUserObject != null)
                    {

                        UpdateAuthenticationDetail(vUserObject);

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

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Logout()
        {
            HttpCookie authCookie = HttpContext.Request.Cookies["UserInfo"];
            if (authCookie != null)
            {
                authCookie.Value = null;
                authCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Response.Cookies.Add(authCookie);
            }
            return RedirectToAction("Index", "Login");
        }

        [NonAction]
        public void UpdateAuthenticationDetail(M_User ObjUser)
        {
            bl_User _objUser = new bl_User();

            DateTime dCurrentLoginTime = DateTime.Now;
            string sCurrentLoginIP = string.Empty;

            // ********************************** // ***************************************
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            sCurrentLoginIP = Dns.GetHostByName(hostName).AddressList[0].ToString(); // Get the IP  

            #region Authentication 

            var vRoleName = _objLogin.GetUserRoleName(ObjUser);

            HttpCookie cookieUser = new HttpCookie("UserInfo");
            cookieUser.Values.Add("Id", Convert.ToString(ObjUser.Id));
            cookieUser.Values.Add("UserId", Convert.ToString(ObjUser.UserName));
            cookieUser.Values.Add("Name", Convert.ToString(ObjUser.Name));
            cookieUser.Values.Add("Email", Convert.ToString(ObjUser.Email));
            cookieUser.Values.Add("Phone", Convert.ToString(ObjUser.Phone));

            cookieUser.Values.Add("LastLoginTime", Convert.ToString(ObjUser.CurrentLoginTime.HasValue ? ObjUser.CurrentLoginTime.Value.ToString("dd/MM/yyyy") +",  "+  ObjUser.CurrentLoginTime.Value.ToString("hh:mm:ss tt") : "[N/A]"));
            cookieUser.Values.Add("LastLoginIP", Convert.ToString(ObjUser.LastLoginIP));
            cookieUser.Values.Add("CurrentLoginTime", Convert.ToString(dCurrentLoginTime != null ? dCurrentLoginTime.ToString("dd/MM/yyyy")+",  "+ dCurrentLoginTime.ToString("hh:mm:ss tt") : "[N/A]"));
            cookieUser.Values.Add("CurrentLoginIP", Convert.ToString(sCurrentLoginIP));
            cookieUser.Values.Add("RoleName", Convert.ToString(vRoleName));
            cookieUser.Expires = DateTime.Now.AddDays(10.0);

            Response.Cookies.Add(cookieUser);

            // Success, create non-persistent authentication cookie.
            //FormsAuthentication.SetAuthCookie(vUserObject.UserName.Trim(), false);
            //FormsAuthenticationTicket ticket1 =
            //   new FormsAuthenticationTicket(
            //        1,                          // version
            //        vUserObject.UserName.Trim(),    // get username  from the form
            //        DateTime.Now,               // issue time is now
            //        DateTime.Now.AddMinutes(10),// expires in 10 minutes
            //        false,                      // cookie is not persistent
            //        "HR"                   // role assignment is stored in userData
            //        );
            //HttpCookie cookieUser = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket1));
            //Response.Cookies.Add(cookieUser);

            #endregion

            #region Update Login Detail

            if (!string.IsNullOrWhiteSpace(Convert.ToString(ObjUser.Id)))
            {
                var vObj = _objUser.GetById(ObjUser.Id);
                if (vObj != null)
                {
                    vObj.LastLoginTime = ObjUser.CurrentLoginTime;
                    vObj.LastLoginIP = sCurrentLoginIP;

                    vObj.CurrentLoginTime = dCurrentLoginTime;
                    vObj.CurrentLoginIP = sCurrentLoginIP;

                    _objUser.Update(vObj);
                }
            }

            #endregion
        }
    }
}