namespace PaasportCardBlogPost.Helpers
{

    public class PaginationParams
    {
        private const int MaxPageSize = 10;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 5;
        public string OnlyToday { get; set; } = "false";
        public string OnlyComment { get; set; } = "false";
        public string ShortPost { get; set; } = "false";

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

    }
}
