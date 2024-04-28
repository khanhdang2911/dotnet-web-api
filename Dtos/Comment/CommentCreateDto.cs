using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_web_api.Dtos.Comment
{
    public class CommentCreateDto
    {
        [Required]
        [MinLength(3,ErrorMessage ="Độ dài của title phải từ 3 kí tự")]
        public string Title{get; set;}=string.Empty;
        public string Content{get; set;}=string.Empty;
    }
}