using AutoMapper;
using PaasportCardBlogPost.Entities;

namespace PaasportCardBlogPost.DataUtilities
{
    public class AutoMapperProfiles : Profile
    {
       
            public AutoMapperProfiles()
            {
                CreateMap<Post, PostDTO>();
                CreateMap<Comment, CommentDTO>();
            }
       
    }
}
