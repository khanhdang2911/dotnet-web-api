using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_web_api.Dtos.Stock;
using dotnet_web_api.Models;

namespace dotnet_web_api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto{
                Id=stockModel.Id,
                CompanyName=stockModel.CompanyName,
                Purchase=stockModel.Purchase,
                LastDiv=stockModel.LastDiv,
                Industry=stockModel.Industry,
                MarketCap=stockModel.MarketCap,
                Comments=stockModel.Comments.Select(c=>c.ToCommentDto()).ToList()
            };
        }
        public static Stock ToStockFromRequest(this CreateStockRequestDto stockRequestDto)
        {
            return new Stock{
                CompanyName=stockRequestDto.CompanyName,
                Purchase=stockRequestDto.Purchase,
                LastDiv=stockRequestDto.LastDiv,
                Industry=stockRequestDto.Industry,
                MarketCap=stockRequestDto.MarketCap,
            };
        }
    }
}