using System.ComponentModel.DataAnnotations.Schema;

namespace WORK.Domain
{
    [Table("WorkType", Schema = "WORK")]
    public class WorkType
    {
        public int ID { get; set; }
        public int TenantID { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
