using EspaciosLogicAPI.GrpcClients;

var builder = WebApplication.CreateBuilder(args);

// gRPC Client
var grpcServerUrl = "https://localhost:5258";
builder.Services.AddSingleton(sp => new EspaciosGrpcClient(grpcServerUrl));

// Controllers
builder.Services.AddControllers();

// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();