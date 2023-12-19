using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quirk.Data;

namespace Quirk.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly QuirkDbContext _quirkDbContext;
        public UserRepository(QuirkDbContext quirkDbContext)
        {
            _quirkDbContext = quirkDbContext;
        }
        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users =await _quirkDbContext.Users.ToListAsync();
            var superAdmin = await _quirkDbContext.Users
                .FirstOrDefaultAsync(x => x.Email == "superadmin@quirk.com");
            if (superAdmin != null)
            {
                users.Remove(superAdmin);
            }
            return users;
        }
    }
}
