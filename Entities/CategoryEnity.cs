using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOAN.Entities
{
    [Table("Category")]
    public class CategoryEnity:BaseEntity
    {
        public string Name { get; set; }
        public string Slug { set; get; }
        public string Image { set; get; }
        public string Description { get; set; }

    }
}
