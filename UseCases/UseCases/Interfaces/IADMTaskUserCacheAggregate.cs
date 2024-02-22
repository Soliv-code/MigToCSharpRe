using Memory.Domain;

namespace UseCases.Interfaces
{
    public interface IADMTaskUserCacheAggregate
    {
        void Execute(
            int pTenantID, 
            List<TmpUserTaskListCategory> userTaskListCategories, 
            List<TmpTaskResponsibleUser> tmpTaskResponsibleUsers
            );
    }
}
