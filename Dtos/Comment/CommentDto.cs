using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_web_api.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set;}
        public string Title{get; set;}=string.Empty;
        public string Content{get; set;}=string.Empty;
        public DateTime CreatedOn{set;get;}=DateTime.Now;
        public int? StockId{set;get;}

    }
}