using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_web_api.Dtos.Comment;
using dotnet_web_api.Models;

namespace dotnet_web_api.Dtos.Stock
{
    public class StockDto
    {
        public int Id{set;get;}
        public string CompanyName{set;get;}=string.Empty;
        public decimal Purchase{set;get;}
        public decimal LastDiv{set;get;}
        public string Industry{set;get;}=string.Empty;
        public long MarketCap{set;get;}
        public List<CommentDto> Comments {set;get;}=new List<CommentDto>();


    }
}