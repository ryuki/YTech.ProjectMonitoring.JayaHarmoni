using JayaHarmoni.Web.Mvc.Controllers.ViewModels;
using System.Web.Mvc;

namespace JayaHarmoni.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new HomeViewModel());
        }
    }
}
