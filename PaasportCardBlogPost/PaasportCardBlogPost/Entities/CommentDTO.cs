namespace PaasportCardBlogPost.Entities
{
    public class CommentDTO
    {
        public int id { get; set; }

        public string? Content { get; set; }

        public DateTime Datetime { get; set; } = DateTime.UtcNow;
    }
}
