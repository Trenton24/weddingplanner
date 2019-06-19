using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            HttpContext.Session.SetString("log", "");
            List<Users> AllUsers = dbContext.users.ToList();
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(Users user)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Index");
                }
            
                PasswordHasher<Users> Hasher = new PasswordHasher<Users>();
                user.Password = Hasher.HashPassword(user, user.Password);
                dbContext.Add(user);
                dbContext.SaveChanges();
                return Redirect("signin");
            }
            return View("Index");
        }

        [HttpGet]
        [Route("/signin")]
        public IActionResult signin()
        {
            return View("login");
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginUser user)
        {
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.users.FirstOrDefault(u => u.Email == user.Email);
                if(userInDb == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("login");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(user, userInDb.Password, user.Password);
                if(result == 0)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("login");
                }
                HttpContext.Session.SetString("log", user.Email);
                return Redirect("result");
            }
            return View("login");
        }
    }
}
