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

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            var tags = _quirkDbContext.Tags.ToList();
            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(string Id)
        {

            var tag = _quirkDbContext.Tags.FirstOrDefault(x => x.Id == Id);
            if (tag != null)
            {
                var editTagReq = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagReq);
            }
            return View(null);
        }

        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var existingTag = _quirkDbContext.Tags.Find(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                _quirkDbContext.SaveChanges();
                return RedirectToAction("Edit", new { Id = editTagRequest.Id });
            }
            return RedirectToAction("Edit", new {Id = editTagRequest.Id});
        }
    }
}
