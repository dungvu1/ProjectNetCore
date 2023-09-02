using DOAN.Entities;
using DOAN.Extension;
using DOAN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOAN.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class CheckOutController : Controller
    {
        private readonly AppDbContext _dbContext;
        public CheckOutController( AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<List<Cartitem>>("GioHang");
            var query = _dbContext.PayEntities.ToList();
            ViewBag.Pay = query;
            return View(cart);
        }
        [HttpPost]
        public IActionResult AddOrder(Order model)
        {          
            try
            {
                var cart = HttpContext.Session.Get<List<Cartitem>>("GioHang");
                if (model.FullName == null ||model.Address == null || model.Phone==null|| model.Email==null || model.Note==null|| MD5WEB.IsGmail(model.Email) == false||model.Paymentid ==0) 
                {
                    return Json(new { err = true });
                }
                else
                {
                    var query = new OrdersEntity()
                    {
                        FullName = model.FullName,
                        Address = model.Address,
                        Phone = model.Phone,
                        Email = model.Email,
                        Note = model.Note,
                        Paymentid = model.Paymentid,
                        UsersId = model.UsersId,
                        StatusId = 1,
                        OrderDate = DateTime.Now,
                        IsDeleted = false,
                        ToTalOrder = Convert.ToInt32(cart.Sum(x => x.TotalMoney))
                    };
                    _dbContext.OrdersEntities.Add(query);
                    foreach (var item in cart)
                    {
                        var odertai = new OrderDetailsEntity()
                        {
                            OrderId = query.OrderId,
                            ProductId = item.Product.Id,
                            Quantity = item.amount,
                            price = item.TotalMoney,
                            ToTal = query.ToTalOrder,
                            CreateOrderdetai = DateTime.Now
                        };
                        _dbContext.OrderDetailsEntities.Add(odertai);
                    }
                    _dbContext.SaveChanges();
                    HttpContext.Session.Remove("GioHang");
                    return Json(new { success = true });
                }

            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
    }
}
