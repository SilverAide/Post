using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApi.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
#nullable enable
        public IFormFile? Image { get; set; }
#nullable disable
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }

        public PostDTO() { }
    }
}
