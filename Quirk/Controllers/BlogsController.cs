﻿using Microsoft.AspNetCore.Mvc;
using Quirk.Repositories;

namespace Quirk.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        public BlogsController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await _blogPostRepository.GetByUrlAsync(urlHandle);
            return View(blogPost);
        }
    }
}