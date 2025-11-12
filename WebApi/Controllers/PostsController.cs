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
            //throw new Exception("This is a sample test exception.");
            var posts = await postService.GetPostsAsync();
            return Ok(posts);
        }
    }
}