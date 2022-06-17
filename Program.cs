using CPSDevExerciseWeb.Database;
using CPSDevExerciseWeb.PgSQLRepositories;
using CPSDevExerciseWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
DatabaseSettings.Configuration = builder.Configuration;

builder.Services.AddTransient<IOrdersRepository, PgSQLOrdersRepository>();
builder.Services.AddTransient<IOrdersReader, XMLOrdersReader>();

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
