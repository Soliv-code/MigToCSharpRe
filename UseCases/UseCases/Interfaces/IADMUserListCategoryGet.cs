using Memory.Domain;
namespace UseCases.Interfaces
{
    public interface IADMUserListCategoryGet
    {
        List<TmpUserTaskListCategory> Execute(
            int pTenantID, 
            List<TmpUser> users, 
            List<TmpListCategory> listCategories
            );
    }
}