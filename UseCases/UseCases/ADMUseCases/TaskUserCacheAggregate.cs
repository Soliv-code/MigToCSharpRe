using Memory.Domain;
using System.Collections.Generic;
using UseCases.ADMPluginInterfaces;
using UseCases.Interfaces;

namespace UseCases.ADMUseCases
{
    public class TaskUserCacheAggregate : IADMTaskUserCacheAggregate
    {
        private readonly IADMRepository admPluginInterfaces;

        public TaskUserCacheAggregate(IADMRepository admPluginInterfaces)
        {
            this.admPluginInterfaces = admPluginInterfaces;
        }
        public void Execute(
            int pTenantID, 
            List<TmpUserTaskListCategory> userTaskListCategories, 
            List<TmpTaskResponsibleUser> tmpTaskResponsibleUsers)
        {
            admPluginInterfaces.TaskUserCacheAggregate(pTenantID, userTaskListCategories, tmpTaskResponsibleUsers);
        }
    }
}
