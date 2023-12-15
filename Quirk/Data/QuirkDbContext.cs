using Microsoft.EntityFrameworkCore;
using Quirk.Models.Domain;

namespace Quirk.Data
{
    public class QuirkDbContext : DbContext
    {
        public QuirkDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
