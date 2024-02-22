using Memory.Domain;

namespace UseCases.Interfaces
{
    public interface IWORKTaskUserCacheAggregate
    {
        void Execute(int pTenantID,
            List<TmpUser> users,
            List<TmpTask> tasks,
            List<TmpTaskResponsibleUser> taskResponsibleUsers);
    }
}