using DOAN.Entities;
using DOAN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DOAN.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger,AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            
            var listProDuct = from p in _dbContext.ProductEntities
                              where p.IsDeleted == false
                              orderby p.Price descending
                              select new 
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  Price = p.Price,
                                  Image =p.Image                             
                              };

            return View(listProDuct);
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetList(string tuKhoa, int pageNumber = 1, int pageSize = 8)
        {
            var query = (from p in _dbContext.ProductEntities
                         join c in _dbContext.CategoryEnities
                         on p.Category_id equals c.Id
                         where (p.IsDeleted == false)
                         select new
                         {
                             id = p.Id,
                             name = p.Name,
                             image = p.Image,
                             price = p.Price,
                             quantity = p.Quantity,
                             category_Id = c.Id,
                             categoryName = c.Name,
                             isDeleted = p.IsDeleted
                         }).ToList();
            if (!String.IsNullOrEmpty(tuKhoa))
            {
                tuKhoa = tuKhoa.ToLower();
                query = query.Where(x => x.name.ToLower().Contains(tuKhoa) || x.categoryName.ToLower().Contains(tuKhoa)).ToList();

            }
            int totalItems = query.Count();
            var paginatedData = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return Json(new { db = paginatedData, dl = totalItems });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}