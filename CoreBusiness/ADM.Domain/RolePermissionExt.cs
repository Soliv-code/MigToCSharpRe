using System.ComponentModel.DataAnnotations.Schema;

namespace ADM.Domain
{
    [Table("RolePermissionExt", Schema = "ADM")]
    public class RolePermissionExt
    {
        public int ID { get; set; }
        public int TenantID { get; set; }
        public int RoleID { get; set; }
        public int PermissionExtID { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
