using System.ComponentModel.DataAnnotations;

namespace PaasportCardBlogPost.Entities
{
    public class Comment
    {
      
            public int id { get; set; }
            [Required]
           
            public string? Content { get; set; }

            public DateTime Datetime { get; set; } = DateTime.Now;

            public int Postid { get; set; }

            public Post? Posts { get; set; }
        
    }
}
