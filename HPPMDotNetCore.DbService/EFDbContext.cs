using HPPMDotNetCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPPMDotNetCore.DbService
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() { }
        public EFDbContext(DbContextOptions options) : base(options)
        {
        }

        //public EFDbContext() : base(GetConnectioString())
        //{
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Config.GetConnectionString());
            }
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
        public DbSet<ExpenseTrackerDataModel> ExpenseTrackers { get; set; }
        public DbSet<PieChartDataModel> PieCharts { get; set; }
        public DbSet<ProductDataModel> Products { get; set; }
        public DbSet<ProductOrderDataModel> ProductOrders { get; set; }
    }
}
