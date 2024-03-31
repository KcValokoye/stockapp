namespace StockAppSQ20.Helpers
{
    public class QueryObject
    {
        //filtering
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;

        //sorting
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; } = false;
        
        //pagination

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
