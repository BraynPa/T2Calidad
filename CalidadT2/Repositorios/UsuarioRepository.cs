using CalidadT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalidadT2.Repositorios
{
    public interface IUsuarioRepositorio
    {
        public Usuario FindUser(string username, string password);
    }
    public class UsuarioRepository : IUsuarioRepositorio
    {
        private readonly AppBibliotecaContext app;

        public UsuarioRepository(AppBibliotecaContext app)
        {
            this.app = app;
        }


        public Usuario FindUser(string username, string password)
        {
            return app.Usuarios
               .Where(o => o.Username == username && o.Password == password)
               .FirstOrDefault();
        }

    }
}
