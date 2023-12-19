using Quirk.Models.Domain;

namespace Quirk.Repositories
{
    public interface IBlogCommentRepository
    {
        Task<BlogComment> AddAsync(BlogComment blogComment);
    }
}
