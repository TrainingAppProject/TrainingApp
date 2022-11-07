<!--
/// <summary>
/// Module purpose: Program c# file for initial project start, which connects to the database, defines authentication data and app settings
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for the COMP7022 project / Auto-generated
/// Revision History:
///
/// </summary>
-->

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrainingApp.Services;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextFactory<TrainingDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<TrainingDbContext>(
    p => p.GetRequiredService<IDbContextFactory<TrainingDbContext>>().CreateDbContext());

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.Cookie.Name = "trainingapp_cookie";

        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
    });

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    IDbContextFactory<TrainingDbContext> contextFactory =
//        scope.ServiceProvider.GetRequiredService<IDbContextFactory<TrainingDbContext>>();

//    using (TrainingDbContext context = contextFactory.CreateDbContext())
//    {
//        context.Database.Migrate();
//    }
//}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseDeveloperExceptionPage();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();

app.Run();

