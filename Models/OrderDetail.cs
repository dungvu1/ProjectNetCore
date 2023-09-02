namespace DOAN.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { set; get; }
        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal price { set; get; }
        public int ToTal { set; get; }
        public DateTime CreateOrderdetai { set; get; }
    }
}
