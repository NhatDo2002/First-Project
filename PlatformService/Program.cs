using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;
using PlatformService.Extensions;
using PlatformService.AsyncDataServices;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.ConfigurationDatabase(builder.Environment, builder.Configuration);
builder.Services.AddOpenApi();
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
//Register a host service that will initialize the MessageBusClient on startup
builder.Services.AddHostedService<RabbitMQInitializerHostedService>();

// Console.WriteLine($"--> Command Service endpoint: {IConfiguration["CommandService"]}");
var app = builder.Build();
app.MapControllers();
PrepDb.PrepPopulation(app, app.Environment.IsProduction());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


// app.UseHttpsRedirection();

app.Run();

