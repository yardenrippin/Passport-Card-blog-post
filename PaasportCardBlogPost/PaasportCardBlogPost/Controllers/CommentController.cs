using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaasportCardBlogPost.Entities;
using PaasportCardBlogPost.Interfaces;
using PaasportCardBlogPost.Middleware;

namespace PaasportCardBlogPost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiKey]
    public class CommentController : ControllerBase
    {
        
        private readonly Icomment _repo;
        private readonly IMapper _Mapper;
        public CommentController(Icomment repo, IMapper Mapper)
        {
            _repo = repo;
            _Mapper = Mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> Get()
        {
            var Comment = await _repo.GetAllComment();

            return Ok(Comment);

        }
        [HttpPost("Add")]
        public async Task<IActionResult> add([FromBody] Comment Comment)
        {
            if (Comment == null)
            {
                return BadRequest("no data to add");
            }
            _repo.Add(Comment);
            if (await _repo.SaveAll())
            {
                return CreatedAtRoute("GetPost", new { id = Comment.id }, Comment);
            }

            throw new Exception(" post" + Comment.id + " failed on add ");
        }


        [HttpGet("{id}", Name = "GetCooment")]
        public async Task<ActionResult<Comment>> GetCooment(int id)
        {
            var Cooment = await _repo.GetByid(id);
         
            return Ok(Cooment);

        }
    }
}

