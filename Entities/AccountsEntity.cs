using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOAN.Entities
{
    [Table("Accounts")]
    public class AccountsEntity
    {
        [Key]
        public int AccountId { get; set; }
        public int Phone { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string FullName { set; get; }
        public int RoleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpDatedDate { get; set;}
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
