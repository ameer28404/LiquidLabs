using Domain.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
    }
}