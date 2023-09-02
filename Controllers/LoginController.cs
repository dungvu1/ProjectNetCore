using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DOAN.Entities;
using DOAN.Models;

namespace DOAN.Controllers
{
    public class LoginController : Controller
    {

        private readonly AppDbContext _dbContext;

        public LoginController(AppDbContext dbContext)
        {

            _dbContext = dbContext;
        }
        public IActionResult Index(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddRegister(Accounts model)
        {
            if (model.FullName !=null && model.Email !=null&& model.Password!=null&& model.Phone !=null)
            {    
                var user = _dbContext.AccountsEntities.FirstOrDefault(x =>x.Phone == model.Phone && x.Email==model.Email);
                if(user == null)
                {

                    var query = new AccountsEntity()
                    {
                        FullName = model.FullName, 
                        Email = model.Email,
                        Phone = model.Phone,
                        Password = MD5WEB.MD5Hash(model.Password),
                        IsActive=true,
                        RoleId=3,
                        CreatedDate= DateTime.Now,
                        UpDatedDate= DateTime.Now,
                    };
                    _dbContext.AccountsEntities.Add(query);
                    _dbContext.SaveChanges();
                    return Json(new { succes = true });
                }
                else if (user != null)
                {
                    TempData["error"] = " Tai khoan da co nguoi su dung ";
                    return Redirect("/login/register");
                }
            }
                return Json(new { error = true });
        }
        public async Task< IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
          CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/login/index");
        }
        [HttpPost]
        public async Task<IActionResult> Logini(Accounts model)
        {
            if (model.Email == null&& model.Password ==null)
            {
                TempData["err"] = "Vui long dien day ";
                return Redirect("/login/index");
            }
      
            var passwordMd5 = MD5WEB.MD5Hash(model.Password);
            var user = _dbContext.AccountsEntities.FirstOrDefault(x => x.Email == model.Email && x.Password == passwordMd5);
            if (user == null)
            {
                TempData["error"] = " Tai khoan mat khaau khong chinh sac";
                return Redirect("/login/index");
            }

            else if(user != null)
            {
                var claims = new List<Claim>()
                    {
                new Claim(ClaimTypes.Name,user.FullName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.AccountId.ToString())
                    };
                if (user.RoleId == 1)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                   
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    var returnUrl = model.Url == null ? "/admin/homeadmin" : model.Url;
                    return Redirect(returnUrl);
                }
                else if(user.RoleId == 3)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "User"));
                    
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                    var returnUrl = model.Url == null ? "/" : model.Url;

                    return Redirect(returnUrl);
                }
            }


            return Ok();
        }

       
    }
}
