

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOAN.Entities
{
    [Table("Status")]
    public class StatusEntity
    {
        [Key]  
        
        public int StatusId { set; get; }
        public string StatusName { set; get; }
    }
}
