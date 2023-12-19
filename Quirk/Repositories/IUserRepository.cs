using Microsoft.AspNetCore.Identity;

namespace Quirk.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
