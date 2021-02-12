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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(b => { b.HasOne(user => user.Role).WithMany(role => role.Users).IsRequired(); });

            builder.Entity<UserRole>(b =>
            {
                b.HasKey(r => r.Id);
                b.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();
                b.ToTable("AspNetRoles");
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                b.Property(u => u.Name).HasMaxLength(256);
                b.Property(u => u.NormalizedName).HasMaxLength(256);

                b.HasMany(role => role.Users).WithOne(user => user.Role);
                b.HasMany<IdentityRoleClaim<long>>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            });

            builder.Ignore<IdentityRoleClaim<long>>();
            builder.Ignore<IdentityUserClaim<long>>();
            builder.Ignore<IdentityUserRole<long>>();
        }
    }
}