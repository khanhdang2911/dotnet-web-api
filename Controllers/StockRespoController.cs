using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_web_api.Data;
using dotnet_web_api.Dtos.Stock;
using dotnet_web_api.Helpers;
using dotnet_web_api.Interfaces;
using dotnet_web_api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockRespoController : ControllerBase
    {
        private readonly IAuthorizationService _authorService;
        private readonly ApplicationDBContext _context;
        private readonly IStockRespository _iStockRespo;
        // private readonly ApplicationDBContext _context;
        public StockRespoController(IStockRespository iStockRespo,IAuthorizationService authorService,ApplicationDBContext context)
        {
            _iStockRespo=iStockRespo;
            _authorService=authorService;
            _context=context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery]ObjectQuery query)
        {
            var userId=User.Claims.First(x=>x.Type=="Id").Value;
            var user=_context.users.Find(int.Parse(userId));
            var check=await _authorService.AuthorizeAsync(User,user,"InAge");
            if(!check.Succeeded)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            var stocks=await _iStockRespo.GetAllSAsync(query);
            var stockDtos=stocks.Select(s=>s.ToStockDto()).ToList();
            return Ok(stockDtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock= await _iStockRespo.GetStockAsync(id);
            if(stock==null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
            
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateStockRequestDto stockRequestDto)
        {
            var stock=await _iStockRespo.Create(stockRequestDto.ToStockFromRequest());
            return Ok(stock);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stock=await _iStockRespo.DeleteById(id);
            if(stock==null)
            {
                return NotFound();
            }
            return Ok();
            
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] UpdateStockRequestDto updateStockRequestDto)
        {
            var stock=await _iStockRespo.Update(id,updateStockRequestDto);
            if(stock==null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}