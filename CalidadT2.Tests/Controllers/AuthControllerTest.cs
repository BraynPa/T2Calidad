using System;
using CalidadT2.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalidadT2.Models;
using CalidadT2.Repositorios;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;
using CalidadT2.Service;

namespace CalidadT2.Tests.Controllers
{
    [TestFixture]
    public class AuthControllerTest
    {
        [Test]
            public void LoginReturnRedirecToAction()
        {
            var repositoryMock = new Mock<IUsuarioRepositorio>();
            repositoryMock.Setup(o => o.FindUser("admin", It.IsAny<String>())).Returns(new Usuario { });
            var authMock = new Mock<ICookieAuthService>();
            var controller = new AuthController(repositoryMock.Object, authMock.Object);
            var result = controller.Login("admin", "admin");
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }
    }
}
