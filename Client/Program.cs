using AsanRegEx.Client;
using AsanRegEx.Client.Services;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<RegexService>();
builder.Services.AddSingleton<CacheService>();
builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();
