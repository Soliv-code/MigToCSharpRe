using ADM.Domain;
using Memory.Domain;
using PA.Domain;
using Plugins.ADM.MSSQL;
using UseCases.Interfaces;
using UseCases.PluginInterfaces;
using WORK.Domain;

namespace Plugins.WORK.MSSQL
{
    public class WORKSQLRepository : IWORKRepository
    {
        private readonly WORKContext db;
        private readonly ADMSQLRepository admSQLRepository;

        public WORKSQLRepository(WORKContext db, ADMSQLRepository admSQLRepository)
        {
            this.db = db;
            this.admSQLRepository = admSQLRepository;
        }
        public void TaskUserCacheAggregate(
            int pTenantID, 
            List<TmpUser> users,
            List<TmpTask> tasks,
            List<TmpTaskResponsibleUser> taskResponsibleUsers
            )
        {
            bool IsOwnerOnly = false;
            byte
                AssignedTo = 2,
                DistrictAvailable = 13,
                pUserWorkType = 16,
                AllTaskAvailable = 19,
                CreatedBy = 20;
            DateTime Now = DateTime.UtcNow;
            //------------------------------------------------------------------------------------
            List<TmpListCategory> listCategories = new List<TmpListCategory>();
            List<int> Ids = new List<int>() { DistrictAvailable, pUserWorkType, AllTaskAvailable };
            var selectedCategories = db.TaskListCategories
                                            .Where(tlc => Ids.Contains(tlc.ID))
                                            .Select(tlc => new { tlc.ID, tlc.PermissionExtID })
                                            .ToList();
            foreach (var category in selectedCategories)
            {
                listCategories.Add(
                    new TmpListCategory
                    {
                        ID = (byte)category.ID,
                        PermissionExtID = (short?)category.PermissionExtID
                    }
                );
            };
            //------------------------------------------------------------------------------------
            List<TmpUserTaskListCategory> userTaskListCategories = admSQLRepository.UserListCategoryGetAsync(pTenantID, users, listCategories);
            //------------------------------------------------------------------------------------
            List<TmpTaskUserCache> taskUserCaches = new List<TmpTaskUserCache>();
            if (userTaskListCategories.Any(utlc => utlc.TaskListCategoryID == AllTaskAvailable))
            {
                var selectedTmpTaskUserCache = (
                    from tlc in userTaskListCategories
                    from t in tasks
                    where tlc.TaskListCategoryID == AllTaskAvailable
                    select new
                    {
                        TaskID = t.ID,
                        UserID = (int)tlc.UserID,
                        TaskListCategoryID = tlc.TaskListCategoryID
                    }
                ).ToList();
                foreach (var el in selectedTmpTaskUserCache)
                {
                    taskUserCaches.Add(new TmpTaskUserCache
                    {
                        TaskID = el.TaskID,
                        UserID = el.UserID,
                        TaskListCategoryID = el.TaskListCategoryID
                    });
                }
                var usersToDelete = (
                    from u in users
                    where taskUserCaches.Any(tuc =>
                        tuc.UserID == u.ID &&
                        tuc.TaskListCategoryID == AllTaskAvailable)
                    select u
                ).ToList();
                foreach (var user in usersToDelete)
                {
                    users.Remove(user);
                }
            }
            //------------------------------------------------------------------------------------
            if (users.Count == 0) return;
            //------------------------------------------------------------------------------------
            List<TenantMember> tenantMembers = admSQLRepository.GetTenantMembers();
            var selecteTaskUserCash = (
            from t in tasks
                join tm in tenantMembers on t.CreatedBy equals tm.ID
                where tm.TenantID == pTenantID &&
                users.Any(u => u.ID == tm.UserID)
                select new
                {
                    TaskID = t.ID,
                    UserID = tm.UserID,
                    TaskListCategoryID = CreatedBy
                }).ToList();
            
            foreach (var el in selecteTaskUserCash)
            {
                taskUserCaches.Add(new TmpTaskUserCache
                {
                    TaskID = el.TaskID,
                    UserID = el.UserID,
                    TaskListCategoryID = el.TaskListCategoryID
                });
            }
            //------------------------------------------------------------------------------------
            List<UserWorkType> userWorkTypes = new List<UserWorkType>();
            List<WorkType> workTypes = db.WorkTypes.ToList();
            if (userTaskListCategories.Any(utlc => utlc.TaskListCategoryID == pUserWorkType))
            {
                var result = from tlc in userTaskListCategories
                             from t in tasks
                             where tlc.TaskListCategoryID == pUserWorkType
                             where (from uwt in userWorkTypes
                                    join wt in workTypes on uwt.TenantID equals wt.TenantID
                                    where uwt.TenantID == pTenantID
                                    where uwt.UserID == tlc.UserID
                                    where uwt.WorkTypeID == t.WorkTypeID
                                    where uwt.Deleted == null
                                    where wt.Deleted == null
                                    select 1).Any()
                             select new
                             {
                                 TaskID = t.ID,
                                 UserID = tlc.UserID,
                                 TaskListCategoryID = tlc.TaskListCategoryID
                             };
            }
            //------------------------------------------------------------------------------------
            TaskUserCacheAggregateResponsibility(
                pTenantID,
                tasks,
                taskUserCaches,
                users,
                userTaskListCategories,
                taskResponsibleUsers);
            //------------------------------------------------------------------------------------
            admSQLRepository.TaskUserCacheAggregate(pTenantID, userTaskListCategories, taskResponsibleUsers);
            //------------------------------------------------------------------------------------

        }

        public void TaskUserCacheAggregateResponsibility(
            int pTenantID,
            List<TmpTask> tasks,
            List<TmpTaskUserCache> taskUserCaches,
            List<TmpUser> users,
            List<TmpUserTaskListCategory> userTaskListCategories,
            List<TmpTaskResponsibleUser> taskResponsibleUsers
            )
        {
            List<TaskOnlineAssigned> taskOnlineAssigneds = db.TaskOnlineAssigneds.ToList();
            byte AssignedTo = 2;
            byte DistrictAvailable = 13;

            var taskAssigned = taskOnlineAssigneds
                .Where(toa =>
                    toa.TenantID == pTenantID &&
                toa.AssignedTo != null)
                .Where(toa => tasks.Any(t => t.ID == toa.TaskID))
                .Select(toa => new { TaskID = toa.TaskID, assignedTo = toa.AssignedTo })
                .ToList();

            var taskUserCache = from ta in taskAssigned
                                where (
                                        from u in users
                                        where u.ID == ta.assignedTo
                                        select 1).Any()
                                select new
                                {
                                    TaskID = ta.TaskID,
                                    UserID = ta.assignedTo,
                                    TaskListCategoryID = AssignedTo
                                };
            foreach (var el in taskUserCache)
            {
                taskUserCaches.Add(new TmpTaskUserCache { TaskID = el.TaskID, UserID = el.TaskID, TaskListCategoryID = el.TaskListCategoryID });
            }

            if (userTaskListCategories.Any(utc => utc.TaskListCategoryID == DistrictAvailable))
            {
                var taskResponsibleUser = (
                                            from t in tasks
                                            where t.ApprovalWith != null || t.EscalatedTo != null
                                            select new
                                            {
                                                TaskID = t.ID,
                                                UserID = t.ApprovalWith
                                            }
                                          )
                                          .Union
                                          (
                                            from ta in taskAssigned
                                            select new
                                            {
                                                TaskID = ta.TaskID,
                                                UserID = ta.assignedTo
                                            }
                                          ).Distinct().ToList();
                foreach (var el in taskResponsibleUser)
                {
                    taskResponsibleUsers.Add(new TmpTaskResponsibleUser { TaskID = el.TaskID, UserID = el.TaskID });
                }
            }
        }
    }
}
