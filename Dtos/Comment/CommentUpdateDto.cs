using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_web_api.Dtos.Comment
{
    public class CommentUpdateDto
    {
        public string Title{get; set;}=string.Empty;
        public string Content{get; set;}=string.Empty;
    }
}