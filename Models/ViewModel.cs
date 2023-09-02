using DOAN.Entities;
namespace DOAN.Models
{
    public class ViewModel
    {
        public List<Category> Categories { get; set; }
        public List<ProductEntity> Productes { set; get; }
        public Product Product { get; set; }
        public string imagee { set; get; }
        public int TotalCount { set; get; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
    }
}
