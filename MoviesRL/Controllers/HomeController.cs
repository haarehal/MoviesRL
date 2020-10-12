using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesRL.Controllers
{
    [AllowAnonymous] // sada mozemo pristupiti homepageu bez redirectanja na login stranicu (inace postoji restrikcija zbog globalnog filtera Authorize)
    public class HomeController : Controller
    {
        // [OutputCache(Duration = 50, Locations = OutputCacheLocation.Server, VaryByParam ="*")] // output kesiranje sa svrhom optimizacije performansi aplikacije na serverskoj strani - koristiti samo ako se podaci nece cesto mijenjati
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}