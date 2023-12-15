using Microsoft.AspNetCore.Mvc;
using Quirk.Models.Domain;

namespace Quirk.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag> GetAsync(string id);
        Task<Tag> DeleteAsync(string id);
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag> AddAsync(Tag tag);
    }
}
