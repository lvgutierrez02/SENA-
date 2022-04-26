using Microsoft.AspNetCore.Mvc;

namespace Sena.WEB.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
