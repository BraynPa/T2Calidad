using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CalidadT2.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using CalidadT2.Repositorios;
using CalidadT2.Service;

namespace CalidadT2.Controllers
{
    public class AuthController : Controller
    {
        private ICookieAuthService cookieAuthService;
        private IUsuarioRepositorio repositorio;

        public AuthController(IUsuarioRepositorio repositorio, ICookieAuthService cookieAuthService)
        {
            
            this.repositorio = repositorio;
            this.cookieAuthService = cookieAuthService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var usuario = repositorio.FindUser(username, password);
            if (usuario != null)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);


                cookieAuthService.SetHttpContext(HttpContext);

                return RedirectToAction("Index", "Home");
            }
            
            ViewBag.Validation = "Usuario y/o contraseña incorrecta";
            return View();
        }


        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
