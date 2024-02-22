using Memory.Domain;
using UseCases.Interfaces;
using UseCases.PluginInterfaces;

namespace UseCases.WORKUseCases
{
    public class TaskUserCacheAggregate : IWORKTaskUserCacheAggregate
    {
        private readonly IWORKRepository workPluginInterfaces;

        public TaskUserCacheAggregate(IWORKRepository workPluginInterfaces)
        {
            this.workPluginInterfaces = workPluginInterfaces;
        }

        public void Execute(int pTenantID, List<TmpUser> users, List<TmpTask> tasks, List<TmpTaskResponsibleUser> taskResponsibleUsers)
        {
            workPluginInterfaces.TaskUserCacheAggregate(pTenantID, users, tasks, taskResponsibleUsers);
        }
    }
}
