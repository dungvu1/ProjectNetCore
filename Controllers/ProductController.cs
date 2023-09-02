using DOAN.Entities;
using DOAN.Extension;
using DOAN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace DOAN.Controllers
{

    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _dbContext;

        public ProductController(ILogger<HomeController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public IActionResult Index(int Id, int pageSize, int pageNumber)
        {
            var cart = HttpContext.Session.Get<List<Cartitem>>("GioHang");
            ViewBag.gih = cart;
            if (Id == 0)
            {
                if (pageSize == 0)
                    pageSize = 8;
                if (pageNumber == 0)
                    pageNumber = 1;
                var product = from p in _dbContext.ProductEntities
                              where (p.IsDeleted == false)
                              select new ProductEntity()
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  Description = p.Description,
                                  Image = p.Image,
                                  Price = p.Price,
                                  Quantity = p.Quantity,
                                  Category_id = p.Category_id,
                              };
                var totalCount = product.Count();
                var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);
                var skip = pageNumber * pageSize - pageSize;
                var dl = new ViewModel()
                {
                    TotalCount = totalCount,
                    Productes = product.Skip(skip).Take(pageSize).ToList(),
                    PageSize = pageSize,
                    PageNumber = pageNumber,
                    PageCount= pageCount
                    
                };
                return View(dl);
            }
            else
            {
                if (pageSize == 0)
                    pageSize = 8;
                if (pageNumber == 0)
                    pageNumber = 1;
                var product = from p in _dbContext.ProductEntities
                              where (p.IsDeleted == false && p.Category_id == Id)
                              select new ProductEntity()
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  Description = p.Description,
                                  Image = p.Image,
                                  Price = p.Price,
                                  Quantity = p.Quantity,
                                  Category_id = p.Category_id,
                              };
                var totalCount = product.Count();
                var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);
                var skip = pageNumber * pageSize - pageSize;
                var dl = new ViewModel()
                {
                    TotalCount = totalCount,
                    Productes = product.Skip(skip).Take(pageSize).ToList(),
                    PageCount = pageCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                };
                return View(dl);
            }       
        }
        public IActionResult Detail(int id)
        {
            var query = from c in _dbContext.CategoryEnities
                        where c.IsDeleted == false
                        select new Category()
                        {
                            Id = c.Id,
                            Name = c.Name

                        };
            ViewBag.lstcategory = query;
            if (id != 0)
            {

                var detail = _dbContext.ProductEntities.Find(id);
                var db = new ViewModel()
                {
                    Categories = query.ToList(),
                    imagee = detail.Image,
                    Product = new Product()
                    {
                        Id = detail.Id,
                        Name = detail.Name,
                        Author = detail.Author,
                        Slug = detail.Slug,
                        Description = detail.Description,
                        Price = detail.Price,
                        Quantity = detail.Quantity,
                        Category_id = detail.Category_id
                    }
                };
                var querylst = from p in _dbContext.ProductEntities
                               where p.IsDeleted == false && p.Category_id == detail.Category_id
                               select new {
                                   id = p.Id,
                                   name = p.Name,
                                   image = p.Image,
                                   price = p.Price,
                                   category_Id = p.Category_id,
                                    };
                ViewBag.lstproduct= querylst;

                return View(db);
            }
            else
            {
                return Redirect("/home/index");
            }
        }

    }
}
