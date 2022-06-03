
using CleanArch.Infra.Data.Context;
using CleanArch.Infra.IoC;
using CleanArch.Mvc.Configurations;
using CleanArch.Mvc.Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder();
var sharedFolder = Path.Combine(builder.Environment.ContentRootPath, "..", "Shared");
builder.Configuration
    .AddJsonFile(Path.Combine(sharedFolder, "sharedSettings.json"), true) // When running using dotnet run
    .AddJsonFile("sharedSettings.json", true);// When app is published
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UniversityIdentityDBConnection")));
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDbContext<UniversityDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("UniversityDBConnection"));
});

builder.Services.AddMvc();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.RegisterAutoMapper();

DependencyContainer.RegisterServices(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllerRoute(
        "default", "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
