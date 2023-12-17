using Microsoft.EntityFrameworkCore;
using Quirk.Data;
using Quirk.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<QuirkDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("QuirkConnection")));
builder.Services.AddScoped<ITagRepository, TagRepository >();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository >();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

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
