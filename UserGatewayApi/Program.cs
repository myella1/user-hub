using UserGatewayApi.Services;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using UserGatewayApi.Options;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
builder.Services.AddHttpClient<IGatewayService, GatewayService>();
builder.Services.AddOptions<GatewayServiceOptions>().BindConfiguration("GatewayServiceOptions");
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<GatewayServiceOptions>(options =>
                                                    { 
                                                        var configuration = builder.Configuration.GetSection("GatewayServiceOptions");                                                         
                                                    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        policy =>
        {
            policy.WithOrigins("https://localhost:44311/")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
        });
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gateway Api service", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorClient");
app.UseAuthorization();
app.UseRouting();


app.MapControllers();

app.Run();
