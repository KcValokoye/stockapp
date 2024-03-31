using System.ComponentModel.DataAnnotations.Schema;

namespace StockAppSQ20.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [ForeignKey("stockId")]

        public int? StockId { get; set; }

        //public Model.Stock? Stock { get; set; }
    }
}
