using DOAN.Extension;
using DOAN.Models;
using Microsoft.AspNetCore.Mvc;

namespace DOAN.Controllers.Components
{
    public class NumberCartViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var cart = HttpContext.Session.Get<List<Cartitem>>("GioHang");
            int soluongsanpham = 0;
            if(cart != null)
            {
                soluongsanpham=cart.Count();
            }
           
            return View(soluongsanpham);

        }
    }
}
