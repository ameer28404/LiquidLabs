using Domain.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post?> GetPostByIdAsync(int id);
        Task SavePostAsync(Post post);
    }
}