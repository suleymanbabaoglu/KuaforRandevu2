using KuaforRandevu2.Models.Entities;
using KuaforRandevu2.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KuaforRandevu2.Controllers
{
    [AllowAnonymous]
    [Authorize(Roles = "user,admin", AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class FrontSideController : Controller
    {
        private readonly IRepository<About> aboutRepository;
        private readonly IRepository<Contact> contactRepository;
        private readonly IRepository<Expert> expertRepository;
        private readonly IRepository<Gallery> galleryRepository;

        public FrontSideController(IRepository<About> aboutRepository, IRepository<Contact> contactRepository, IRepository<Expert> expertRepository, IRepository<Gallery> galleryRepository)
        {
            this.aboutRepository = aboutRepository;
            this.contactRepository = contactRepository;
            this.expertRepository = expertRepository;
            this.galleryRepository = galleryRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View(aboutRepository.Get().FirstOrDefault());
        }

        public IActionResult Experts()
        {
            return View(expertRepository.Table.Include(e=>e.User));
        }

        public IActionResult Gallery()
        {
            return View(galleryRepository.Get());
        }

        public IActionResult Contact()
        {
            ViewBag.Contact = contactRepository.Get().FirstOrDefault();
            return View();
        }
       
    }
}
