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
            return await _postRepository.GetAllPostsAsync();
        }
    }
}