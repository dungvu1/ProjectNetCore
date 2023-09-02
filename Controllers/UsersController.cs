using DOAN.Entities;
using DOAN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DOAN.Controllers
{
    
    public class UsersController : Controller
    {
        private readonly AppDbContext _dbContext;

        public UsersController( AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(int id)
        {
          
            var cart = _dbContext.OrdersEntities.Where(x=>x.UsersId == id).ToList();
            var statu = _dbContext.StatusEntities.ToList();
            ViewBag.status = statu;
            ViewBag.Cart=cart;
            var acc = _dbContext.AccountsEntities.Find(id);
            var accuser = new Accounts()
            {
                AccountId = id,
                FullName = acc.FullName,
                Email = acc.Email,
                Phone = acc.Phone,
            };
            return View(acc);
        }
    }
}
