using PaasportCardBlogPost.Entities;

namespace PaasportCardBlogPost.Interfaces
{
    public interface Icomment
    {
        Task<IEnumerable<Comment>> GetAllComment();
        void Add(Comment Comment);
        Task<bool> SaveAll();

        Task<Comment> GetByid(int id);
    }
}
