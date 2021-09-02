using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Domain.Models;

namespace DataAccess.Repositories
{
    public class PostRepository
    {

        private readonly PostDbContext _context;

        public PostRepository(PostDbContext context)
        {
            _context = context;
        }

        public async Task CreatePost(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task<Post> GetPostById(int id)
        {
            var query = await _context.Posts.FindAsync(id);
            if (query != null)
            {
                return query;
            }
            throw new ArgumentException("Couldn't find post with that Id", nameof(id));
        }

        public async Task<List<Post>> GetAllPosts()
        {
            if (_context.Posts.Count() > 0)
            {
                return await _context.Posts.ToListAsync();
            }
            return new List<Post>();
        }

        public async Task DeletePost(int id)
        {
            var query = await _context.Posts.FindAsync(id);
            if (query != null)
            {
                _context.Posts.Remove(query);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Couldn't find Post to delete with that Id.", nameof(id));
            }
        }

        public async Task<bool> DeleteExpiredPosts()
        {
            var expiredPosts = await _context.Posts.Select(e => e).Where(e => DateTime.Now > e.Timestamp.AddDays(1)).ToListAsync();

            if (expiredPosts.Count > 0)
            {
                foreach (var post in expiredPosts)
                {
                    _context.Posts.Remove(post);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
