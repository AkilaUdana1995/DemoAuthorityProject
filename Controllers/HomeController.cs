using DemoAuthorityProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DemoAuthorityProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

       [Authorize(Roles ="Admin")]
     //[Authorize]
        public async Task<IActionResult> Secured()
        {
            var idToken = await HttpContext.GetTokenAsync("id_token");  //this token is the type of JWT or JOT
            return View();
        }

        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }

       // [Authorize]
        [HttpGet("login")]
        public IActionResult login(string returnUrl)
        {
             ViewData["ReturnUrl"] = returnUrl;
               return View();
        
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> validate(string username, string password, string returnUrl)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    if (username == "AAA" && password == "123")
        //    {
              
        //            var claims = new List<Claim>();
        //            claims.Add(new Claim("username", username));
        //            claims.Add(new Claim(ClaimTypes.NameIdentifier, username));  //simply we can add any name here...
        //           claims.Add(new Claim(ClaimTypes.Name, username));
        //            var ClaimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //            var ClaimsPrincipal = new ClaimsPrincipal(ClaimsIdentity);
        //            await HttpContext.SignInAsync(ClaimsPrincipal);
        //            return Redirect(returnUrl);
                
              
        //        // return View("Secured");
        //    }

        //    TempData["Error"] = "Error. username and/or Password is incorrect";
        //    return View("login");// BadRequest();

        //}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
            // return View("login");

           // return RedirectToAction(@"https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://localhost:44346/");

        }
    }
}
