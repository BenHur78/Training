using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(
    opts => { opts.UseSqlServer(builder.Configuration["ConnectionStrings:SportsStoreConnection"]); }
);

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

builder.Services.AddRazorPages();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.UseStaticFiles();

app.MapControllerRoute("catpage", "{category}/Page{productPage:int}", new { Controller = "Home", action = "Index" });

app.MapControllerRoute("page", "Page{productPage:int}", new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("category", "{category}", new { Controller = "Home", action = "Index", productPage = 1 });

//There is a coupling here on "productPage", HomeController.Index parameter and PageLinkTagHelper. If we change 'productPage' to another name
//you have to update HomeController.Index parameter and PageLinkTagHelper.
app.MapControllerRoute("pagination", "Products/Page{productPage}", new { Controller = "Home", action = "Index" });

app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();
