using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOAN.Entities
{
    [Table("OrderDetails")]
    public class OrderDetailsEntity
    {
        [Key]
        public int OrderDetailId { set; get; }
        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal price { set; get; }
        public int ToTal { set; get; }
        public DateTime CreateOrderdetai { set; get;}
    }
}
