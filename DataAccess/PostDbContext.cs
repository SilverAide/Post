using System;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

using DataAccess.Models;

namespace DataAccess
{
    public class PostDbContext : DbContext
    {
        public PostDbContext([NotNull] DbContextOptions options) :
            base(options)
        { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.PostId);

                entity.Property(e => e.Description)
                    .IsRequired(true);

                entity.Property(e => e.Timestamp)
                    .IsRequired(true);
            });

             modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Description)
                    .IsRequired(true);

                    entity.Property(e => e.PostId)
                    .IsRequired(true);

                entity.Property(e => e.Timestamp)
                    .IsRequired(true);
                
                 entity.HasOne(e => e.Post)
                      .WithMany(e => e.Comments)
                      .IsRequired()
                      .HasForeignKey(e => e.PostId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Post>()
                .HasData(new[]
                {
                    new Post
                    {
                        PostId = 1,
                        Image = File.ReadAllBytes("../Images/Dogs.jpg"),
                        Description = "This is a test post",
                        Timestamp = DateTime.Now
                    },
                    new Post
                    {
                        PostId = 2,
                        Image = File.ReadAllBytes("../Images/Dogs.jpg"),
                        Description = "This is another test post",
                        Timestamp = DateTime.Now.AddHours(1)
                    },
                    new Post
                    {
                        PostId = 3,
                        Image = File.ReadAllBytes("../Images/Dogs.jpg"),
                        Description = "This is the last test post",
                        Timestamp = DateTime.Now.AddHours(2)
                    }
                });


                modelBuilder.Entity<Comment>()
                .HasData(new[]
                {
                    new Comment
                    {
                        Id = 1,
                        PostId = 1,
                        Description = "This is a test post",
                        Timestamp = DateTime.Now
                    },
                    new Comment
                    {
                        Id = 2,
                        PostId = 1,
                        Description = "This is another test post",
                        Timestamp = DateTime.Now.AddHours(1)
                    },
                    new Comment
                    {
                        Id = 3,
                        PostId = 1,
                        Description = "This is the last test post",
                        Timestamp = DateTime.Now.AddHours(2)
                    }
                });
        }
    }
}
