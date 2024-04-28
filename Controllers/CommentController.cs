using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_web_api.Dtos.Comment;
using dotnet_web_api.Dtos.Stock;
using dotnet_web_api.Interfaces;
using dotnet_web_api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace dotnet_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IStockRespository _istockRespository;
        private readonly IcommentRespository _icommentRespository;
        public CommentController(IcommentRespository icommentRespository,IStockRespository istockRespository)
        {
            _icommentRespository=icommentRespository;
            _istockRespository=istockRespository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCommentAsync()
        {
            var comments=await _icommentRespository.GetAllCommentAsync();
            var commentDtos=comments.Select(c=>c.ToCommentDto());
            return Ok(commentDtos);
        }

        [HttpPost]
        [Route("{stockId:int}")]
        public async Task<IActionResult> CreateCommentAsync(int stockId, CommentCreateDto commentCreateDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Kiem tra stock id
            if(!await _istockRespository.CheckExist(stockId))
            {
                return BadRequest("StockId does not exist ");
            }

            await _icommentRespository.CreateCommentAsync(commentCreateDto.ToCommentFromCreate(stockId));
            
            return Ok(commentCreateDto);
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCommentAsync([FromRoute] int id, [FromBody] CommentUpdateDto commentUpdateDto)
        {
            var comment=await _icommentRespository.UpdateCommentByIdAsync(id,commentUpdateDto);
            if(comment==null)
            {
                return NotFound("Comment does not exist");
            }
            return Ok(comment);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCommentAsync([FromRoute]int id)
        {
            var comment=await _icommentRespository.DeleteCommentByIdAsync(id);
            if(comment==null)
            {
                return NotFound("Comment does not exist");
            }
            return Ok(comment);
        }
    }
}