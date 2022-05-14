using CalidadT2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalidadT2.Repositorios
{
    public interface ILibroRepository
    {
        //List<Libro> ObtenerLibrosConAutores();

        object ObtenerFirst(int id);
        Comentario AddComentario(Comentario comentario,Usuario usuario);

    }
    public class LibroRepository : ILibroRepository
    {
        private AppBibliotecaContext app;
        public LibroRepository(AppBibliotecaContext app)
        {
            this.app = app;
        }
        //public List<Libro> ObtenerLibrosConAutores()
        //{
         //   return app.Libros.Include(o => o.Autor).ToList();

        //}
        public object ObtenerFirst(int id)
        {
            return app.Libros
                 .Include("Autor")
                 .Include("Comentarios.Usuario")
                 .Where(o => o.Id == id)
                 .FirstOrDefault();
        }
        public Comentario AddComentario(Comentario comentario, Usuario usuario)
        {

            comentario.UsuarioId = usuario.Id;
            comentario.Fecha = DateTime.Now;
            app.Comentarios.Add(comentario);

            var libro = app.Libros.Where(o => o.Id == comentario.LibroId).FirstOrDefault();
            libro.Puntaje = (libro.Puntaje + comentario.Puntaje) / 2;

            app.SaveChanges();
            return comentario;
        }
    }
}
