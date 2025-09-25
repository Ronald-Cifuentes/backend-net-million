using Million.Api.Models;
using Million.Api.Repositories;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Bind MongoSettings
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));

// Registrar IMongoClient con la connection string del configuration
var connectionString = builder.Configuration["MongoSettings:ConnectionString"] ?? "mongodb://localhost:27017";
builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(connectionString));

// Registrar repositorio
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
