using PaasportCardBlogPost.Entities;
using PaasportCardBlogPost.Helpers;

namespace PaasportCardBlogPost.Interfaces
{
    public interface Ipost
    {
        Task<PageingList<PostDTO>> GetPosts(PaginationParams pagination);
        void Add(Post Post);

        Task Delete(int id);
      
        Task <Post>GetByid(int id);
        Task<bool> SaveAll();

    }
}
