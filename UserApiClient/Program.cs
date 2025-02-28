using UserApiClient;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UserApiClient.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app"); 
builder.Services.AddScoped<ApiService>();

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7066/") });

await builder.Build().RunAsync();
