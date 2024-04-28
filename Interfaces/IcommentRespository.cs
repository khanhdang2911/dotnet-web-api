using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_web_api.Dtos.Comment;
using dotnet_web_api.Models;

namespace dotnet_web_api.Interfaces
{
    public interface IcommentRespository
    {
        public Task<List<Comment>> GetAllCommentAsync();
        public Task<Comment?>GetCommentByIdAsync(int id);
        public Task<Comment>CreateCommentAsync(Comment comment);
        public Task<Comment?>DeleteCommentByIdAsync(int id);
        public Task<Comment?>UpdateCommentByIdAsync(int id,CommentUpdateDto commentUpdateDto);
    }
}