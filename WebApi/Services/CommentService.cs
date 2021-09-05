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
    public class CommentService
    {
        private readonly CommentRepository _repo;

        public CommentService(CommentRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateComment(Comment comment)
        {
            if (comment == null || comment.Description == null)
            {
                throw new ArgumentException("Invalid comment format", nameof(comment));
            }
            
               
                Comment newComment = new Comment(){
                    Id = comment.Id,
                    Description = comment.Description,
                    Timestamp = comment.Timestamp,
                    PostId = comment.PostId
                };
                await _repo.CreateComment(newComment);
            
        }

        public async Task<Comment> GetCommentById(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Invalid Id value", nameof(id));
            }
            return await _repo.GetCommentById(id);
        }

        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            return await _repo.GetAllComments();
        }

        public async Task DeleteComment(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Invalid Id value", nameof(id));
            }
            await _repo.DeleteComment(id);
        }
    }
}
