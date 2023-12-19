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
        public async Task<BlogComment> AddAsync(BlogComment blogComment)
        {
            await _quirkDbcontext.Comments.AddAsync(blogComment);
            await _quirkDbcontext.SaveChangesAsync();
            return blogComment;
        }
    }
}
