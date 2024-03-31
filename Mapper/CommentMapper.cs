using StockAppSQ20.Dtos.Comment;
using StockAppSQ20.Dtos.Stock;
using StockAppSQ20.Model;

namespace StockAppSQ20.Mapper
{
    public static class CommentMapper
    {
        public static Comment ToCommentFromCreateDTO(this CreateCommentDto commentModel, int stockId)
        {
            return new Comment
            {
                Title = commentModel.Title,
                Content = commentModel.Content,
                StockId = stockId
            };
        }

        public static CommentDto ToComment(this Comment commentModel)
        {
            return new CommentDto()
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };

        }
    }
}

