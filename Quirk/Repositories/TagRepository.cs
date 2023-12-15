using Azure;
using Microsoft.EntityFrameworkCore;
using Quirk.Data;
using Quirk.Models.Domain;
using Quirk.Models.ViewModels;

namespace Quirk.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly QuirkDbContext _quirkDbContext;
        public TagRepository(QuirkDbContext quirkDbContext)
        {
            _quirkDbContext = quirkDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await _quirkDbContext.Tags.AddAsync(tag);
            await _quirkDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag> DeleteAsync(string id)
        {
            var deleteTag = await _quirkDbContext.Tags.FindAsync(id);
            if (deleteTag != null)
            {
                _quirkDbContext.Tags.Remove(deleteTag);
                await _quirkDbContext.SaveChangesAsync();
                return deleteTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
           return await _quirkDbContext.Tags.ToListAsync();

        }

        public async Task<Tag> GetAsync(string id)
        {
            return await _quirkDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await _quirkDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await _quirkDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}
