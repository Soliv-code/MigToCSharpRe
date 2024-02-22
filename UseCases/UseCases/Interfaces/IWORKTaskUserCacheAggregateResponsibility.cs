using Memory.Domain;

namespace UseCases.Interfaces
{
    public interface IWORKTaskUserCacheAggregateResponsibility
    {
        void Execute(int pTenantID,
            List<TmpTask> tasks,
            List<TmpTaskUserCache> taskUserCaches,
            List<TmpUser> users,
            List<TmpUserTaskListCategory> userTaskListCategories,
            List<TmpTaskResponsibleUser> taskResponsibleUsers
            );
    }
}