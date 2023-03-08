using Microsoft.AspNetCore.Authentication.Cookies;
using Movies.WEB;
using Movies.WEB.Services;
using Movies.WEB.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<ICategoryService, CategoryService>();
builder.Services.AddHttpClient<IMovieService, MoviesService>();
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddHttpClient<IReviewService, ReviewService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMovieService, MoviesService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//Authenticantion
builder.Services.AddSession(options => options.IdleTimeout= TimeSpan.FromMinutes(20));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = $"/Auth/AccessDenied";
        options.Cookie.Name = "cookie";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    });

//

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    SD.APIBase = builder.Configuration["ServiceUrls:CategoryAPILocal"];
}

if (app.Environment.IsProduction())
{
    SD.APIBase = builder.Configuration["ServiceUrls:CategoryAPIRemote"];
}

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

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.UseCookiePolicy();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
