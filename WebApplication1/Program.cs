using Microsoft.EntityFrameworkCore;
using WebAppRepositoryWithUOW.Core;
using WebAppRepositoryWithUOW.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
//register Db options 
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("cs"),
        b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
        ));
//register generic base repository
//builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
//register IunitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
