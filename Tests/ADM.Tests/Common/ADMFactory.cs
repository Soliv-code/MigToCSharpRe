using ADM.Domain;
using Microsoft.EntityFrameworkCore;
using Plugins.ADM.MSSQL;

namespace ADM.Tests.Common
{
    public class ADMFactory
    {
        public static ADMContext Create()
        {
            var options = new DbContextOptionsBuilder<ADMContext>()
                .UseInMemoryDatabase("SomeString")
                .Options;
            var context = new ADMContext(options);
            context.Database.EnsureCreated();
            context.UserRoles.AddRange(
                new UserRole { ID = 1, TenantID = 0, UserID = 1, RoleID = 0, Deleted = null },
                new UserRole { ID = 2, TenantID = 0, UserID = 2, RoleID = 0, Deleted = null },
                new UserRole { ID = 3, TenantID = 1, UserID = 3, RoleID = 0, Deleted = null },
                new UserRole { ID = 4, TenantID = 0, UserID = 4, RoleID = 0, Deleted = null }
            );
            context.RolePermissionExts.AddRange(
                new RolePermissionExt { ID = 1, TenantID = 0, RoleID = 0, PermissionExtID = 0, Deleted = null},
                new RolePermissionExt { ID = 2, TenantID = 0, RoleID = 0, PermissionExtID = 0, Deleted = null },
                new RolePermissionExt { ID = 3, TenantID = 0, RoleID = 0, PermissionExtID = 0, Deleted = null },
                new RolePermissionExt { ID = 4, TenantID = 1, RoleID = 0, PermissionExtID = 0, Deleted = null },
                new RolePermissionExt { ID = 5, TenantID = 0, RoleID = 0, PermissionExtID = 0, Deleted = DateTime.Now }
            );
            context.SaveChanges();
            return context;
        }
        public static void Destroy(ADMContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
