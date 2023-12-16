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

        public Task<BlogPost> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
           return await _quirkDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public Task<BlogPost> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
