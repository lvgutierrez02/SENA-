using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sena.WEB.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult NoAutorizado()
        {
            return View();
        }
    }
}
