using ApiBlog.Data;
using ApiBlog.Models;
using ApiBlog.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiBlog.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _bd;

        public PostRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool CreatePost(Post post)
        {
            post.FechaCreacion = DateTime.Now;
            _bd.Post.Add(post);
            return Save();
        }

        public bool DeletePost(Post post)
        {
            _bd.Post.Remove(post);
            return Save();
        }

        public Post GetPost(int postId)
        {
            return _bd.Post.FirstOrDefault(c => c.Id == postId);
        }

        public ICollection<Post> GetPosts()
        {
            return _bd.Post.OrderBy(c => c.Id).ToList();
        }

        public bool PostExists(string nombre)
        {
            bool valor = _bd.Post.Any(c => c.Titulo.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool PostExists(int id)
        {
            bool valor = _bd.Post.Any(c => c.Id == id);
            return valor;
        }

        public bool Save()
        {
            return _bd.SaveChanges() > 0? true : false;
        }

        public bool UpdatePost(Post post)
        {
            post.FechaActualizacion = DateTime.Now;
            var imagenDesdeBd = _bd.Post.AsNoTracking().FirstOrDefault(c => c.Id == post.Id);

            if (post.RutaImagen == null)
            {
                post.RutaImagen = imagenDesdeBd.RutaImagen;
            }

            _bd.Post.Update(post);
            return Save();
        }
    }
}
