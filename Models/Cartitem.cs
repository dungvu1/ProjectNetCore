using DOAN.Entities;
using XAct;

namespace DOAN.Models
{
    public class Cartitem
    {
        public ProductEntity Product { get; set; }
        public int  amount { get; set; }
        public decimal TotalMoney => amount * Product.Price;
    }
}
