using Microsoft.EntityFrameworkCore;
using WORK.Domain;

namespace Plugins.WORK.MSSQL
{
    public class WORKContext : DbContext
    {
        public WORKContext(DbContextOptions<WORKContext> options) : base(options) { }
        public DbSet<TaskListCategory> TaskListCategories { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }
        public DbSet<TaskOnlineAssigned> TaskOnlineAssigneds { get; set; }
       
    }
}
