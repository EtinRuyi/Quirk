using Microsoft.AspNetCore.Mvc;

namespace Quirk.Controllers
{
    public class AdminBlogPostController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
