using ASO.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASO.DataAccess
{
    public class DataContext : IdentityDbContext<User, UserRole, long>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}