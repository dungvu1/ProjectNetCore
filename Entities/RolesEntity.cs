using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Printing;

namespace DOAN.Entities
{
    [Table("Roles")]
    public class RolesEntity
    {
        [Key]
        public int RoleId { set; get; }
        public string RoleName { set; get; }
        public string Description { set; get; }
        public bool IsDeleted { get; set; }
    }
}
