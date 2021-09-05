using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using DataAccess.Repositories;
using Domain.Models;
using WebApi.DTOs;
using DataAccess.Models;
using DataAccess.Mappers;



namespace WebApi.Services
{
    public class PostService
    {
        private readonly PostRepository _repo;

        public PostService(PostRepository repo)
        {
            _repo = repo;
        }

        public async Task CreatePost(DataAccess.Models.Post post)
        {
            if (post == null || post.Description == null)
            {
                throw new ArgumentException("Invalid post format", nameof(post));
            }
                await _repo.CreatePost(post.ToDomain());
        
        }

        public async Task<Domain.Models.Post> GetPostById(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Invalid Id value", nameof(id));
            }
            return await _repo.GetPostById(id);
        }

        public async Task<IEnumerable<Domain.Models.Post>> GetAllPosts()
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
