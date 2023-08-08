using HPPMDotNetCore.DbService;
using HPPMDotNetCore.Models;
using HPPMDotNetCore.Repositories.Base;

namespace HPPMDotNetCore.Repositories.Repo
{
    public class BlogRepository : RepositoryBase<BlogDataModel>, IBlogRepository
    {
        public BlogRepository(EFDbContext db) : base(db)
        {
        }
    }
}
