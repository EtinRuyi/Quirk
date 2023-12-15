using Microsoft.AspNetCore.Mvc;
using Quirk.Data;
using Quirk.Models.Domain;
using Quirk.Models.ViewModels;

namespace Quirk.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly QuirkDbContext _quirkDbContext;
        public AdminTagsController(QuirkDbContext quirkDbContext)
        {
            _quirkDbContext = quirkDbContext;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult SubmitTag(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            _quirkDbContext.Tags.Add(tag);
            _quirkDbContext.SaveChanges();

            return View("Add");
        }
    }
}
