using TruongDinhQuan_QuanLyThuVien.Components;
using MudBlazor.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TruongDinhQuan_QuanLyThuVien.Service;
using TruongDinhQuan_QuanLyThuVien.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();


//login Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_token";
        options.LoginPath = "/";
        options.AccessDeniedPath = "/denied";
        options.Cookie.MaxAge = TimeSpan.FromMinutes(10);  // Gi? nguyên th?i gian s?ng c?a cookie

        //options.Cookie.IsEssential = true; // Đảm bảo cookie là cần thiết
        //options.Cookie.Expiration = null;  // Đặt expiration là null để cookie trở thành session cookie
        options.SlidingExpiration = true;  // Gia hạn thời gian sống cookie khi người dùng hoạt động
    });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

//BookDbcontext
builder.Services.AddDbContext<QLTVDbcontext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//service User
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

//Authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
