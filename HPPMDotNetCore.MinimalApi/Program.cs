using HPPMDotNetCore.DbService;
using HPPMDotNetCore.Models;
using HPPMDotNetCore.Models.ApiModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.UseSerilog((ctx, lc) => lc
//    .WriteTo.Console()
//    .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/Hppmlogs.log")));


string connectionString = builder.Configuration.GetConnectionString("DbConnection");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EFDbContext>(opt =>
{
    opt.UseSqlServer(connectionString);
});
builder.Services.AddScoped(n => new DapperService(connectionString));
//builder.Services.AddScoped<DapperService>();

var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/Hppmlogs.log"))
    .WriteTo.MSSqlServer(
                   connectionString: configuration.GetSection("Serilog:ConnectionStrings:LogDatabase").Value,
                   tableName: configuration.GetSection("Serilog:TableName").Value,
                   appConfiguration: configuration,
                   autoCreateSqlTable: true,
                   columnOptionsSection: configuration.GetSection("Serilog:ColumnOptions"),
                   schemaName: configuration.GetSection("Serilog:SchemaName").Value)
    .CreateLogger();
Log.Information("Logging");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSerilogRequestLogging();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
}).WithName("GetWeatherForecast");

app.MapGet("/blog/{pageNo}/{pageSize}",
    async (int pageNo, int pageSize, EFDbContext _dbContext) =>
{
    var blogList = await _dbContext
                .Blogs
                .AsNoTracking()
                .Pagination(pageNo,
                            pageSize)
                .ToListAsync();

    ResponseModel response = BaseResponseModel.GetSuccess(blogList);

    return Results.Ok(response);
})
    .WithName("GetBlog");

app.MapGet("/blog/{id}", async (int id, EFDbContext _db) =>
{
    var blog = await _db
            .Blogs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Blog_Id == id);

    ResponseModel response = BaseResponseModel.GetSuccess(blog);

    return Results.Ok(response);
}).WithName("GetBlogItem");

app.MapPost("/blog", async (BlogDataModel blog, EFDbContext db) =>
{
    await db.Blogs.AddAsync(blog);
    int result = await db.SaveChangesAsync();
    ResponseModel response = result > 0 ?
        BaseResponseModel.GetSuccess() : BaseResponseModel.GetError();
    return Results.Ok(response);

}).WithName("PotBlog");

app.MapPut("/blog/{id}", async (int id, BlogDataModel blog, EFDbContext db) =>
{

    db.Entry(blog).State = EntityState.Modified;
    db.Blogs.Update(blog);
    int result = await db.SaveChangesAsync();

    ResponseModel response = result > 0 ?
        BaseResponseModel.GetSuccess() : BaseResponseModel.GetError();
    return Results.Ok(response);

}).WithName("PutBlog");

app.MapDelete("/blog/{id}", async (int id, EFDbContext db) =>
{
    BlogDataModel blog = await db
                        .Blogs
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Blog_Id == id);

    db.Entry(blog).State = EntityState.Deleted;
    db.Blogs.Update(blog);
    int result = await db.SaveChangesAsync();

    ResponseModel response = result > 0 ?
        BaseResponseModel.GetSuccess() : BaseResponseModel.GetError();
    return Results.Ok(response);

}).WithName("DeleteBlog");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public static class DevCode
{
    public static IQueryable<TSource> Pagination<TSource>(this IQueryable<TSource> source, int pageNo, int pageSize) where TSource : class
    {
        int skipRowCount = (pageNo - 1) * pageSize;
        return source.Skip(skipRowCount).Take(pageSize);
    }
}