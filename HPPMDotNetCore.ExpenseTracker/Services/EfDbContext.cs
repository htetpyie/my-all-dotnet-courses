using HPPMDotNetCore.ExpenseTracker.Features.Expense;
using HPPMDotNetCore.ExpenseTracker.Features.Income;
using HPPMDotNetCore.ExpenseTracker.Features.SignUp;
using HPPMDotNetCore.ExpenseTracker.Features.User;
using HPPMDotNetCore.ExpenseTracker.Features.UserType;
using Microsoft.EntityFrameworkCore;

namespace HPPMDotNetCore.ExpenseTracker.Services
{
    public class EfDbContext : DbContext
    {
        public EfDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ExpenseDataModel> Expense { get; set; }
        public DbSet<IncomeDataModel> Income { get; set; }
        public DbSet<UserDataModel> User{ get; set; }
        public DbSet<UserTypeDataModel> UserType{ get; set; }
        public DbSet<SignUpDataModel> SignUp{ get; set; }
    }
}
