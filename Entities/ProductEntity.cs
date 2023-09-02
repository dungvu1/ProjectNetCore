using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOAN.Entities
{
    [Table("Product")]
    public class ProductEntity: BaseEntity
    {   
        public string Name { get; set; }
        public string Author { set; get; }
        public string Slug { set; get; }
        public string Image { set; get; }
        public string Description { get; set; }
        public Decimal Price { set; get; }
        public int Quantity { set; get; }
        public int Category_id { set; get; }

    }
}
