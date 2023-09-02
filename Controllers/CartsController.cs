using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using DOAN.Models;
using Newtonsoft.Json;
using DOAN.Entities;
using DOAN.Extension;
using Microsoft.AspNetCore.Authorization;

namespace DOAN.Controllers
{
    public class CartsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _dbContext;

        public CartsController(ILogger<HomeController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public List<Cartitem> GioHang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<Cartitem>>("GioHang");
                if (gh == null)
                {
                    gh = new List<Cartitem>();
                }
                return gh;
            }
 
        }
        [HttpPost]
        public IActionResult AddToCart(int id, int? amount)
        {
            List<Cartitem> cart = GioHang;
            try
            {
                Cartitem cartitem = cart.SingleOrDefault(p => p.Product.Id == id);
                if (cartitem != null)
                {
                    cartitem.amount = cartitem.amount + amount.Value;

                }
                else
                {
                    ProductEntity hh = _dbContext.ProductEntities.SingleOrDefault(p => p.Id == id);
                    cartitem = new Cartitem
                    {
                        amount = amount.HasValue ? amount.Value : 1,
                        Product = hh
                    };
                    cart.Add(cartitem);
                }
                HttpContext.Session.Set<List<Cartitem>>("GioHang", cart);
                return Json(new {success=true});
            }
            catch (Exception ex)
            {
                return Json(new { success=false });
            }
        }
        [HttpPost]
        public IActionResult UpdateCart(int id, int? amount)
        {
            var cart = HttpContext.Session.Get<List<Cartitem>>("GioHang");                       
            try
            {
                if (cart != null)
                {
                    Cartitem item = cart.SingleOrDefault(p => p.Product.Id == id);
                    if (item != null && amount.HasValue)
                    {
                        item.amount = amount.Value;
                    }
                    HttpContext.Session.Set<List<Cartitem>>("GioHang", cart);
                }
                return Json(new { success = true });
            }
            catch { return Json(new { success = false }); }
        }
        [HttpPost]
        public IActionResult Remove(int id)
        {
            try
            {
                List<Cartitem> giohang = GioHang;
                Cartitem item = giohang.SingleOrDefault(p => p.Product.Id == id);
                if (item != null)
                {
                    giohang.Remove(item);
                }
                HttpContext.Session.Set<List<Cartitem>>("GioHang", giohang);
                return Json(new { success = true });
            }
            catch {
                return Json(new { success = false });
            }
        }

        public IActionResult Index()
        {

            return View(GioHang);
        }

    }
}
