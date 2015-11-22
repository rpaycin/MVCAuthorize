using MVCAuthorize.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

/// <summary>
/// authorize niteliği 3 şekilde kullanılabilir. 1. Action 2. Controller 3. Global
/// 1 ile 2 de direk authorization metodunu eklemek yeterli
/// global çağlı olması için global.asaxa FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters); ekliyoruz
/// app starta FilterConfig dosyası oluşturuyoruz. bu örnekte global çağlı authorize var
/// </summary>
namespace MVCAuthorize.Controllers
{

    /// <summary>
    /// Authorize niteliği classa eklenirse controller altındaki tüm actionlara yetkilendirme kontrolü yapılır
    /// </summary>
    //[Authorize]
    public class HomeController : Controller
    {

        /// <summary>
        /// Metot bazında yetki kontrolü yapılıyor
        /// [Authorize] niteliğini eklemen yeter. web.confige birşey eklemene gerek yok. 401 hatası gelir
        /// web.confige  <authentication mode="Forms"> <forms loginUrl = "~/Home/Login" timeout="2880" /></authentication> eklenirse yetkisiz girişte direk logine yönlendirme yapılır
        /// </summary>
        /// <returns></returns> 
        //[Authorize]
        public ActionResult Index()
        {
            var isAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            return View();
        }

        public ActionResult Exit()
        {
            //
            FormsAuthentication.SignOut();

            return RedirectToAction("Index");//index sayfasına yönelemez. çünkü signout oldu
        }

        //Bu sayfaya sadece user1 ve user2 kullanıcıları girebilir
        [Authorize(Users = "user1,user2")]
        public ActionResult UserPage()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult AdminPage()
        {
            return View();
        }


        //AllowAnonymous niteliği yetkisiz girişe izin vermektedir
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginModel login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (true)
                {
                    //tarayıcı cookilere kapalı ise çalışmayabilir
                    FormsAuthentication.SetAuthCookie(login.UserName, false);
                    Session["RoleName"] = "admin";

                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                }
                else
                    ModelState.AddModelError("", "Kullanıcı bulunamadı!");
            }
            return View();
        }
    }
}