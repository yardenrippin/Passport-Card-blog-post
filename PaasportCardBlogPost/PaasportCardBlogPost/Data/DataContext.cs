using Microsoft.EntityFrameworkCore;
using PaasportCardBlogPost.Entities;

namespace PaasportCardBlogPost.Data
{
  
        public class DataContext : DbContext
        {
            public DataContext(DbContextOptions<DataContext> options) : base(options)
            {

            }
            public DbSet<Post> Posts { get; set; }
            public DbSet<Comment> Comments { get; set; }
        }
    
}
