using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController(IPostService postService) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var posts = await postService.GetPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await postService.GetPostByIdAsync(id);
            if(post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }
    }
}