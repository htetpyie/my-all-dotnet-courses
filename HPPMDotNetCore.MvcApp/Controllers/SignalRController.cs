using Microsoft.AspNetCore.Mvc;

namespace HPPMDotNetCore.MvcApp.Controllers
{
    public class SignalRController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
