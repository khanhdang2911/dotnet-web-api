using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace dotnet_web_api.Models
{
    [Table("Stock")]
    public class Stock
    {
        public int Id{set;get;}
        public string CompanyName{set;get;}=string.Empty;
        [Column(TypeName ="decimal(18,2)")]
        public decimal Purchase{set;get;}
        [Column(TypeName ="decimal(18,2)")]
        public decimal LastDiv{set;get;}
        public string Industry{set;get;}=string.Empty;
        public long MarketCap{set;get;}
        public List<Comment> Comments{set;get;}=new List<Comment>();
    }
}