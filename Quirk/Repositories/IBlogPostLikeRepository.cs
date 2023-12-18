using Quirk.Models.Domain;

namespace Quirk.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(string id);
        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);
    }
}
