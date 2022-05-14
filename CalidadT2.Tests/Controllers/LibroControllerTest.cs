using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using CalidadT2.Service;
using CalidadT2.Repositorios;
using CalidadT2.Controllers;
using Microsoft.AspNetCore.Mvc;
using CalidadT2.Models;

namespace CalidadT2.Tests.Controllers
{
    internal class LibroControllerTest
    {
        [Test]
        public void CasoDetails()
        {
            var claim = new Mock<ICookieAuthService>();
            var repository = new Mock<ILibroRepository>();
            repository.Setup(o => o.ObtenerFirst(1)).Returns(new Object());
            var controller = new LibroController(repository.Object, claim.Object);
            var view = controller.Details(1) as ViewResult;

            Assert.IsNotNull(view);
        }
        [Test]
        public void CasoAddComentary()
        {
            var repository = new Mock<ILibroRepository>();
            var claim = new Mock<ICookieAuthService>();
            claim.Setup(v => v.GetLoggedUsername()).Returns(new Usuario { });
            repository.Setup(o => o.AddComentario(new Comentario(), new Usuario { })).Returns(new Comentario());
            var controller = new LibroController(repository.Object, claim.Object);
            var view = controller.AddComentario(new Comentario { Texto = "abc" }) as ViewResult;

            Assert.IsNull(view);
        }
    }
}
