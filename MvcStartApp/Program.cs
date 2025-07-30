using Microsoft.EntityFrameworkCore;
using MvcStartApp.Data;
using MvcStartApp.Data.Interfaces;
using MvcStartApp.Data.Repositories;
using MvcStartApp.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ���������� �������� � ��������� DI
builder.Services.AddControllersWithViews();

// ����������� ��������� ��
builder.Services.AddDbContext<BlogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ����������� �����������
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
//builder.Services.AddTransient<LoggingMiddleware>();

//// ����������� ��������� �� (������ ���� �� builder.Build())
//string connection = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<BlogContext>(options =>
//    options.UseSqlServer(connection));

var app = builder.Build();

// ������������ middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// ��������� middleware
app.UseMiddleware<LoggingMiddleware>();

// �������������
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();