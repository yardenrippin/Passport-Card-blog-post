using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PaasportCardBlogPost.Data;
using PaasportCardBlogPost.Entities;
using PaasportCardBlogPost.Helpers;
using PaasportCardBlogPost.Interfaces;

namespace PaasportCardBlogPost.DataUtilities
{
    public class PostUtils:Ipost
    {
        private readonly DataContext _context;
        private readonly IMapper _Mapper;
        public PostUtils(DataContext context, IMapper Mapper)
        {
            _context = context;
            _Mapper = Mapper;   
        }

        public void Add(Post Post)
        {
            _context.Posts.Add(Post);
        }

        public async Task Delete(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.id == id);
            _context.Posts.Remove(post);
        }

        public async Task<PageingList<PostDTO>> GetPosts(PaginationParams PaginationParams)
        {

           var query = _context.Posts.Include(C=>C.Comments).OrderByDescending(x => x.DateTime).AsNoTracking();
           if (PaginationParams.OnlyComment == "true")
            {
                query = query.Where(c => c.Comments.Count > 0);
            }
            if (PaginationParams.OnlyToday == "true")
            {
                query = query.Where(p => p.DateTime.Date==DateTime.Today.Date);
            }
            if (PaginationParams.ShortPost == "true")
            {
                query = query.Where(P=>P.Content.Length<50);
            }
            return await PageingList<PostDTO>.CreateAsync(query.ProjectTo<PostDTO>(_Mapper
           .ConfigurationProvider).AsNoTracking(), PaginationParams.PageNumber, PaginationParams.PageSize);
        
        }
  
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task <Post> GetByid(int id)
        {
           
            var post = await _context.Posts.Include(C => C.Comments).FirstOrDefaultAsync(x => x.id == id);
            return post;
        }
    }
}
