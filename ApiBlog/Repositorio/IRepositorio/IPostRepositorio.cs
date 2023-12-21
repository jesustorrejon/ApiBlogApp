using ApiBlog.Modelos;

namespace ApiBlog.Repositorio.IRepositorio
{
    public interface IPostRepositorio
    {
        ICollection<Post> GetPosts();
        Post GetPost(int postId);
        bool PostExists(string nombre);
        bool PostExists(int id);
        bool CreatePost(Post post);
        bool UpdatePost(Post post);
        bool DeletePost(Post post);
        bool Save();
    }
}
