using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KuaforRandevu2.Models.Entities;
using KuaforRandevu2.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var data = appRepo.Table.Include(u => u.User).Include(u => u.Expert.User);
            return View(data);
        }

        public IActionResult AppointmentUpdate(int id)
        {
            var app = appRepo.Table.Include(a => a.Expert).Include(a => a.Expert.User).Where(a => a.Id == id).FirstOrDefault();            
            ViewBag.user = userRepo.Table.Where(a => a.Id == app.UserId).FirstOrDefault();
            return View(app);
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

        [HttpPost, AllowAnonymous]
        public IActionResult ContactFormAdd(ContactForm contactForm)
        {
            cformRepo.Add(contactForm);
            cformRepo.Save();
            return Redirect("~/FrontSide/Contact");
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
            return View(expertRepo.Table.Include(m => m.User));
        }

        public IActionResult ExpertAdd()
        {
            var ignoredId = expertRepo.Get().Select(i => i.UserId).ToArray();
            var users = (IEnumerable<SelectListItem>)userRepo.Table.Where(s => !ignoredId.Contains(s.Id)).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.FullName
            });

            ViewBag.Users = new SelectList(users, "UserId", "FullName");
            return View();
        }

        [HttpPost]
        public IActionResult ExpertAdd(Expert expert)
        {
            expertRepo.Add(expert);
            expertRepo.Save();
            return RedirectToAction("ExpertsManagement");
        }

        public IActionResult ExpertUpdate(int id)
        {
            var expert = expertRepo.Table.Where(u => u.Id == id).FirstOrDefault();
            var user = userRepo.Table.Where(u => u.Id == expert.UserId).FirstOrDefault();
            ViewBag.userFullName = user.FullName;
            return View(expert);
        }

        [HttpPost]
        public IActionResult ExpertUpdate(Expert expert)
        {
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
            return View(galleryRepo.Table.Include(u => u.ImgUser));
        }

        public IActionResult GalleryAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GalleryAdd(Gallery gallery, IFormFile galleryImage)
        {
            var userId = userRepo.Table.Where(m => m.UserName == HttpContext.User.Identity.Name).Select(s => s.Id).FirstOrDefault();
            gallery.ImgDate = DateTime.Now;
            gallery.ImgUserId = userId;

            Random r = new Random();
            FileInfo fi;
            if (galleryImage != null)
            {
                var fileName = galleryImage.FileName;
                fi = new FileInfo(fileName);
                string imgPath = "img-" + gallery.ImgDate.ToString("dd-MM-yyyy-HH-mm") + "-" + r.Next(1000, 10000).ToString() + fi.Extension;

                gallery.ImgPath = imgPath;


                // If file with same name exists delete it
                if (System.IO.File.Exists(imgPath))
                {
                    System.IO.File.Delete(imgPath);
                }

                // Create new local file and copy contents of uploaded file
                using (var localFile = System.IO.File.OpenWrite("wwwroot/img/" + imgPath))
                using (var uploadedFile = galleryImage.OpenReadStream())
                {
                    uploadedFile.CopyTo(localFile);
                    galleryRepo.Add(gallery);
                    galleryRepo.Save();
                }
            }

            return RedirectToAction("GalleryManagement");
        }

        public IActionResult GalleryUpdate(int id)
        {
            var gallery = galleryRepo.Table.Where(m => m.Id == id).FirstOrDefault();
            var user = userRepo.Table.Where(u => u.Id == gallery.ImgUserId).FirstOrDefault();
            ViewBag.userFullName = user.FullName;
            return View(gallery);
        }

        [HttpPost]
        public IActionResult GalleryUpdate(Gallery gallery)
        {
            galleryRepo.Update(gallery);
            galleryRepo.Save();
            return RedirectToAction("GalleryManagement");
        }

        public IActionResult GalleryDelete(int id)
        {
            var gallery = galleryRepo.Table.Where(m => m.Id == id).FirstOrDefault();
            galleryRepo.Delete(gallery);
            galleryRepo.Save();
            return RedirectToAction("GalleryManagement");
        }

    }
}