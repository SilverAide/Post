using System;

namespace Domain.Models
{
    public class Post
    {
        public int Id { get; set; }

        #nullable enable
        public byte[]? Image { get; set; }
        #nullable disable
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }

        public Post() { }

        public Post(string description, byte[] image = null)
        {
            Description = description;
            Image = image;
            Timestamp = DateTime.Now;
        }
    }
}
