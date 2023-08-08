using HPPMDotNetCore.DbService;
using HPPMDotNetCore.Models;
using HPPMDotNetCore.Repositories.Wrapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPPMDotNetCore.ConsoleApp.RepositoryCodeExample
{
    public class RepositoryExample
    {
        private IRepositoryWrapper _repoWrapper;
        
        private void CreateWrapper()
        {
            //if (_repoWrapper == null) _repoWrapper = new RepositoryWrapper(new EFDbContext());
            _repoWrapper ??= new RepositoryWrapper(new EFDbContext()); 
        }

        public async Task RunAsync()
        {
            CreateWrapper();
            #region Blogs
            //Retrieve
            var blogs =  await _repoWrapper
                .Blog
                .FindAll()
                .ToListAsync();

            // Pagination
            var blogPagination = await _repoWrapper
                .Blog
                .FindAll()
                .Skip(1)
                .Take(20)
                .ToListAsync();
           

            // Create
            BlogDataModel createModel = BlogDataModel.Create();
            int createResult = await _repoWrapper
                .Blog
                .CreateAsync(createModel);

            // Get Item
            BlogDataModel blog = await _repoWrapper
                .Blog
                .FindByCondition(x => x.Blog_Id == createModel.Blog_Id)
                .FirstOrDefaultAsync();
            

            //Update           
            int updateResult = _repoWrapper
                .Blog
                .Update(createModel);

            //Delete
            int deleteResult = _repoWrapper
                .Blog
                .Delete(createModel);

            #endregion

            #region Expense Tracker
            //Get all
            var expenses = await _repoWrapper
                .ExpenseTracker
                .FindAll()
                .ToListAsync();

            //Create
            ExpenseTrackerDataModel createExpModel = ExpenseTrackerDataModel.Create();
            int ExpCreateResult = await _repoWrapper
                .ExpenseTracker
                .CreateAsync(createExpModel);

            //Get item
            var expenseTracker = await _repoWrapper
                .ExpenseTracker
                .FindByCondition(x => x.Id == createExpModel.Id)
                .FirstOrDefaultAsync();
          
            //Update
            int ExpUpdateResult = _repoWrapper
                .ExpenseTracker
                .Update(createExpModel);
            
            //Delete
            int ExpDeleteResult = _repoWrapper
                .ExpenseTracker
                .Delete(createExpModel);
            #endregion
        }

    }
}
