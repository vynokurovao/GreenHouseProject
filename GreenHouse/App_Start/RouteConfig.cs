using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GreenHouse
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "RegistrationCreate",
                url: "Registration/Create",
                defaults: new { controller = "Registration", action = "Create" }
                );

            routes.MapRoute(
                name: "RegistrationAdd",
                url: "Registration/Add",
                defaults: new { controller = "Registration", action = "Add" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
