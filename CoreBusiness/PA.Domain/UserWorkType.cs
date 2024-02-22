using System.ComponentModel.DataAnnotations.Schema;

namespace PA.Domain
{
    [Table("UserWorkType", Schema = "PA")]
    public class UserWorkType
    {
        public int TenantID { get; set; }
        public short WorkTypeID { get; set; }
        public int UserID { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
