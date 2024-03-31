using StockAppSQ20.Model;

namespace StockAppSQ20.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment> GetByIdAsync(int id);
    }
}
