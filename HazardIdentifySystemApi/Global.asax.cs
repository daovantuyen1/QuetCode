using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HazardIdentifySystemApi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_PostAuthorizeRequest()
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }


        //protected void Application_EndRequest(object sender, EventArgs e)
        //{
        //    // them doan code nay de fix loi response header : Set-cookie :samesite:Lax
        //    foreach (string cookieKey in Response.Cookies.AllKeys)
        //    {
        //        var cookie = Response.Cookies[cookieKey];
        //        if (cookie != null)
        //        {
        //            cookie.Secure = true; // Ensure HTTPS is used
        //            cookie.HttpOnly = true; // For security
        //            // Manually set the SameSite attribute
        //            string cookieHeader = $"{cookie.Name}={cookie.Value}; Path={cookie.Path}; HttpOnly; Secure; SameSite=None";
        //            Response.Headers.Add("Set-Cookie", cookieHeader);
        //        }
        //    }
        //}

    }
}
