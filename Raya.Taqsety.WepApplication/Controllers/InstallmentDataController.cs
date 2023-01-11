using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Raya.Taqsety.WepApplication.Controllers
{
    public class InstallmentDataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        } 
        public IActionResult UpdateForm()
        {
            return View("updateDataViews/UpdateForm");
        }
        public IActionResult Summary()
        {
            return View("Summary");
        }
        public IActionResult CreaditTeamPortal()
        {
            return View();
        }
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

            return LocalRedirect(returnUrl);
        }
    }
}
