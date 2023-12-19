using Quirk.Models.Domain;

namespace Quirk.Repositories
{
    public interface IBlogCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment blogComment);
        Task<IEnumerable<BlogPostComment>> GetByBlogIdAsync(string blogPostId);
    }
}
