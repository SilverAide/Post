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
    public class PostRepository
    {

        private readonly PostDbContext _context;

        public PostRepository(PostDbContext context)
        {
            _context = context;
        }

        public async Task CreatePost(Post post)
        {
            var postDb = post.ToDataAccess();
            await _context.Posts.AddAsync(postDb);
            await _context.SaveChangesAsync();
            
        }

        public async Task<Post> GetPostById(int id)
        {
            var query = await _context.Posts.FindAsync(id);
            if (query != null)
            {
                return query.ToDomain();
            }
            throw new ArgumentException("Couldn't find post with that Id", nameof(id));
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            if (_context.Posts.Count() > 0)
            {
            
                var posts = await _context.Posts.Include(c => c.Comments).ToListAsync();
                return posts.Select(p => p.ToDomain());
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
