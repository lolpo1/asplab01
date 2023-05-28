using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");


app.UseFileServer(new FileServerOptions
{
    EnableDirectoryBrowsing = true,
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), @"static"))
});

// Використовуємо спеціальний middleware для обробки помилок
app.UseStatusCodePages(async statusCodeContext =>
{
    var response = statusCodeContext.HttpContext.Response;
    response.ContentType = "text/html; charset=UTF-8";

    if (response.StatusCode == 404)
    {
        var webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var fileProvider = new PhysicalFileProvider(webRoot);
        var file = fileProvider.GetFileInfo("404.html");
        await response.SendFileAsync(file.PhysicalPath);
    }
});


app.MapGet("/home/index", async (context) =>
{
    context.Response.ContentType = "text/plain; charset=UTF-8";
    await context.Response.WriteAsync("Home Index");
});

app.MapGet("/home/about", async (context) =>
{
    context.Response.ContentType = "text/plain; charset=UTF-8";
    await context.Response.WriteAsync("Home about");
});

app.UseFileServer();

// Middleware для обробки неіснуючих сторінок (404)
app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == StatusCodes.Status404NotFound)
    {
        context.Response.ContentType = "text/html";
        var fileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
        var file = fileProvider.GetFileInfo("404.html");

        await context.Response.SendFileAsync(file.PhysicalPath);
    }
});


app.Run();
