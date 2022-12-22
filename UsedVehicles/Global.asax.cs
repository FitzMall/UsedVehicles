using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace UsedVehicles
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RouteTable.Routes.MapRoute(
                "UsedInventory",
                "Home/Inventory/{location}/{minDays}/{maxDays}/{filter}", // URL with parameters
                new { controller = "Home", action = "Inventory", location = UrlParameter.Optional, minDays = UrlParameter.Optional, maxDays = UrlParameter.Optional, filter = UrlParameter.Optional }
            );

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            SetUserInformation();
        }

        public void SetUserInformation()
        {

            if (Request.Cookies["User"] != null)
            {
                var cookieValue = Request.Cookies["User"].Value;

                NameValueCollection qsCollection = HttpUtility.ParseQueryString(cookieValue);

                var userIdFromCookie = qsCollection["login"].ToString();
                var userNameFromCookie = qsCollection["name"].ToString();

                Session.Add("UserId", userIdFromCookie);
                Session.Add("UserName", userNameFromCookie);
            }
            else
            {
                Session.Add("UserId", "statlerc");
                Session.Add("UserName", "Default User");

            }

        }

    }
}
