using Microsoft.EntityFrameworkCore;
using Quirk.Data;
using Quirk.Models.Domain;

namespace Quirk.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly QuirkDbContext _quirkDbContext;
        public BlogPostLikeRepository(QuirkDbContext quirkDbContext)
        {
            _quirkDbContext = quirkDbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
           await _quirkDbContext.BlogPostLikes.AddAsync(blogPostLike);
            await _quirkDbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(string id)
        {
           return await _quirkDbContext.BlogPostLikes
                .Where(x =>  x.BlogPostId == id).ToListAsync();
        }

        public async Task<int> GetTotalLikes(string id)
        {
           return await _quirkDbContext.BlogPostLikes
                .CountAsync(x  => x.BlogPostId == id);
        }
    }
}
