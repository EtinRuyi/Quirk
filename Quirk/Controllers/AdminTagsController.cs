using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> SubmitTag(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await _quirkDbContext.Tags.AddAsync(tag);
            await _quirkDbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var tags = await _quirkDbContext.Tags.ToListAsync();
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {

            var tag = await _quirkDbContext.Tags.FirstOrDefaultAsync(x => x.Id == Id);
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
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var existingTag = await _quirkDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

               await _quirkDbContext.SaveChangesAsync();
                return RedirectToAction("Edit", new { Id = editTagRequest.Id });
            }
            return RedirectToAction("Edit", new {Id = editTagRequest.Id});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deleteTag = await _quirkDbContext.Tags.FindAsync(editTagRequest.Id);
            if (deleteTag != null)
            {
                _quirkDbContext.Tags.Remove(deleteTag);
                await _quirkDbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { Id = editTagRequest.Id });
        }
    }
}
