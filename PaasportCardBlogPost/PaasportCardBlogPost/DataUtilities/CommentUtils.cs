using Microsoft.EntityFrameworkCore;
using PaasportCardBlogPost.Data;
using PaasportCardBlogPost.Entities;
using PaasportCardBlogPost.Interfaces;

namespace PaasportCardBlogPost.DataUtilities
{
    public class CommentUtils:Icomment
    {

        private readonly DataContext _context;

        public CommentUtils(DataContext context)
        {
            _context = context;
        }

        public void Add(Comment Comment)
        {
            _context.Comments.Add(Comment);
        }

        public async Task<IEnumerable<Comment>> GetAllComment()
        {

            var List = await _context.Comments.OrderBy(x => x.Datetime).ToArrayAsync();
            return List;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Comment> GetByid(int id)
        {

            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.id == id);
            return comment;
        }
    }
}

