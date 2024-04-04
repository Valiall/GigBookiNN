using Microsoft.AspNetCore.Mvc;

namespace GigBookin.Controllers
{
    public class EventOrganiserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
