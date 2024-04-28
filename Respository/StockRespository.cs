using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_web_api.Data;
using dotnet_web_api.Dtos.Stock;
using dotnet_web_api.Helpers;
using dotnet_web_api.Interfaces;
using dotnet_web_api.Mappers;
using dotnet_web_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_web_api.Respository
{
    public class StockRespository : IStockRespository
    {
        private readonly ApplicationDBContext _context;
        public StockRespository(ApplicationDBContext context)
        {
            _context=context;
        }

        public async Task<bool> CheckExist(int stockId)
        {
            return await _context.stocks.AnyAsync(s=>s.Id==stockId);
        }

        public async Task<Stock> Create(Stock stockModel)
        {
            await _context.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteById(int id)
        {
            var stock=_context.stocks.Find(id);
            if(stock==null)
            {
                return null;
            }
            _context.stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllSAsync(ObjectQuery query)
        {
            var stocks=_context.stocks.Include(s=>s.Comments).AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks=stocks.Where(s=>s.CompanyName.Contains(query.CompanyName));
            }
            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy=="LastDiv")
                {
                    stocks=query.IsDescending ? stocks.OrderByDescending(s=>s.LastDiv) : 
                            stocks.OrderBy(s=>s.LastDiv);
                }
            }
            // pagination
            stocks=stocks.Skip((query.PageNumber-1)*query.PageSize).Take(query.PageSize);

            return await stocks.ToListAsync();
        }

        public async Task<Stock?> GetStockAsync(int id)
        {
            var stock=await _context.stocks.Include(s=>s.Comments).SingleOrDefaultAsync(s=>s.Id==id);
            return stock;
        }

        public async Task<Stock?> Update(int id,UpdateStockRequestDto updateStockRequestDto)
        {
            var stock=await _context.stocks.FirstOrDefaultAsync(s=>s.Id==id);
            if(stock==null)
            {
                return null;
            }

            _context.Entry(stock).State = EntityState.Modified;        
                stock.CompanyName=updateStockRequestDto.CompanyName;
                stock.Purchase=updateStockRequestDto.Purchase;
                stock.LastDiv=updateStockRequestDto.LastDiv;
                stock.Industry=updateStockRequestDto.Industry;
                stock.MarketCap=updateStockRequestDto.MarketCap;
            _context.SaveChanges();
            return stock;
        }
    }
}