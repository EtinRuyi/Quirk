namespace Quirk.Models.ViewModels
{
    public class BlogCommentsViewModel
    {
        public string Description { get; set; }
        public string UserName { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}
