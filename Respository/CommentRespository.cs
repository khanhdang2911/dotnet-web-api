using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_web_api.Data;
using dotnet_web_api.Dtos.Comment;
using dotnet_web_api.Interfaces;
using dotnet_web_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_web_api.Respository
{
    public class CommentRespository : IcommentRespository
    {
        private readonly ApplicationDBContext _context;
        public CommentRespository(ApplicationDBContext context)
        {
            _context=context;
        }
        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            await _context.comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

       

        public async Task<List<Comment>> GetAllCommentAsync()
        {
            var comments=await _context.comments.ToListAsync();
            return comments;
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            var comment=await _context.comments.FirstOrDefaultAsync(s=>s.Id==id);
            if(comment==null)
            {
                return null;
            }
            return comment;
        }

         public async Task<Comment?> DeleteCommentByIdAsync(int id)
        {
            var comment=await _context.comments.FirstOrDefaultAsync(s=>s.Id==id);
            if(comment==null)
            {
                return null;
            }
            _context.comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> UpdateCommentByIdAsync(int id,CommentUpdateDto commentUpdateDto)
        {
            var commentUpdate=await _context.comments.FirstOrDefaultAsync(s=>s.Id==id);
            if(commentUpdate==null)
            {
                return null;
            }
            commentUpdate.Title=commentUpdateDto.Title;
            commentUpdate.Content=commentUpdate.Content;
            await _context.SaveChangesAsync();
            return commentUpdate;
        }
    }
}