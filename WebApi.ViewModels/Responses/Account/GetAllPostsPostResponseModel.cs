using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.ViewModels.Responses.Account
{
    public class GetAllPostsPostResponseModel
    {
        public IEnumerable<PostGetAllPostsPostResponseModelItem> Posts { get; set; }
    }

    public class PostGetAllPostsPostResponseModelItem
    {
        public IEnumerable<PostGetAllPostsPostResponseModelItem> Genres { get; set; }
    }

    public class CategoryGetAllPostsPostResponseModelItem
    {
        public IEnumerable<PostGetAllPostsPostResponseModelItem> Genres { get; set; }
    }
}
