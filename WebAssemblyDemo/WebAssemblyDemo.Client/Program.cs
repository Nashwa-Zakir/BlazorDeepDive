using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ServerManagement.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient("ServerApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7277/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.AddTransient<IServersApiRepository, ServersApiRepository>();


await builder.Build().RunAsync();
