using Microsoft.AspNetCore.Mvc;
using Quirk.Models.ViewModels;
using Quirk.Repositories;

namespace Quirk.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogPostLikeRepository _blogLikeRepository;
        public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogLikeRepository)
        {
            _blogPostRepository = blogPostRepository;
            _blogLikeRepository = blogLikeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await _blogPostRepository.GetByUrlAsync(urlHandle);
            var blogDetailsVM = new BlogDetailsViewModel();

            if (blogPost != null)
            {
               var totalLikes = await _blogLikeRepository.GetTotalLikes(blogPost.Id);

                blogDetailsVM = new BlogDetailsViewModel
                {
                    Id = blogPost.Id,
                    Content = blogPost.Content,
                    PageTitle = blogPost.PageTitle,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Heading = blogPost.Heading,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    UrlHandle = blogPost.UrlHandle,
                    Visible = blogPost.Visible,
                    Tags = blogPost.Tags,
                    TotalLikes = totalLikes,
                };
            }
            return View(blogDetailsVM);
        }
    }
}
