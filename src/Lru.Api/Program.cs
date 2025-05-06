using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Lru.Core;

var builder = WebApplication.CreateBuilder(args);

// Register the LRU cache as a singleton with capacity = 2
builder.Services.AddSingleton<ILruCache<string, string>>(sp => new LruCache<string, string>(2));

var app = builder.Build();

// GET /cache/{key}
app.MapGet("/cache/{key}", (string key, ILruCache<string, string> cache) =>
{
    return cache.TryGet(key, out var value)
        ? Results.Ok(value)
        : Results.NotFound();
});

// PUT /cache/{key}
app.MapPut("/cache/{key}", async (string key, HttpContext ctx, ILruCache<string, string> cache) =>
{
    using var reader = new StreamReader(ctx.Request.Body);
    var body = await reader.ReadToEndAsync();
    var value = System.Text.Json.JsonSerializer.Deserialize<string>(body);
    cache.AddOrUpdate(key, value!);
    return Results.NoContent();
});

// DELETE /cache/{key}
app.MapDelete("/cache/{key}", (string key, ILruCache<string, string> cache) =>
{
    return cache.Delete(key)
        ? Results.NoContent()
        : Results.NotFound();
});

app.Run();
