using ADM.Domain;
using Memory.Domain;
using UseCases.ADMPluginInterfaces;
using UseCases.Interfaces;

namespace Plugins.ADM.MSSQL
{
    public class ADMSQLRepository : IADMRepository
    {
        private readonly ADMContext db;
        public ADMSQLRepository(ADMContext db)
        {
            this.db = db;
        }
        public List<TenantMember> GetTenantMembers()
        {
            return db.TenantMembers.ToList();
        }
        // Он был асинхронным, но пришлось поменять, т.к. сначала преобразование типов,
        // потом тесты не работали из-за TASK / IEnumerable, в общем, оставил без Async / Await
        public List<TmpUserTaskListCategory> UserListCategoryGetAsync(
            int pTenantID, 
            List<TmpUser> users,
            List<TmpListCategory> listCategories)
        {
            List<TmpUserTaskListCategory> userTaskListCategories = new List<TmpUserTaskListCategory>();

            List<UserRole> userRoles =  db.UserRoles.ToList();
            List<RolePermissionExt> rolePermissionExts =  db.RolePermissionExts.ToList();

            var result = (
                from u in users
                from lc in listCategories
                where (
                    from ur in userRoles
                    where
                        ur.TenantID == pTenantID &&
                        ur.UserID == u.ID &&
                        ur.Deleted == null && (
                            from rpe in rolePermissionExts
                            where
                                rpe.TenantID == ur.TenantID &&
                                rpe.RoleID == ur.RoleID &&
                                rpe.PermissionExtID == lc.PermissionExtID &&
                                rpe.Deleted == null
                            select 1).Any()
                    select 1).Any()
                select new { UserID = (int)u.ID, ListCategoryID = (byte)lc.ID }
                )
                .Union(
                    from u in users
                    from lc in listCategories
                    where lc.PermissionExtID == null
                    select new { UserID = (int)u.ID, ListCategoryID = (byte)lc.ID }
                ).ToList();
            foreach (var item in result)
            {
                userTaskListCategories.Add(
                    new TmpUserTaskListCategory
                    {
                        UserID = item.UserID,
                        TaskListCategoryID = item.ListCategoryID
                    });
            }
            return userTaskListCategories.ToList();
        }
        
        public void TaskUserCacheAggregate(
            int pTenantID, 
            List<TmpUserTaskListCategory> userTaskListCategories,
            List<TmpTaskResponsibleUser> tmpTaskResponsibleUsers
            )
        {
            List<UserDistrict> userDistricts = db.UserDistricts.ToList();
            byte DistrictAvailable = 13;
            if (userTaskListCategories.Any(utlc => utlc.TaskListCategoryID == DistrictAvailable))
            {
                var taskUserCache = userTaskListCategories
                    .Where(tlc => tlc.TaskListCategoryID == DistrictAvailable)
                    .Join(tmpTaskResponsibleUsers,
                        tlc => tlc.UserID,
                        tu => tu.UserID,
                        (tlc, tu) => new { tu.TaskID, tlc.UserID, tlc.TaskListCategoryID })
                    .Where(tu => userDistricts.Any(ud =>
                        ud.TenantID == pTenantID &&
                        ud.UserID == tu.UserID &&
                        ud.Deleted == null &&
                        userDistricts.Any(ud1 =>
                            ud1.TenantID == ud.TenantID &&
                            ud1.DistrictID == ud.DistrictID &&
                            ud1.UserID == tu.UserID &&
                            ud1.Deleted == null)))
                    .ToList();
            }
        }
    }
}
