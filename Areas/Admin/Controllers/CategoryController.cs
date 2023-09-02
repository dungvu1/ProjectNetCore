using DOAN.Entities;
using DOAN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Printing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DOAN.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _environment;
        public CategoryController(AppDbContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }
        public IActionResult Index()
        {
            
            
            return View();
        }
        [HttpGet]
        public IActionResult Getlist(string tuKhoa, int pageNumber = 1, int pageSize = 5)//, int pageNumber = 1, int pageSize = 10
        {
            var GG = _dbContext.CategoryEnities.Where(x => x.IsDeleted == false).ToList();
            if (!String.IsNullOrEmpty(tuKhoa))
            {
                tuKhoa = tuKhoa.ToLower();
                GG = GG.Where(x=>x.Name.ToLower().Contains(tuKhoa)).ToList();
 
            }
            int totalItems = GG.Count();
            var paginatedData = GG.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return Json(new { db = paginatedData, totalItems });
        }
        public IActionResult Add() { return View(); }
        public IActionResult Edit(int id)
        {
            var entiti = _dbContext.CategoryEnities.Find(id);
            var edit = new Category()
            {
                Id = entiti.Id,
                Name = entiti.Name,
                Slug = entiti.Slug,
                Description = entiti.Description,
            };

            return View(edit);
        }

        [HttpPost]
        public IActionResult AddCategory(Category model)
        {
            if (model.Image != null && model.Image.Length > 0)
            {

                var fullPath = Path.Combine(_environment.WebRootPath, "imagecategory", model.Image.FileName);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
                var qu = new CategoryEnity()
                {
                    Name = model.Name,
                    Slug = model.Slug,
                    Description = model.Description,
                    Image = model.Image.FileName,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsActive = true

                };
                _dbContext.CategoryEnities.Add(qu);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
           return Json(new { error = true });
        }
        [HttpPost]
        public IActionResult EditCategory(Category model)
        {
            var query = _dbContext.CategoryEnities.Find(model.Id);
            if (model.Image != null && model.Image.Length > 0)
            {

                var fullPath = Path.Combine(_environment.WebRootPath, "imagecategory", model.Image.FileName);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
                    query.Name = model.Name;
                    query.Slug = model.Slug;
                    query.Description = model.Description;
                    query.Image = model.Image.FileName;
                    query.CreatedDate = DateTime.Now;
                    query.UpdatedDate = DateTime.Now;
                    query.IsActive = true;


                _dbContext.CategoryEnities.Update(query);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
           return Json(new {error = true});
            
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {

            var query = _dbContext.CategoryEnities.Find(id);
            query.IsDeleted = true;
            _dbContext.Update(query);
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
