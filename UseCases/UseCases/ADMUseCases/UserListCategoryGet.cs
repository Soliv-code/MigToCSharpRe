using Memory.Domain;
using UseCases.ADMPluginInterfaces;
using UseCases.Interfaces;

namespace UseCases.ADMUseCases
{
    public class UserListCategoryGet : IADMUserListCategoryGet
    {
        private readonly IADMRepository admPluginInterfaces;

        public UserListCategoryGet(IADMRepository admPluginInterfaces)
        {
            this.admPluginInterfaces = admPluginInterfaces;
        }
        public List<TmpUserTaskListCategory> Execute(
            int pTenantID,
            List<TmpUser> users,
            List<TmpListCategory> listCategories
            )
        {
            return admPluginInterfaces.UserListCategoryGetAsync(pTenantID, users, listCategories);
        }
    }
}
