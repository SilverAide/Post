using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using DataAccess.Repositories;
using Domain.Models;
using WebApi.DTOs;

namespace WebApi.Services
{
    public class PostService
    {
        private readonly PostRepository _repo;

        public PostService(PostRepository repo)
        {
            _repo = repo;
        }

        public async Task CreatePost(PostDTO post)
        {
            if (post == null || post.Description == null)
            {
                throw new ArgumentException("Invalid post format", nameof(post));
            }
            using(var ms = new MemoryStream())
            {
                post.Image.CopyTo(ms);
                var fileBytes = ms.ToArray();
                Post newPost = new Post(post.Description, fileBytes);
                await _repo.CreatePost(newPost);
            }
        }

        public async Task<Post> GetPostById(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Invalid Id value", nameof(id));
            }
            return await _repo.GetPostById(id);
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _repo.GetAllPosts();
        }

        public async Task DeletePost(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Invalid Id value", nameof(id));
            }
            await _repo.DeletePost(id);
        }
    }
}
