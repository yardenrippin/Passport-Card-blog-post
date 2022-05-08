using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaasportCardBlogPost.Entities;
using PaasportCardBlogPost.Helpers;
using PaasportCardBlogPost.HttpExtensions;
using PaasportCardBlogPost.Interfaces;
using PaasportCardBlogPost.Middleware;

namespace PaasportCardBlogPost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiKey]
    public class PostController : ControllerBase
    {
        private readonly Ipost _repo;
        private readonly IMapper _Mapper;
        public PostController(Ipost repo, IMapper Mapper)
        {
            _repo = repo;
            _Mapper = Mapper;
        }
        [HttpGet("{id}", Name = "GetPost")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var Post = await _repo.GetByid(id);
            var toreturn = _Mapper.Map<PostDTO>(Post);
            return Ok(toreturn);

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> Get([FromQuery] PaginationParams pagination)
        {
            var Post = await _repo.GetPosts(pagination);
            Response.AddPaginationHeader(Post.CurrentPage, Post.PageSize, Post.TotalCount, Post.TotalPages);

            
            return Ok(Post);


        }
        [HttpPost("Add")]
        public async Task<IActionResult> add([FromBody] Post post)
        {
            if (post == null)
            {
                return BadRequest("no data to add");
            }
           _repo.Add(post);
            if (await _repo.SaveAll())
            {
            
                return CreatedAtRoute("GetPost", new {id= post.id }, post);
            }
            
            throw new Exception(" post" + post.Title + " failed on add ");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("this Post dont exist on data");
            }

            await _repo.Delete(id);

            if (await _repo.SaveAll())
                return Ok();

            throw new Exception(" post" + id + "failed on delete");
        }
    }

}

