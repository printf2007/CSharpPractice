using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RouteDemo
{
    public class MyRoute:RouteBase
    {
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (httpContext.Request.UserAgent.Contains("Chrome/79.0.3945.130 Safari"))
            {
                RouteData routeData = new RouteData(this, new MvcRouteHandler());
                routeData.Values.Add("controller", "home");
                routeData.Values.Add("action", "refuse");
                return routeData;
            }
            else
                return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            Console.WriteLine("1234");
            return null;
        }


    }


    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.Add("refuse", new MyRoute());

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Regex",
            //    url: "{controller}/{action}-{year}-{month}-{day}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    constraints:new
            //    {
            //        year=@"\d{4}",
            //        month=@"\d{2}",
            //        day = @"\d{2}"
            //    }
            //);
        }
    }
}
