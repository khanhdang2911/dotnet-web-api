using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_web_api.Data;
using dotnet_web_api.Dtos.Stock;
using dotnet_web_api.Models;

namespace dotnet_web_api.Interfaces
{
    public interface IStockRespository
    {
        public Task<List<Stock>> GetAllSAsync();
        public Task<Stock> GetStockAsync(int id);
        public void DeleteById(int id);
        public void Create(CreateStockRequestDto stockDto);
        public bool Update(int id,UpdateStockRequestDto updateStockRequestDto);
    }
}