using Microsoft.EntityFrameworkCore;
using MvcStartApp.Data;
using MvcStartApp.Data.Interfaces;
using MvcStartApp.Data.Repositories;
using MvcStartApp.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов в контейнер DI
builder.Services.AddControllersWithViews();

// Регистрация контекста БД
builder.Services.AddDbContext<BlogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация репозитория
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
//builder.Services.AddTransient<LoggingMiddleware>();

//// Регистрация контекста БД (должно быть ДО builder.Build())
//string connection = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<BlogContext>(options =>
//    options.UseSqlServer(connection));

var app = builder.Build();

// Конфигурация middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Кастомное middleware
app.UseMiddleware<LoggingMiddleware>();

// Маршрутизация
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();