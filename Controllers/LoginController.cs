using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KuaforRandevu2.Models.Entities;
using KuaforRandevu2.Models.Repositories.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KuaforRandevu2.Controllers
{
    [Authorize(Roles = "admin,user", AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class LoginController : Controller
    {
        private readonly IRepository<User> userRepo;
        private readonly IRepository<Role> roleRepo;
        public LoginController(IRepository<User> userRepo)
        {
            this.userRepo = userRepo;            
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("admin"))
            {
                return Redirect("~/AdminPanel");
            }
            else if (HttpContext.User.Identity.IsAuthenticated && HttpContext.User.IsInRole("user"))
            {
                return Redirect("~/UserPanel");
            }
            else
                return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(User user)
        {
            var data = userRepo.Table.Include(s => s.Role).Where(s => s.UserName == user.UserName && s.Password == user.Password).FirstOrDefault();

            if (data == null)
                return RedirectToAction("Index");

            if (data.RoleId == 1)
            {
                SignInAsync(data);
                return Redirect("~/AdminPanel");
            }
            else if (data.RoleId == 2)
            {
                SignInAsync(data);
                return Redirect("~/UserPanel");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task SignInAsync(User user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Role, user.Role.RoleName));

            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}