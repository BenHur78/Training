var builder = WebApplication.CreateBuilder(args);

//Adding services to the container
builder.Services.AddControllersWithViews();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.MapDefaultControllerRoute();

app.Run();
