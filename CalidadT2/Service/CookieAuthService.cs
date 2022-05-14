using CalidadT2.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace CalidadT2.Service
{
    public interface ICookieAuthService
    {
        void SetHttpContext(HttpContext httpContext);
        Usuario GetLoggedUsername();

    }


    public class CookieAuthService : ICookieAuthService
    {
        private AppBibliotecaContext app;
        private HttpContext httpContext;
        public CookieAuthService(AppBibliotecaContext app)
        {
            this.app = app;
        }

        public void SetHttpContext(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }
        public Usuario GetLoggedUsername()
        {
            var claim = httpContext.User.Claims.FirstOrDefault();
            var user = app.Usuarios.Where(o => o.Username == claim.Value).FirstOrDefault();
            return user;
        }
    }
}
