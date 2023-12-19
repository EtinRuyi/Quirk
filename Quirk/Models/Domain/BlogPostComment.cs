namespace Quirk.Models.Domain
{
    public class BlogPostComment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string BlogPostId { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}
