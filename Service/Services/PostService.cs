using DataAccessLayer.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Service.Interfaces;
using System.Net.Http.Json;

namespace Service.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PostService(IPostRepository postRepository, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _postRepository = postRepository;
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;

            var baseUrl = _configuration.GetValue<string>("ExternalApiBaseUrl");
            if(!string.IsNullOrEmpty(baseUrl))
                _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            var postsInDb = await _postRepository.GetAllPostsAsync();
            if(postsInDb != null && postsInDb.Any())
            {
                return postsInDb;
            }

            var postsFromApi = await _httpClient.GetFromJsonAsync<List<Post>>("posts");
            if(postsFromApi is not null)
            {
                foreach(var post in postsFromApi)
                {
                    await _postRepository.SavePostAsync(post);
                }
            }
            return postsFromApi ?? new List<Post>();
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            var postInDb = await _postRepository.GetPostByIdAsync(id);
            if(postInDb != null)
            {
                return postInDb;
            }

            var postFromApi = await _httpClient.GetFromJsonAsync<Post>($"posts/{id}");
            if(postFromApi != null)
            {
                await _postRepository.SavePostAsync(postFromApi);
            }
            return postFromApi;
        }
    }
}