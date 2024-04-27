using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_web_api.Data;
using dotnet_web_api.Dtos.Stock;
using dotnet_web_api.Mappers;
using dotnet_web_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_web_api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController:Controller
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context=context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks=_context.stocks.ToList().Select(s=>s.ToStockDto());
            return Ok(stocks);
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock=_context.stocks.Find(id);
            if(stock==null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public IActionResult Create([FromBody]CreateStockRequestDto stockRequestDto)
        {
            _context.stocks.Add(stockRequestDto.ToStockFromRequest());
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var stock=_context.stocks.Find(id);
            if(stock==null)
            {
                return NotFound();
            }
            _context.stocks.Remove(stock);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] UpdateStockRequestDto updateStockRequestDto)
        {
            var stockReal=_context.stocks.Find(id);
            if(stockReal==null)
            {
                return NotFound();
            }
            _context.Entry(stockReal).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            
            stockReal.CompanyName=updateStockRequestDto.CompanyName;
            stockReal.Purchase=updateStockRequestDto.Purchase;
            stockReal.LastDiv=updateStockRequestDto.LastDiv;
            stockReal.Industry=updateStockRequestDto.Industry;
            stockReal.MarketCap=updateStockRequestDto.MarketCap;
            _context.SaveChanges();
            return Ok();
        }
    }
}