using Microsoft.EntityFrameworkCore;
using Quirk.Data;

namespace Quirk.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly QuirkDbContext _quirkDbContext;
        public BlogPostLikeRepository(QuirkDbContext quirkDbContext)
        {
            _quirkDbContext = quirkDbContext;
        }
        public async Task<int> GetTotalLikes(string id)
        {
           return await _quirkDbContext.BlogPostLikes
                .CountAsync(x  => x.BlogPostId == id);
        }
    }
}
