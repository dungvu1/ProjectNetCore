using DOAN.Entities;
using DOAN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DOAN.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _environment;
        public RoleController(AppDbContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }
        public IActionResult Index()
        {   
           

            return View();
        }
        public IActionResult GetList()
        {
            var query = _dbContext.RolesEntities.Where(x=>x.IsDeleted==false).ToList();
            return Json(new { db = query });
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {   
            var query = _dbContext.RolesEntities.Find(id);
            var db = new Roles()
            {
                RoleId = query.RoleId,
                RoleName = query.RoleName,
                Description = query.Description
            };
            return View(db);
        }
        [HttpPost]
        public IActionResult AddRole(Roles model)
        {
            if(model.RoleName != null && model.Description != null)
            {
                var query = new RolesEntity()
                {
                    RoleName = model.RoleName,
                    Description = model.Description
                };
                _dbContext.RolesEntities.Add(query);
                _dbContext.SaveChanges();
                return Json(new {success =true});
            }
            return Json(new { error = true });
        }
        [HttpPost]
        public IActionResult EditRole(Roles model)
        {
            
            if (model.RoleName != null && model.Description != null)
            {
                var query = _dbContext.RolesEntities.Find(model.RoleId);
                query.RoleName = model.RoleName;
                query.Description = model.Description;
                _dbContext.RolesEntities.Update(query);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { error =true });
            
        }
        [HttpPost]
        public IActionResult DeleteRole(int id)
        {
            var query = _dbContext.RolesEntities.Find(id);
            query.IsDeleted=true;
            _dbContext.RolesEntities.Update(query);
            _dbContext.SaveChanges();
            return Json(new { success = true });
        }
         
    }
}
