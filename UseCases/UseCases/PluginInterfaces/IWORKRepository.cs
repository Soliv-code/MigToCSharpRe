using Memory.Domain;

namespace UseCases.PluginInterfaces
{
    public interface IWORKRepository
    {
        public void TaskUserCacheAggregateResponsibility(
            int pTenantID,
            List<TmpTask> tasks,
            List<TmpTaskUserCache> taskUserCaches,
            List<TmpUser> users,
            List<TmpUserTaskListCategory> userTaskListCategories,
            List<TmpTaskResponsibleUser> taskResponsibleUsers
            );
        public void TaskUserCacheAggregate(
            int pTenantID, 
            List<TmpUser> users,
            List<TmpTask> tasks,
            List<TmpTaskResponsibleUser> taskResponsibleUsers
            );
    }
}
