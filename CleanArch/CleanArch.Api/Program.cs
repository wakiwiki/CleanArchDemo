using CleanArch.Api.Configurations;
using CleanArch.Infra.Data.Context;
using CleanArch.Infra.IoC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder();
var sharedFolder = Path.Combine(builder.Environment.ContentRootPath, "..", "Shared");
builder.Configuration
    .AddJsonFile(Path.Combine(sharedFolder, "sharedSettings.json"), true) // When running using dotnet run
    .AddJsonFile("sharedSettings.json", true);// When app is published
builder.Services.AddDbContext<UniversityDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("UniversityDBConnection"));
});

builder.Services.AddMvc();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "University Api", Version = "v1" });
});
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
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "University Api V1");
});
app.UseRouting();
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

app.Run();








