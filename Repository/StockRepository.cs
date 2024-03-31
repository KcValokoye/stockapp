using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockAppSQ20.Data;
using StockAppSQ20.Dtos.Stock;
using StockAppSQ20.Helpers;
using StockAppSQ20.Interfaces;
using StockAppSQ20.Model;
using System.Collections;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StockAppSQ20.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingStock == null)
            {
                return null; //or not found, but never return status code, any where other than the controller.
            }

            _context.Stocks.Remove(existingStock);
            await _context.SaveChangesAsync();
            return existingStock;
        }

        /*public async Task<List<Stock?>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
        }*/
        
        public async Task<List<Stock?>> GetAllAsync(QueryObject query)
        {
            var stock = _context.Stocks.AsQueryable();

            //filtering

            if (!string.IsNullOrEmpty(query.Symbol))
            {
                stock = stock.Where(s => s.Symbol.Contains(query.Symbol));
            }
            if (!string.IsNullOrEmpty(query.CompanyName))
            {
                stock = stock.Where(s => s.CompanyName.Contains(query.CompanyName));
            } 
            
            //sorting
            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
            if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
               {
                stock = query.IsDescending ? stock.OrderByDescending(s => s.Symbol) : stock.OrderBy(s => s.Symbol);
               }
            }

            //pagination

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stock.Skip(skipNumber).Take(query.PageSize).ToListAsync();
            }
         
       


        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingStock == null)
            {
                return null; //or not found, but never return status code, any where other than the controller.
            }
            //else start updating

            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();
            return existingStock;

        }

        public async Task<bool> StockExist(int id)
        {
            return await _context.Stocks.AnyAsync(x =>x.Id ==id);
        }

       
    }

    /* public class StockRepository : IStockRepository
     {
         private readonly ApplicationDBContext _context;

         public StockRepository(ApplicationDBContext context)
         {
             _context = context;
         }


         public async Task<Stock> CreateAsync(Stock stockModel)
         {
             await _context.Stocks.AddAsync(stockModel);
             await _context.SaveChangesAsync();
             return stockModel;
         }

         public async Task<Stock> GetByIdAsync(int id)
         {
             return await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

         }

         public async Task<Stock> UpdateAsync(int id, UpdateStockRequestDto stockDto)
         {
             var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
             if (existingStock == null)
             {
                 return null;
             }

             existingStock.Symbol = stockDto.Symbol;
             existingStock.CompanyName = stockDto.CompanyName;
             existingStock.Purchase = stockDto.Purchase;
             existingStock.LastDiv = stockDto.LastDiv;
             existingStock.Industry = stockDto.Industry;
             existingStock.MarketCap = stockDto.MarketCap;


             await _context.SaveChangesAsync();
             return existingStock;

         }

         [HttpGet]

         public async Task<List<Stock?>> GetAllAsync(QueryObject query)
         {
             *//*{
                 return await _context.Stocks.ToListAsync();
             }
     *//*

             var stock = _context.Stocks..AsQueryable();
             if (!string.IsNullOrWhiteSpace(query.Symbol))
             {
                 stock = stock.Where(stock => stock.Symbol.Contains(query.Symbol));
             }

             if (!string.IsNullOrWhiteSpace(query.CompanyName))
             {

             }
         }

         public async Task<Stock?> DeleteAsync(int id)

         {
             var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
             if (existingStock == null)
             {
                 return null;
             }
             _context.Stocks.Remove(existingStock);
             await _context.SaveChangesAsync();
             return existingStock;

         }

         public Task GetAll(object id)
         {
             throw new NotImplementedException();
         }
     }*/
}

    