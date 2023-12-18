namespace Quirk.Models.ViewModels
{
    public class AddLikeRequest
    {
        public string BlogPostId { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; } = Guid.NewGuid().ToString();
    }
}
