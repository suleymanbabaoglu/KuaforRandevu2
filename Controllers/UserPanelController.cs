using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KuaforRandevu2.Models.Entities;
using KuaforRandevu2.Models.Repositories.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KuaforRandevu2.Controllers
{
    [Authorize(Roles = "user,admin", AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class UserPanelController : Controller
    {
        private readonly IRepository<User> userRepo;
        private readonly IRepository<Appointment> appRepo;
        private readonly IRepository<Expert> expertRepo;
        public UserPanelController(IRepository<User> userRepo, IRepository<Appointment> appRepo, IRepository<Expert> expertRepo)
        {
            this.userRepo = userRepo;
            this.appRepo = appRepo;
            this.expertRepo = expertRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PersonalSettings()
        {
            var user = userRepo.Get(u => u.UserName == HttpContext.User.Identity.Name);
            return View(user);
        }
        [HttpPost]
        public IActionResult PersonalSettings(User postUser)
        {
            return View();
        }

        public IActionResult AppointmentManagement()
        {
            var user = userRepo.Get(u => u.UserName == HttpContext.User.Identity.Name);
            var app = appRepo.Table.Include(a=>a.Expert).Include(a=>a.Expert.User).Where(a=>a.UserId==user.Id).FirstOrDefault();
            
            return View(app);
        }

        [HttpPost]
        public IActionResult AppointmentManagement(Appointment postAppointment)
        {
            var user = userRepo.Get(u => u.UserName == HttpContext.User.Identity.Name);
            return View();
        }
    }
}