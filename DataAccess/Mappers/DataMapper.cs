using System.Linq;
using DataAccess.Models;

namespace DataAccess.Mappers
{

    public static class DataMapper
    {

            
       public static Domain.Models.Post ToDomain(this Post post)
        {
            Domain.Models.Post domainPost = new();
            domainPost.PostId = post.PostId;
            domainPost.Description = post.Description;
            domainPost.Image = post.Image;
            domainPost.Timestamp = post.Timestamp;
            if (post.Comments is not null)
                domainPost.Comments = post.Comments
                    .Select(c => c.ToDomain(domainPost)).ToHashSet();

            return domainPost;
        }


         public static Domain.Models.Comment ToDomain(this Comment comment,
            Domain.Models.Post post)
        {
            Domain.Models.Comment domainComment = new();
            domainComment.Id = comment.Id;
             domainComment.Description = comment.Description;
            domainComment.PostId = post.PostId;
            domainComment.Timestamp = comment.Timestamp;

            return domainComment;
        }

       

        public static DataAccess.Models.Post ToDataAccess(this Domain.Models.Post post)
        {
            DataAccess.Models.Post dbPost = new();

            dbPost.PostId = post.PostId;
            dbPost.Description = post.Description;
            dbPost.Image = post.Image;
            dbPost.Timestamp = post.Timestamp;
            if (post.Comments is not null)
                dbPost.Comments = post.Comments
                    .Select(c => c.ToDataAccess(dbPost)).ToHashSet();
            return dbPost;

        }


        public static DataAccess.Models.Comment ToDataAccess(this Domain.Models.Comment comment, Post post)
        {
            DataAccess.Models.Comment dbComment = new()
            {
                Id = comment.Id,
                Description = comment.Description,
                PostId = post.PostId,
                Timestamp = comment.Timestamp,
            };

            return dbComment;
        }


    }





}