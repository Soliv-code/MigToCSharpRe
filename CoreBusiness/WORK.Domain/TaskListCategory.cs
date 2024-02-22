using System.ComponentModel.DataAnnotations.Schema;

namespace WORK.Domain
{
    [Table("TaskListCategory", Schema = "WORK")]
    public class TaskListCategory
    {
        public int ID { get; set; }
        public int PermissionExtID { get; set; }

    }
}
