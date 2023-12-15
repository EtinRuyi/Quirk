namespace Quirk.Models.Domain
{
    public class Tag
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}