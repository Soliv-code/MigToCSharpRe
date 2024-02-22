using ADM.Domain;
using Memory.Domain;

namespace UseCases.ADMPluginInterfaces
{
    public interface IADMRepository
    {
        public List<TenantMember> GetTenantMembers();
        public List<TmpUserTaskListCategory> UserListCategoryGetAsync(
            int pTenantID, 
            List<TmpUser> users, 
            List<TmpListCategory> listCategories
            );

        public void TaskUserCacheAggregate(
            int pTenantID,
            List<TmpUserTaskListCategory> userTaskListCategories,
            List<TmpTaskResponsibleUser> tmpTaskResponsibleUsers
            );
    }
}
