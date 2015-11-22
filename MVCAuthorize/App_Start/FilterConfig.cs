using MVCAuthorize.Authorize;
using System.Web.Mvc;

namespace MVCAuthorize.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new AuthorizeAttribute());//genel authorize. mvc nin kendi yapısı kullanılmaktadır

            filters.Add(new CustomAuthorizationFilter());//custom filter

            //filters.Add(new CustomAuthAttribute());//custom authorize attribute
        }
    }
}