using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_web_api.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        public string CompanyName{set;get;}=string.Empty;
        public decimal Purchase{set;get;}
        public decimal LastDiv{set;get;}
        public string Industry{set;get;}=string.Empty;
        public long MarketCap{set;get;}
    }
}