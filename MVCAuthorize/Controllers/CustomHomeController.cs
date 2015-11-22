using MVCAuthorize.Authorize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCAuthorize.Controllers
{
    public class CustomHomeController : Controller
    {
        [CustomAuth(Users = "test1")]
        public ActionResult UserPage()
        {
            return View();
        }

        [CustomAuth(Roles = "admin,sales")]
        public ActionResult RolePage()
        {
            return View();
        }
    }
}