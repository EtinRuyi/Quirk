﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quirk.Models.Domain;
using Quirk.Models.ViewModels;
using Quirk.Repositories;

namespace Quirk.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository _tagRepository;
        public AdminTagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
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
            ValidateAddTagRequest(addTagRequest);
            if (ModelState.IsValid == false)
            {
                return View();
            }
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await _tagRepository.AddAsync(tag);
            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var tags = await _tagRepository.GetAllAsync();
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {

            var tag = await _tagRepository.GetAsync(Id);
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
            var updatedTag = await _tagRepository.UpdateAsync(tag);
            if (updatedTag != null){}
            else{}

            return RedirectToAction("Edit", new {id = editTagRequest.Id});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await _tagRepository.DeleteAsync(editTagRequest.Id);
            if (deletedTag != null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { Id = editTagRequest.Id });
        }

        private void ValidateAddTagRequest(AddTagRequest addTagRequest)
        {
            if (addTagRequest.Name != null && addTagRequest.DisplayName != null)
            {
                if (addTagRequest.Name == addTagRequest.DisplayName)
                {
                    ModelState.AddModelError("Names", "Names cannot be the same");
                }
            }
        }
    }
}
