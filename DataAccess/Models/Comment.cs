using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace DataAccess.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        
        public int PostId { get; set; }
        public Post Post { get; set; }

        public Comment() { }

        public Comment(int postId, string description)
        {
            Description = description;
            Timestamp = DateTime.Now;
            PostId = PostId;

        }
    }
}