using System.ComponentModel.DataAnnotations.Schema;

namespace ADM.Domain
{
    [Table("TenantMember", Schema = "ADM")]
    public class TenantMember
    {
        public int ID { get; set; }
        public int TenantID { get; set; }
        public int UserID { get; set; }
    }
}
