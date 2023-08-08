using HPPMDotNetCore.Models.ApiModels;
using RepoDb.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace HPPMDotNetCore.Models
{
    [Map("[Tbl_Blog]")]
    [Table("Tbl_Blog")]
    public class BlogDataModel
    {
        [Primary]
        [Key]
        [Column("Blog_Id")]
        [Map("Blog_Id")]
        public int Blog_Id { get; set; }

        [Column("Blog_Title")]
        [Map("Blog_Title")]
        public string Blog_Title { get; set; }

        [Column("Blog_Author")]
        [Map("Blog_Author")]
        public string Blog_Author { get; set; }

        [Column("Blog_Content")]
        [Map("Blog_Content")]
        public string Blog_Content { get; set; }

        //Models
        public static BlogDataModel Create() => new BlogDataModel
        {
            Blog_Title = "The Benefits of Meditation",
            Blog_Author = "Sarah Johnson",
            Blog_Content = "Meditation is a practice that ...",
        };
        public static BlogDataModel Update() => new BlogDataModel
        {
            Blog_Id = 2,
            Blog_Title = "Updated 5 Reasons to Travel Solo",
            Blog_Author = "David Lee",
            Blog_Content = "Traveling solo can be  ...",
        };
    }

    public class BlogResponseModel : BaseResponseModel
    {

    }

    public class BlogListResponseModel : BaseResponseModel
    {
        public List<BlogDataModel> Blogs { get; set; }
    }
  
}