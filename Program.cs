
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using tmdb_web_application.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register HttpClient
builder.Services.AddHttpClient();

// Register TmdbService
builder.Services.AddScoped<TmdbService>();

// Configure Identity and Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// ... rest of your code

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/register", async (HttpContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) =>
{
    var email = context.Request.Form["email"];
    var password = context.Request.Form["password"];

    var user = new IdentityUser { UserName = email, Email = email };
    var result = await userManager.CreateAsync(user, password);

    if (result.Succeeded)
    {
        await signInManager.SignInAsync(user, isPersistent: false);
        context.Response.Redirect("/Home/Index");
    }
    else
    {
        foreach (var error in result.Errors)
        {
            await context.Response.WriteAsync(error.Description);
        }
    }
});

app.MapPost("/login", async (HttpContext context, SignInManager<IdentityUser> signInManager) =>
{
    var email = context.Request.Form["email"];
    var password = context.Request.Form["password"];
    var rememberMe = context.Request.Form["rememberMe"] == "true";

    var result = await signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);

    if (result.Succeeded)
    {
        context.Response.Redirect("/Home/Index");
    }
    else
    {
        await context.Response.WriteAsync("Invalid login attempt.");
    }
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
