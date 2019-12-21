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
    [Authorize(Roles = "admin", AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AdminPanelController : Controller
    {
        private readonly IRepository<User> userRepo;
        private readonly IRepository<Appointment> appRepo;
        private readonly IRepository<Expert> expertRepo;
        private readonly IRepository<About> aboutRepo;
        private readonly IRepository<Contact> contactRepo;
        private readonly IRepository<ContactForm> cformRepo;
        private readonly IRepository<Role> roleRepo;
        private readonly IRepository<Gallery> galleryRepo;
        public AdminPanelController(IRepository<User> userRepo, IRepository<Appointment> appRepo, IRepository<Expert> expertRepo, IRepository<Contact> contactRepo, IRepository<About> aboutRepo, IRepository<ContactForm> cformRepo, IRepository<Role> roleRepo, IRepository<Gallery> galleryRepo)
        {
            this.userRepo = userRepo;
            this.roleRepo = roleRepo;
            this.appRepo = appRepo;
            this.cformRepo = cformRepo;
            this.aboutRepo = aboutRepo;
            this.expertRepo = expertRepo;
            this.contactRepo = contactRepo;
            this.galleryRepo = galleryRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserManagement()
        {
            return View(userRepo.Table.Include(u => u.Role));
        }

        public IActionResult UserAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserAdd(User user, string selectedGender, int selectedRole)
        {
            user.RoleId = selectedRole;
            user.Gender = selectedGender;
            userRepo.Add(user);
            userRepo.Save();
            return RedirectToAction("UserManagement");
        }

        public IActionResult UserUpdate(int id)
        {
            return View(userRepo.Table.Where(u => u.Id == id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult UserUpdate(User user, string selectedGender, int selectedRole)
        {
            user.RoleId = selectedRole;
            user.Gender = selectedGender;
            userRepo.Update(user);
            userRepo.Save();
            return RedirectToAction("UserManagement");
        }


        public IActionResult UserDelete(int id)
        {
            var user = userRepo.Table.Where(u => u.Id == id).FirstOrDefault();
            userRepo.Delete(user);
            userRepo.Save();
            return RedirectToAction("UserManagement");
        }

        public IActionResult AppointmentManagement()
        {
            return View(appRepo.Get());
        }

        public IActionResult AppointmentUpdate(int id)
        {
            return View(appRepo.Get().Where(u => u.Id == id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult AppointmentUpdate(Appointment appointment)
        {
            appRepo.Update(appointment);
            appRepo.Save();
            return RedirectToAction("AppointmentManagement");
        }

        public IActionResult AppointmentDelete(int id)
        {
            var app = appRepo.Table.Where(u => u.Id == id).FirstOrDefault();
            appRepo.Delete(app);
            appRepo.Save();
            return RedirectToAction("AppointmentManagement");
        }

        public IActionResult ContactFormManagement()
        {
            return View(cformRepo.Get());
        }

        public IActionResult ContactFormDelete(int id)
        {
            var cform = cformRepo.Table.Where(u => u.Id == id).FirstOrDefault();
            cformRepo.Delete(cform);
            cformRepo.Save();
            return RedirectToAction("ContactFormManagement");
        }

        public IActionResult ExpertsManagement()
        {
            return View(expertRepo.Get());
        }

        public IActionResult ExpertAdd()
        {
            var users = userRepo.Get();
            ViewBag.Users = users;
            return View();
        }

        [HttpPost]
        public IActionResult ExpertAdd(Expert expert, int selectedUser)
        {
            expert.UserId = selectedUser;
            expertRepo.Add(expert);
            expertRepo.Save();
            return RedirectToAction("ExpertsManagement");
        }

        public IActionResult ExpertUpdate(int id)
        {
            return View(expertRepo.Table.Where(u => u.Id == id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult ExpertUpdate(Expert expert, int selectedUser)
        {

            expert.UserId = selectedUser;
            expertRepo.Update(expert);
            expertRepo.Save();
            return RedirectToAction("ExpertsManagement");
        }


        public IActionResult ExpertDelete(int id)
        {
            var expert = expertRepo.Table.Where(u => u.Id == id).FirstOrDefault();
            expertRepo.Delete(expert);
            expertRepo.Save();
            return RedirectToAction("ExpertsManagement");
        }

        public IActionResult AboutUsManagement()
        {
            return View(aboutRepo.Get().FirstOrDefault());
        }

        [HttpPost]
        public IActionResult AboutUsManagement(About about)
        {
            aboutRepo.Update(about);
            aboutRepo.Save();
            return RedirectToAction("AboutUsManagement");
        }

        public IActionResult ContactManagement()
        {
            return View(contactRepo.Get().FirstOrDefault());
        }

        [HttpPost]
        public IActionResult ContactManagement(Contact contact)
        {
            contactRepo.Update(contact);
            contactRepo.Save();
            return RedirectToAction("ContactManagement");
        }



        public IActionResult GalleryManagement()
        {
            return View(galleryRepo.Get());
        }


    }
}