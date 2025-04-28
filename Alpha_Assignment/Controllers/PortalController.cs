using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class PortalController : Controller
    {
        [Route("portal/signin")]
        public IActionResult PortalView()
        {
            return View();
            //return LocalRedirect("/projects");
        }
    }
}
