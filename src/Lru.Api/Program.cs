// Lru.Api/Program.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
// TODO: Register LruCache<string,string> implementation
// builder.Services.AddSingleton<ILruCache<string, string>, LruCache<string, string>>();

var app = builder.Build();

// TODO: Map cache endpoints

app.Run();
