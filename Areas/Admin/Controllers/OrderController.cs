using DOAN.Entities;
using DOAN.Models;
using Microsoft.AspNetCore.Mvc;

namespace DOAN.Areas.Admin.Controllers
{
    [Area("admin")]
    public class OrderController : Controller
    {

        private readonly AppDbContext _dbContext;
        public OrderController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var query = from o in _dbContext.OrdersEntities
                        join s in _dbContext.StatusEntities
                        on o.StatusId equals s.StatusId
                        where o.IsDeleted == false
                        select new Order()
                        {
                            OrderId = o.OrderId,
                            Address = o.Address,
                            FullName = o.FullName,
                            Phone = o.Phone,
                            Email = o.Email,
                            UsersId = o.UsersId,
                            OrderDate = o.OrderDate,
                            StatusName = s.StatusName,
                            Paymentid = o.Paymentid,
                            StatusId = o.StatusId,
                            Note = o.Note,
                            ToTalOrder = o.ToTalOrder,
                            IsDeleted = o.IsDeleted
                        };
            var pay = _dbContext.PayEntities.ToList();
            ViewBag.Pay = pay;
            return View(query.ToList());
        }
    }
}
