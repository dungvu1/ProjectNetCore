using System.ComponentModel.DataAnnotations;

namespace DOAN.Entities
{
    public class BaseEntity
    {
        [Key]   
        
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { set; get; }
        public bool IsDeleted { get; set; }
    }
}
