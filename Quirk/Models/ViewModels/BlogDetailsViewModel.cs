﻿using Quirk.Models.Domain;

namespace Quirk.Models.ViewModels
{
    public class BlogDetailsViewModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;
        public string Author { get; set; }
        public bool Visible { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public int TotalLikes { get; set; }
        public bool Liked { get; set;}
        public string CommentDescription { get; set; }
        public IEnumerable<BlogCommentsViewModel> Comments { get; set;}
        public IEnumerable<BlogPost> BlogPosts { get; set; }
    }
}
