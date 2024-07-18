using Microsoft.EntityFrameworkCore;
using Product_MVC.Models.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Have to Add DB Context Service Container Here
builder.Services.AddDbContext<AppDbContext>(options =>
{
    //get the Connection String
    var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    //Provide this to Applications DBContext Class
    options.UseSqlServer(ConnectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
