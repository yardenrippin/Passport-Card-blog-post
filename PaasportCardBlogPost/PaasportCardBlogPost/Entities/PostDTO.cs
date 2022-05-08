namespace PaasportCardBlogPost.Entities
{
    public class PostDTO
    {
        public int id { get; set; }

        public string? Title { get; set; }

       public string? Author { get; set; }

     
        public string? Content { get; set; }

        public DateTime DateTime { get; set; } = DateTime.UtcNow;

        public ICollection<CommentDTO>? Comments { get; set; }
    }
}
