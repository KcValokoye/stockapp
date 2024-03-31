using StockAppSQ20.Dtos.Stock;
using StockAppSQ20.Helpers;
using StockAppSQ20.Model;

namespace StockAppSQ20.Interfaces;

public interface IStockRepository
{
    Task<Stock> CreateAsync(Stock stockModel);
   // Task GetAll(object id);
    Task<Stock> GetByIdAsync(int id);

    Task<Stock> UpdateAsync(int id, UpdateStockRequestDto updateDto);

    Task<List<Stock?>> GetAllAsync(QueryObject query);

    Task<Stock?> DeleteAsync(int id);

    Task<bool> StockExist(int id);
}