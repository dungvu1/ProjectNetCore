namespace DOAN.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { set; get; }
        public string Slug { set; get; }
        public IFormFile Image { set; get; }
        public string Description { get; set; }
        public Decimal Price { set; get; }
        public int Quantity { set; get; }
        public int Category_id { set; get; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public bool IsActive { set; get; }
        public bool IsDeleted { get; set; }
    }
}
