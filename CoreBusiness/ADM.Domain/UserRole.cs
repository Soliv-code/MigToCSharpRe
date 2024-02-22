using System.ComponentModel.DataAnnotations.Schema;

namespace ADM.Domain
{
    [Table("UserRole", Schema = "ADM")]
    public class UserRole
    {
        public int ID { get; set; }
        public int TenantID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
