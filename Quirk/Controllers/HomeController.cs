﻿using Microsoft.AspNetCore.Mvc;
using Quirk.Models;
using Quirk.Models.ViewModels;
using Quirk.Repositories;
using System.Diagnostics;

namespace Quirk.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ITagRepository _tagRepository;

        public HomeController(ILogger<HomeController> logger, 
            IBlogPostRepository blogPostRepository, 
            ITagRepository tagRepository)
        {
            _logger = logger;
            _blogPostRepository = blogPostRepository;
            _tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _blogPostRepository.GetAllAsync();
            var tags = await _tagRepository.GetAllAsync();

            var model = new HomeViewModel
            {
                BlogPosts = posts,
                Tags = tags
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}