using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class AlphaController : Controller
    {
        public IActionResult AlphaView()
        {
            return View();
        }
    }
}
