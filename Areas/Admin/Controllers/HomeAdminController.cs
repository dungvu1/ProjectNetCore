using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOAN.Areas.Admin.Controllers
{   

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeAdminController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
