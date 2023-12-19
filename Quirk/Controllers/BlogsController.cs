using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quirk.Models.Domain;
using Quirk.Models.ViewModels;
using Quirk.Repositories;

namespace Quirk.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogPostLikeRepository _blogLikeRepository;
        private readonly IBlogCommentRepository _blogCommentRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public BlogsController(IBlogPostRepository blogPostRepository, 
            IBlogPostLikeRepository blogLikeRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IBlogCommentRepository blogCommentRepository)
        {
            _blogPostRepository = blogPostRepository;
            _blogLikeRepository = blogLikeRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _blogCommentRepository = blogCommentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var liked = false;
            var blogPost = await _blogPostRepository.GetByUrlAsync(urlHandle);
            var blogDetailsVM = new BlogDetailsViewModel();

            if (blogPost != null)
            {
               var totalLikes = await _blogLikeRepository.GetTotalLikes(blogPost.Id);
                if (_signInManager.IsSignedIn(User))
                {
                    var likesForBlog = await _blogLikeRepository.GetLikesForBlog(blogPost.Id);
                    var userId = _userManager.GetUserId(User);
                    if (userId != null)
                    {
                        var likeFromUser = likesForBlog.FirstOrDefault(x => x.UserId == userId);
                        liked = likeFromUser != null;
                    }
                }

                var blogComments = await _blogCommentRepository.GetByBlogIdAsync(blogPost.Id);
                var blogCommentView = new List<BlogCommentsViewModel>();
                foreach (var blogComment in blogComments)
                {
                    blogCommentView.Add(new BlogCommentsViewModel
                    {
                        Description = blogComment.Description,
                        DateAdded = blogComment.DateAdded,
                        UserName = (await _userManager.FindByIdAsync(blogComment.UserId.ToString())).UserName
                    });
                }

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
                    Liked = liked,
                    Comments = blogCommentView,
                };
            }
            return View(blogDetailsVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var commentDM = new BlogPostComment
                {
                    BlogPostId = blogDetailsViewModel.Id,
                    Description = blogDetailsViewModel.CommentDescription,
                    UserId = _userManager.GetUserId(User),
                    DateAdded = DateTime.UtcNow,
                };
                await _blogCommentRepository.AddAsync(commentDM);
                return RedirectToAction("Index", "Blogs", 
                    new { urlHandle = blogDetailsViewModel.UrlHandle});
            }
            return View();
            
        }
    }
}
