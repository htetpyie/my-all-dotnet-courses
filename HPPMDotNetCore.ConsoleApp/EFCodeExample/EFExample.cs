using HPPMDotNetCore.DbService;
using HPPMDotNetCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPPMDotNetCore.ConsoleApp.EFCodeExample
{
    public class EFExample
    {
        public static async Task RunAsync()
        {
            EFDbContext db = new EFDbContext();

            // Create
            var insertModel = BlogDataModel.Create();
            await db.Blogs.AddAsync(insertModel);
            await db.SaveChangesAsync();

            //Get By Id
            var getByIdModel = await db.Blogs
               .FirstOrDefaultAsync(x => x.Blog_Id == insertModel.Blog_Id);

            // Update
            getByIdModel.Blog_Content = "testing";
            db.Entry(getByIdModel).State = EntityState.Modified;
            db.Blogs.Update(getByIdModel);
            await db.SaveChangesAsync();

            // Delete
            var deleteByIdModel = await db.Blogs
                .FirstOrDefaultAsync(x => x.Blog_Id == insertModel.Blog_Id);
            db.Entry(deleteByIdModel).State = EntityState.Deleted;
            db.Blogs.Remove(deleteByIdModel);
            await db.SaveChangesAsync();
        }
    }
}
