using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_web_api.Dtos.Comment;
using dotnet_web_api.Models;

namespace dotnet_web_api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto{
                Id=commentModel.Id,
                Title=commentModel.Title,
                Content=commentModel.Content,
                CreatedOn =commentModel.CreatedOn,
                StockId=commentModel.StockId
            };
        }
        public static Comment ToCommentFromUpdate(this CommentUpdateDto commentUpdateDto)
        {
            return new Comment{
                Title=commentUpdateDto.Title,
                Content=commentUpdateDto.Content,
            };
        }
        public static Comment ToCommentFromCreate(this CommentCreateDto commentCreateDto,int stockId)
        {
            return new Comment{
                Title=commentCreateDto.Title,
                Content=commentCreateDto.Content,
                CreatedOn =DateTime.Now,
                StockId=stockId
            };
        }
    }
}