using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyVet.Controllers
{
    public class DatesController : Controller
    {
        // GET: DatesController
        public IActionResult Index()
        {
            return View();
        }
    }
}
