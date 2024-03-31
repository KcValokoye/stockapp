using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockAppSQ20.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5,ErrorMessage = "Title must be more than 5 character")]
        [MaxLength (280, ErrorMessage = "Title must be more than 280 character")]


        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Content must be more than 5 character")]
        [MaxLength(280, ErrorMessage = "Content must be more than 280 character")]

        public string Content { get; set; } = string.Empty;

       // public DateTime CreatedOn { get; set; } = DateTime.Now;

        //public Model.Stock? Stock { get; set; }
    }
}
