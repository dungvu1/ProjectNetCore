using DOAN.Entities;
using DOAN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOAN.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _environment;
        public ProductController(AppDbContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var query = from c in _dbContext.CategoryEnities
                        where (c.IsDeleted == false)
                        select new
                        {
                            Id = c.Id,
                            Name = c.Name,
                        };
            return View(query.ToList());
        }
       
        [HttpGet]
        public IActionResult GetList(string tuKhoa, int pageNumber = 1, int pageSize = 6)
        {
            var query = (from p in _dbContext.ProductEntities
                        join c in _dbContext.CategoryEnities
                        on p.Category_id equals c.Id
                        where(p.IsDeleted==false)
                        select new
                        {   
                            id=p.Id,
                            name = p.Name,
                            author=p.Author,
                            slug=p.Slug,
                            desciption=p.Description,
                            image=p.Image,
                            price=p.Price,
                            quantity=p.Quantity,
                            category_Id=c.Id,
                            categoryName = c.Name,
                            createdDate=p.CreatedDate,
                            createdBy=p.CreatedBy,
                            isActive=p.IsActive,
                            isDeleted=p.IsDeleted
                        }).ToList();
            if (!String.IsNullOrEmpty(tuKhoa))
            {
                tuKhoa = tuKhoa.ToLower();
                query = query.Where(x => x.name.ToLower().Contains(tuKhoa)||x.categoryName.ToLower().Contains(tuKhoa)).ToList();

            }
            int totalItems = query.Count();
            var paginatedData = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return Json(new {db=paginatedData,dl= totalItems});
        }
        public IActionResult AddProduct()
        {
            var query = from c in _dbContext.CategoryEnities
                        where (c.IsDeleted == false)
                        select new
                        {
                            Id = c.Id,
                            Name = c.Name,
                        };
            return View(query.ToList());
        }
        public IActionResult EditProduct(int id)
        {  
            var query = _dbContext.ProductEntities.Find(id);
            var cate = from c in _dbContext.CategoryEnities
                       where c.IsDeleted==false
                       select new Category ()
                       {
                           Id=c.Id,
                           Name=c.Name,
                       };
            var db = new ViewModel()
            {
                Categories = cate.ToList(),
                Product = new Product()
                {
                    Id = query.Id,
                    Name = query.Name,
                    Author = query.Author,
                    Slug = query.Slug,
                    Price = query.Price,
                    Quantity = query.Quantity,
                    Category_id = query.Category_id
                }
            };
            return View(db);
        }
        [HttpPost]
        public IActionResult Add(Product model)
        {
            if (model.Image != null && model.Image.Length > 0)
            {
                var fullPath = Path.Combine(_environment.WebRootPath, "imageproduct", model.Image.FileName);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
                var query = new ProductEntity()
                {
                    Name=model.Name,
                    Author=model.Author,
                    Slug=model.Slug,
                    Category_id=model.Category_id,
                    Image= model.Image.FileName,
                    Price=model.Price,
                    Quantity=model.Quantity,
                    CreatedDate=DateTime.Now,
                    Description=model.Description,
                    UpdatedDate=DateTime.Now,
                    IsActive=true,
                    IsDeleted=false

                };
                _dbContext.ProductEntities.Add(query);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            
            return Json(new {error=true});
        }
        [HttpPost]
        public IActionResult edit(Product model)
        {
            var query = _dbContext.ProductEntities.Find(model.Id);

            if (model.Image != null && model.Image.Length > 0)
            {
                var fullPath = Path.Combine(_environment.WebRootPath, "imageproduct", model.Image.FileName);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
                query.Name = model.Name;
                query.Author = model.Author;
                query.Slug = model.Slug;
                query.Category_id = model.Category_id;
                query.Image = model.Image.FileName;
                query.Price = model.Price;
                query.Quantity = model.Quantity;
                query.CreatedDate = DateTime.Now;
                query.Description = model.Description;
                query.UpdatedDate = DateTime.Now;
                query.IsActive = true;

                _dbContext.ProductEntities.Update(query);
                _dbContext.SaveChanges();

                return Json(new { success = true });

            }
            return Json(new { error = true });
        }
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            var query=_dbContext.ProductEntities.Find(id);
            query.IsDeleted = true;
            _dbContext.ProductEntities.Update(query);
            _dbContext.SaveChanges();
            return Json(new { success = true });
        }
        public class FileUpload
        {
            public string FileName { get; set; }

            public string FilePath { get; set; }

        }
    }

}
