using Microsoft.EntityFrameworkCore;
using PastryShop.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

//Invoke the GetCart() method, passing it the Service Provider instance.
//The scoped instance ensures that the Shopping Cart
builder.Services.AddScoped<IShoppingCart,ShoppingCart>(sp => ShoppingCart.GetCart(sp));

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PastryShopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PastryShopConnectionString"));
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
app.UseSession(); 

app.UseRouting();

app.UseAuthorization();


app.MapDefaultControllerRoute();//"{controller=Home}/{action=Index}/{id?}"

DbInitializer.Seed(app);

app.Run();
