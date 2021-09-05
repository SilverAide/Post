using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Domain.Models;
using DataAccess.Mappers;


namespace DataAccess.Repositories
{
    public class CommentRepository
    {

        private readonly PostDbContext _context;

        public CommentRepository(PostDbContext context)
        {
            _context = context;
        }

        public async Task CreateComment(Comment comment)
        {
               if (await _context.Posts.FirstOrDefaultAsync(p => p.PostId == comment.PostId) is Models.Post post)
            {
                var domainPost = post.ToDomain();
                var commentDb = comment.ToDataAccess(post);
                await _context.Comments.AddAsync(commentDb);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Post { comment.PostId } not found.", nameof(comment));
            }
           
        }

        public async Task<Comment> GetCommentById(int id)
        {

               var comment = await _context.Comments
                .Include(c => c.Post)
                .SingleOrDefaultAsync(c => c.Id == id);

            return comment?.ToDomain(comment.Post.ToDomain());
        }

        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            if (_context.Comments.Count() > 0)
            {
                var comments = await _context.Comments.ToListAsync();

                return comments.Select(p => p.ToDomain(p.Post.ToDomain()));
            }
            return new List<Comment>();
        }

        public async Task DeleteComment(int id)
        {
            var query = await _context.Comments.FindAsync(id);
            if (query != null)
            {
                _context.Comments.Remove(query);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Couldn't find Comment to delete with that Id.", nameof(id));
            }
        }

        public async Task<bool> DeleteExpiredcomemnts()
        {
            var expiredComments = await _context.Comments.Select(e => e).Where(e => DateTime.Now > e.Timestamp.AddDays(1)).ToListAsync();

            if (expiredComments.Count > 0)
            {
                foreach (var comemnt in expiredComments)
                {
                    _context.Comments.Remove(comemnt);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
