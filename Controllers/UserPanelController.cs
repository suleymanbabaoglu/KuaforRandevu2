using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KuaforRandevu2.Models.Entities;
using KuaforRandevu2.Models.Repositories.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            userRepo.Update(postUser);
            userRepo.Save();
            return RedirectToAction("PersonalSettings");
        }

        public IActionResult AppointmentManagement()
        {
            var user = userRepo.Get(u => u.UserName == HttpContext.User.Identity.Name);
            var app = appRepo.Table.Include(a => a.Expert).Include(a => a.Expert.User).Where(a => a.UserId == user.Id);

            return View(app);
        }

        public IActionResult SetAppointment()
        {
            var experts = expertRepo.Table.Include(s => s.User).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.User.FullName
            }); ;
            ViewBag.Users = new SelectList(experts, "Id", "User.FullName");
            return View();
        }

        [HttpPost]
        public IActionResult SetAppointment(Appointment appointment)
        {
            appointment.UserId = userRepo.Table.Where(m => m.UserName == HttpContext.User.Identity.Name).Select(s => s.Id).FirstOrDefault();
            if (DateTime.Now.AddMinutes(60.0) < appointment.AppointmentDate)
            {
                appRepo.Add(appointment);
                appRepo.Save();
                return RedirectToAction("AppointmentManagement");
            }
            else
            {
                return RedirectToAction("SetAppointment");
            }
        }

        public IActionResult UpdateAppointment(int id)
        {
            var app = appRepo.Table.Include(a => a.Expert).Include(a => a.Expert.User).Where(a => a.Id == id).FirstOrDefault();
            ViewBag.user = userRepo.Table.Where(a => a.Id == app.UserId).FirstOrDefault();
            return View(app);
        }

        [HttpPost]
        public IActionResult UpdateAppointment(Appointment appointment, DateTime existDate)
        {
            if ((existDate - DateTime.Now).TotalMinutes > 60.0 && existDate > DateTime.Now)
            {
                appRepo.Update(appointment);
                appRepo.Save();
                return RedirectToAction("AppointmentManagement");
            }
            else
            {
                return RedirectToAction("UpdateAppointment");
            }


        }

        public IActionResult DeleteAppointment(int id)
        {
            appRepo.Delete(appRepo.Table.Where(m => m.Id == id).FirstOrDefault());
            appRepo.Save();
            return RedirectToAction("AppointmentManagement");
        }
    }
}