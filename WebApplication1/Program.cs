using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppRepositoryWithUOW.EF.Data;
using WebAppRepositoryWithUOW.EF.IdentityModels;
using WebAppRepositoryWithUOW.EF.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

//register generic base repository
//builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

//register Db options 
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("cs"),
        b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
        ));

//register UserManager<IdentityUser> == repository for IdentityUser class - RoleManager<IdentityRole> - SignInManager<IdentityUser> 
builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()    //add UserStore class - RoleStore class that interact with database 
                .AddDefaultTokenProviders();

//register IunitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//register Auto Mapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();   //check cookie for credential (username & password)

app.UseAuthorization();   //check for role

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();