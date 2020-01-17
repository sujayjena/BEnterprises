using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace BE.Web
{
    public class CustomAuthentication : ActionFilterAttribute, IAuthenticationFilter
    {
        #region Propperty

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string RoleName { get; set; }
        public string LastLoginTime { get; set; }
        public string LastLoginIP { get; set; }
        public string CurrentLoginTime { get; set; }
        public string CurrentLoginIP { get; set; }

        #endregion

        public CustomAuthentication()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies["UserInfo"];
            if (authCookie != null)
            {
                this.Name = Convert.ToString(authCookie["Name"]);
                this.Phone = Convert.ToString(authCookie["Phone"]);
                this.Email = Convert.ToString(authCookie["Email"]);
                this.UserName = Convert.ToString(authCookie["UserId"]);
                this.LastLoginTime = Convert.ToString(authCookie["LastLoginTime"]);
                this.LastLoginIP = Convert.ToString(authCookie["LastLoginIP"]);
                this.CurrentLoginTime = Convert.ToString(authCookie["CurrentLoginTime"]);
                this.CurrentLoginIP = Convert.ToString(authCookie["CurrentLoginIP"]);
                this.RoleName = authCookie["RoleName"];
            }
            else
            {
                this.Name = string.Empty;
                this.Phone = string.Empty;
                this.Email = string.Empty;
                this.UserName = string.Empty;
                this.LastLoginTime = string.Empty;
                this.LastLoginIP = string.Empty;
                this.CurrentLoginTime = string.Empty;
                this.CurrentLoginIP = string.Empty;
                this.RoleName = string.Empty;
            }
        }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            CustomAuthentication objCa = new CustomAuthentication();
            if (string.IsNullOrEmpty(objCa.UserName))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                //Redirecting the user to the Login View of Account Controller
                filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Login" },
                    { "action", "Index" }
               });

                //If you want to redirect to some error view, use below code
                //filterContext.Result = new ViewResult()
                //{
                //    ViewName = "Login"
                //};
            }
        }
    }
}