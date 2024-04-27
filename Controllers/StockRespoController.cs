using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_web_api.Data;
using dotnet_web_api.Dtos.Stock;
using dotnet_web_api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockRespoController : ControllerBase
    {
        private readonly IStockRespository _iStockRespo;
        public StockRespoController(ApplicationDBContext context,IStockRespository iStockRespo)
        {
            _iStockRespo=iStockRespo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try{
                var stocks=await _iStockRespo.GetAllSAsync();
                return Ok(stocks);
            }
            catch(Exception e){

            }
            return BadRequest();            
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try{
                var stock= await _iStockRespo.GetStockAsync(id);
                return Ok(stock);
            }
            catch(Exception e){
                return BadRequest();  
            }
        }
        
        // [HttpPost]
        // public IActionResult Create([FromBody]CreateStockRequestDto stockRequestDto)
        // {
        //     return Ok();
        // }
        // [HttpDelete("{id}")]
        // public IActionResult Delete(int id)
        // {
            
        //     return Ok();
        // }
        // [HttpPut("{id}")]
        // public IActionResult Update(int id,[FromBody] UpdateStockRequestDto updateStockRequestDto)
        // {
        //     return Ok();
        // }
    }
}