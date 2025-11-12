using DataAccessLayer.Interfaces;
using Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public PostRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            var posts = new List<Post>();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("SELECT UserId, Id, Title, Body FROM Post", connection);
            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while(await reader.ReadAsync())
            {
                posts.Add(new Post
                {
                    UserId = reader.GetInt32(0),
                    Id = reader.GetInt32(1),
                    Title = reader.GetString(2),
                    Body = reader.GetString(3)
                });
            }
            return posts;
        }
    }
}