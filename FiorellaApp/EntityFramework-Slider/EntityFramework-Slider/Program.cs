using EntityFramework_Slider.Data;
using EntityFramework_Slider.Services;
using EntityFramework_Slider.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(); //bunu bele qeyd edirikki yeni burada session storage istifade edeceyik

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();   //servicelerin brauserdeki(cookiedeki) datalara el chatanligini temin etm,ek uchun

builder.Services.AddScoped<ILayoutService, LayoutService>();  //LayoutServiceni istifade etmek uchun 

builder.Services.AddScoped<IBasketService, BasketService>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IBlogService, BlogService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<ISliderService, SliderService>();

builder.Services.AddScoped<IExpertService, ExpertService>();

builder.Services.AddScoped<IFooterService, FooterService>();









var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); //static fayllar uchun yeni jc css falan

app.UseSession(); // session istifade edeceyik

app.UseRouting();

app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//     
//});

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
