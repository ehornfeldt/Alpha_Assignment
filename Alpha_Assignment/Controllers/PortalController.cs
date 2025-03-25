using Microsoft.AspNetCore.Mvc;

namespace Alpha_Assignment.Controllers
{
    public class PortalController : Controller
    {
        public IActionResult PortalView()
        {
            return View();
        }
    }
}
