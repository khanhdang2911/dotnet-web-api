using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_web_api.Data;
using dotnet_web_api.Dtos.Stock;
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
        public void Create(CreateStockRequestDto stockRequestDto)
        {
            _context.stocks.Add(stockRequestDto.ToStockFromRequest());
            _context.SaveChanges();
        }
        public void DeleteById(int id)
        {
            var stock=_context.stocks.Find(id);
            _context.stocks.Remove(stock);
        }
        public async Task<List<Stock>> GetAllSAsync()
        {
            var stocks=await _context.stocks.ToListAsync();
            return stocks;
        }

        public async Task<Stock> GetStockAsync(int id)
        {
            var stock=await _context.stocks.SingleOrDefaultAsync(s=>s.Id==id);
            return stock;
        }

        public bool Update(int id,UpdateStockRequestDto updateStockRequestDto)
        {
            var stock=_context.stocks.SingleOrDefault(s=>s.Id==id);
            if(stock!=null)
            {
                _context.Entry(stock).State = EntityState.Modified;
                stock.CompanyName=updateStockRequestDto.CompanyName;
                stock.Purchase=updateStockRequestDto.Purchase;
                stock.LastDiv=updateStockRequestDto.LastDiv;
                stock.Industry=updateStockRequestDto.Industry;
                stock.MarketCap=updateStockRequestDto.MarketCap;
                _context.SaveChanges();
            }
            return false;
        }

       
    }
}