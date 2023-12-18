namespace Quirk.Models.Domain
{
    public class BlogPostLike
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string BlogPostId { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; } = Guid.NewGuid().ToString();

    }
}
