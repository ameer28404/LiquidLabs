using Domain.Models;

namespace Service.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPostsAsync();
        Task<Post?> GetPostByIdAsync(int id);
    }
}