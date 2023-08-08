using System;
using HPPMDotNetCore.ExpenseTracker.Features.Dashboard;
using HPPMDotNetCore.ExpenseTracker.Features.Income;
using HPPMDotNetCore.ExpenseTracker.Hubs;
using HPPMDotNetCore.ExpenseTracker.Middleware;
using HPPMDotNetCore.ExpenseTracker.Features.Email;
using HPPMDotNetCore.ExpenseTracker.Features.Expense;
using HPPMDotNetCore.ExpenseTracker.Features.Income;
using HPPMDotNetCore.ExpenseTracker.Features.SignUp;
using HPPMDotNetCore.ExpenseTracker.Features.User;
using HPPMDotNetCore.ExpenseTracker.Features.UserType;
using HPPMDotNetCore.ExpenseTracker.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace HPPMDotNetCore.ExpenseTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSignalR();
            services.AddDistributedMemoryCache();
            services.AddSession(options => { options.IdleTimeout = TimeSpan.FromHours(1); });
            services.AddDbContext<EfDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DbConnection"));
            });

            services.Configure<EmailSetting>(Configuration.GetSection("EmailSetting"));
            services.Configure<AppSetting>(Configuration.GetSection("AppSettings"));

            services.AddScoped(n => new DapperService(Configuration.GetConnectionString("DbConnection")));
            services.AddScoped<IIncomeService, IncomeService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserTypeService, UserTypeService>();
            services.AddScoped<ISignUpService, SignUpService>();
            services.AddScoped<IDashboardService, DashboardService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseSessionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=SignIn}/{action=Login}/{id?}");
                endpoints.MapHub<BalanceHub>("/balanceHub");
            });
        }
    }
}