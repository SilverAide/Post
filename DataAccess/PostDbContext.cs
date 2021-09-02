using System;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

using Domain.Models;

namespace DataAccess
{
    public class PostDbContext : DbContext
    {
        public PostDbContext([NotNull] DbContextOptions options) :
            base(options)
        { }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Description)
                    .IsRequired(true);

                entity.Property(e => e.Timestamp)
                    .IsRequired(true);
            });

            modelBuilder.Entity<Post>()
                .HasData(new[]
                {
                    new Post
                    {
                        Id = 1,
                        Image = File.ReadAllBytes("../Images/Dogs.jpg"),
                        Description = "This is a test post",
                        Timestamp = DateTime.Now
                    },
                    new Post
                    {
                        Id = 2,
                        Image = File.ReadAllBytes("../Images/Dogs.jpg"),
                        Description = "This is another test post",
                        Timestamp = DateTime.Now.AddHours(1)
                    },
                    new Post
                    {
                        Id = 3,
                        Image = File.ReadAllBytes("../Images/Dogs.jpg"),
                        Description = "This is the last test post",
                        Timestamp = DateTime.Now.AddHours(2)
                    }
                });
        }
    }
}
