using Memory.Domain;
using UseCases.Interfaces;
using UseCases.PluginInterfaces;

namespace UseCases.WORKUseCases
{
    public class TaskUserCacheAggregateResponsibility : IWORKTaskUserCacheAggregateResponsibility
    {
        private readonly IWORKRepository workPluginInterfaces;

        public TaskUserCacheAggregateResponsibility(IWORKRepository workPluginInterfaces)
        {
            this.workPluginInterfaces = workPluginInterfaces;
        }
        public void Execute(int pTenantID,
            List<TmpTask> tasks,
            List<TmpTaskUserCache> taskUserCaches,
            List<TmpUser> users,
            List<TmpUserTaskListCategory> userTaskListCategories,
            List<TmpTaskResponsibleUser> taskResponsibleUsers)
        {
            workPluginInterfaces.TaskUserCacheAggregateResponsibility(
                pTenantID, 
                tasks, 
                taskUserCaches, 
                users, 
                userTaskListCategories, 
                taskResponsibleUsers);
        }
    }
}
