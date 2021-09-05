using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public class Post
    {
        public int PostId { get; set; }

        #nullable enable
        public byte[]? Image { get; set; }
        #nullable disable
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Post() { }

        public Post(string description, byte[] image = null)
        {
            Description = description;
            Image = image;
            Timestamp = DateTime.Now;
        }
    }
}
