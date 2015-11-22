using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCAuthorize.Authorize
{
    //nitelik olarak ekleyemezsn
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "action","Login"},
                        { "controller","Home"}
                    }
                    );
                //filterContext.Result = new HttpUnauthorizedResult();
            }
            else
            {
                ViewDataDictionary viewData = new ViewDataDictionary();
                viewData.Add("Message", "You do not have sufficient privileges for this operation.");
                filterContext.Result = new ViewResult
                {
                    ViewData = viewData
                };
            }
            //else if (filterContext.HttpContext.User.IsInRole("admin"))
            //{
            //    filterContext.Result = new HttpUnauthorizedResult();
            //}
        }
    }
}