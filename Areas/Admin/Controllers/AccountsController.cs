using DOAN.Entities;
using DOAN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;


namespace DOAN.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class AccountsController : Controller
    {   
        private readonly AppDbContext _dbContext;
        public AccountsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            //ViewData["QuyenTruyCap"] = new SelectList(_dbContext.RolesEntities,"RoleId","RoleName");
            List<SelectListItem> IsTrangThai = new List<SelectListItem>();
            IsTrangThai.Add(new SelectListItem() { Text = "Active", Value = "1" });
            IsTrangThai.Add(new SelectListItem() { Text = "Block", Value = "0" });
            ViewData["TrangThai"] = IsTrangThai;
            var query = _dbContext.RolesEntities.Where(x => x.IsDeleted == false).ToList();
            return View(query);
        }
        public IActionResult EdiAccounts(int id)
        {
            var query = _dbContext.AccountsEntities.Find(id);
            var role = _dbContext.RolesEntities.Where(x =>x.IsDeleted == false).ToList();
            ViewBag.role = role;
           var lst = new Accounts()
            {   
               AccountId = id,
                FullName = query.FullName,
                RoleId = query.RoleId,
                Email = query.Email
               
            };
            return View(lst) ;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var query = (from a in _dbContext.AccountsEntities
                         join r in _dbContext.RolesEntities
                         on a.RoleId equals r.RoleId
                         where (a.IsDelete == false)
                         select new 
                         {
                             AccountId = a.AccountId,
                             FullName = a.FullName,
                             Phone = a.Phone,
                             Email = a.Email,
                             RoleName = r.RoleName,
                             RoleId = a.RoleId,
                             IsActive = a.IsActive,
                             Password = a.Password,
                             CreatedDate = a.CreatedDate,
                             UpDatedDate = a.UpDatedDate,
                             IsDelete = a.IsDelete
                         }).ToList();
            return Json(new { db = query });
        }
    }
}
