using System.ComponentModel.DataAnnotations;

namespace PaasportCardBlogPost.Entities
{
    public class Post
    {
        public int id { get; set; }

        [Required] public string? Title { get; set; }

        [Required] public string? Author { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 5)]
        public string? Content { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        public ICollection<Comment>? Comments { get; set; }
    }
}
