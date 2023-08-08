using System;

namespace HPPMDotNetCore.Queries
{
    public class BlogQuery
    {
        public static string GetListQuery
        {
            get
            {
                return "select * from tbl_blog";
            }
        }
        public static string GetItemQuery
        {
            get
            {
                return "select * from tbl_blog where blog_id = @blog_id";
            }
        }
        public static string InsertQuery
        {
            get
            {
                return @$"INSERT INTO tbl_blog
                            (blog_title, 
                            blog_author, 
                            blog_content)
                        VALUES( @Blog_Title,
                                @Blog_Author, 
                                @Blog_Content )";
            }
        }
        public static string UpdateQuery
        {
            get
            {
                return @$"UPDATE tbl_blog
                            SET blog_title = @Blog_Title, 
                                blog_author = @Blog_Author, 
                                blog_content = @Blog_Author 
                            WHERE blog_id = @Blog_Id";
            }
        }
        public static string DeleteQuery
        {
            get
            {
                return $"DELETE FROM tbl_blog WHERE blog_id = @id";
            }
        }
    }
}
