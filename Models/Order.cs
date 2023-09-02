namespace DOAN.Models
{
    public class Order
    {
        public int OrderId { set; get; }
        public int UsersId { set; get; }
        public DateTime OrderDate { set; get; }
        public string FullName { set; get; }
        public int Phone { set; get; }
        public string Address { set; get; }
        public string Email { set; get; }
        public int Paymentid { set; get; }
        public int StatusId { set; get; }
        public string StatusName { set; get; }
        public bool IsDeleted { set; get; }
        public string Note { set; get; }
        public int ToTalOrder { set; get; }
    }
}
