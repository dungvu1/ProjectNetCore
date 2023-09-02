namespace DOAN.Models
{
    public class Roles
    {
        public int RoleId { set; get; }
        public string RoleName { set; get; }
        public string Description { set; get; }
        public bool IsDeleted { get; set; }
    }
}
