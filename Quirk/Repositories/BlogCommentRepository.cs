using Microsoft.EntityFrameworkCore;
using Quirk.Data;
using Quirk.Models.Domain;

namespace Quirk.Repositories
{
    public class BlogCommentRepository : IBlogCommentRepository
    {
        private readonly QuirkDbContext _quirkDbcontext;
        public BlogCommentRepository(QuirkDbContext quirkDbcontext)
        {
            _quirkDbcontext = quirkDbcontext;
        }
        public async Task<BlogPostComment> AddAsync(BlogPostComment blogComment)
        {
            await _quirkDbcontext.Comments.AddAsync(blogComment);
            await _quirkDbcontext.SaveChangesAsync();
            return blogComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetByBlogIdAsync(string blogPostId)
        {
            return await _quirkDbcontext.Comments
                .Where(x => x.BlogPostId == blogPostId)
                .ToListAsync();
        }
    }
}
