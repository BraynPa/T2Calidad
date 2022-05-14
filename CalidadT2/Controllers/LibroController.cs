using System;
using System.Linq;
using CalidadT2.Models;
using CalidadT2.Repositorios;
using CalidadT2.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CalidadT2.Controllers
{
    public class LibroController : Controller
    {
        private ICookieAuthService cookieAuthService;
        private ILibroRepository repository;

        public LibroController(ILibroRepository repository, ICookieAuthService cookieAuthService)
        {
            this.repository = repository;
            this.cookieAuthService = cookieAuthService;
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = repository.ObtenerFirst(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddComentario(Comentario comentario)
        {
            Usuario user = cookieAuthService.GetLoggedUsername();
            repository.AddComentario(comentario,user);

            return RedirectToAction("Details", new { id = comentario.LibroId });
        }

        /*private Usuario LoggedUser()
       {
           var claim = HttpContext.User.Claims.FirstOrDefault();
           var user = app.Usuarios.Where(o => o.Username == claim.Value).FirstOrDefault();
           return user;
       } */

    }
}
