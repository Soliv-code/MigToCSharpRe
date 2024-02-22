using System.ComponentModel.DataAnnotations.Schema;

namespace WORK.Domain
{
    [Table("TaskOnlineAssigned", Schema = "WORK")]
    public class TaskOnlineAssigned
    {
        public int ID { get; set; }
        public int TaskID { get; set; }
        public int TenantID { get; set; }
        public int? AssignedTo { get; set; }
    }
}
