using System.ComponentModel.DataAnnotations.Schema;

namespace ADM.Domain
{
    [Table("UserDistrict", Schema = "ADM")]
    public class UserDistrict
    {
        public int ID { get; set; }
        public int TenantID { get; set; }
        public int UserID { get; set; }
        public int DistrictID { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
