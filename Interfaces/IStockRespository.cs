using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_web_api.Data;
using dotnet_web_api.Dtos.Stock;
using dotnet_web_api.Helpers;
using dotnet_web_api.Models;

namespace dotnet_web_api.Interfaces
{
    public interface IStockRespository
    {
        public Task<List<Stock>> GetAllSAsync(ObjectQuery query);
        public Task<Stock?> GetStockAsync(int id);
        public Task<Stock?> DeleteById(int id);
        public Task<Stock> Create(Stock stockDto);
        public Task<Stock?> Update(int id,UpdateStockRequestDto updateStockRequestDto);
        public Task<bool> CheckExist(int stockId);
    }
}