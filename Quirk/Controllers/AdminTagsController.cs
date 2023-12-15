using Microsoft.AspNetCore.Mvc;

namespace Quirk.Controllers
{
    public class AdminTagsController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
