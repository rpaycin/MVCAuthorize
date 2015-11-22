using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace MVCAuthorize.Authorize
{
    //niteliğin class ve metotlarda kullanılacağını ekliyoruz. globalde de kullanabiliriz. filterconfige eklemen yeterli
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthAttribute : AuthorizeAttribute
    {
        const char _splitCharacter = ',';

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //hangi  controller ve actiondan geldi
            string controller = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
            string action = httpContext.Request.RequestContext.RouteData.Values["action"].ToString();

            //kullanıcı girişi var mı?
            IPrincipal user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
                return false;

            //kullanıcı bazında yetkisi var mı
            if (controller == "CustomHome" && action == "UserPage" && !Users.Split(_splitCharacter).Contains(httpContext.User.Identity.Name))
                return false;

            //role bazında yetkisi var mı
            if (controller == "CustomHome" && action == "RolePage" && !Roles.Split(_splitCharacter).Contains(httpContext.Session["RoleName"]))
                return false;

            return base.AuthorizeCore(httpContext);
        }
    }
}