using EspaciosGrpcDatos.Data;
using EspaciosGrpcDatos.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// Database Connection
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("No connection string found");

builder.Services.AddDbContext<EspaciosContext>(opt =>
    opt.UseMySQL(connectionString)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<EspaciosService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();