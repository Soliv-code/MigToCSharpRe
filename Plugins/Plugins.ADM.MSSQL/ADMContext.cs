using ADM.Domain;
using Microsoft.EntityFrameworkCore;

namespace Plugins.ADM.MSSQL
{
    public class ADMContext : DbContext
    {
        public ADMContext(DbContextOptions<ADMContext> options) : base(options) { }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserDistrict> UserDistricts { get; set; }
        public DbSet<TenantMember> TenantMembers { get; set; }
        public DbSet<RolePermissionExt> RolePermissionExts { get; set; }

    }
}
