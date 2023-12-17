using Microsoft.EntityFrameworkCore;
using Quirk.Data;
using Quirk.Models.Domain;

namespace Quirk.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly QuirkDbContext _quirkDbContext;
        public BlogPostRepository(QuirkDbContext quirkDbContext)
        {
            _quirkDbContext = quirkDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await _quirkDbContext.AddAsync(blogPost);
            await _quirkDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost> DeleteAsync(string id)
        {
            var deletePost = await _quirkDbContext.BlogPosts.FindAsync(id);
            if (deletePost != null)
            {
                _quirkDbContext.BlogPosts.Remove(deletePost);
                await _quirkDbContext.SaveChangesAsync();
                return deletePost;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
           return await _quirkDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost> GetAsync(string id)
        {
            return await _quirkDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost> GetByUrlAsync(string urlHandle)
        {
            return await _quirkDbContext.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await _quirkDbContext.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (existingBlogPost != null)
            {
                existingBlogPost.Id = blogPost.Id;
                existingBlogPost.Heading = blogPost.Heading;
                existingBlogPost.PageTitle = blogPost.PageTitle;
                existingBlogPost.Content = blogPost.Content;
                existingBlogPost.ShortDescription = blogPost.ShortDescription;
                existingBlogPost.Author = blogPost.Author;
                existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlogPost.UrlHandle = blogPost.UrlHandle;
                existingBlogPost.PublishedDate = blogPost.PublishedDate;
                existingBlogPost.Visible = blogPost.Visible;
                existingBlogPost.Tags = blogPost.Tags;

                await _quirkDbContext.SaveChangesAsync();
                return existingBlogPost;
            }
            return null;
        }
    }
}
